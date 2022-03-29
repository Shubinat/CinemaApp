using CinemaApp.Entities;
using CinemaApp.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CinemaApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для PlacesWindow.xaml
    /// </summary>
    public partial class PlacesWindow : Window
    {
        private Session _session;
        public PlacesWindow(Session session)
        {
            InitializeComponent();
            _session = session;
            GenerateHall(session);

        }

        private void GenerateHall(Session session)
        {
            Hall.Children.Clear();
            foreach (var row in session.Hall.Places.GroupBy(x => x.RowNumber))
            {
                StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
                stackPanel.Children.Add(new TextBlock()
                {
                    Text = "Ряд № " + row.Key.ToString(),
                    Foreground = Brushes.White,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 16,
                    Margin = new Thickness(5),
                    FontWeight = FontWeights.Bold
                });
                foreach (var place in row)
                {
                    PlaceControl element = new PlaceControl(place, session);
                    element.PlaceSelected += Element_PlaceSelected;
                    element.PlaceClicked += Element_PlaceClicked;
                    stackPanel.Children.Add(element);
                }
                Hall.Children.Add(stackPanel);
            }
        }

        private void Element_PlaceClicked(Place place)
        {
            var charSet = "QWERTYUIOPASDFGHJKLZXCVBNM1234567890";
            var randNumber = "";
            do
            {
                randNumber = CreateRandNumber(charSet);
            } while (App.Context.Tickets.ToList().FirstOrDefault(x => x.Number == randNumber) != null);
            var ticket = new Ticket()
            {
                Number = randNumber,
                Session = _session,
                RealeseDate = DateTime.Now,
                TicketStatu = App.Context.TicketStatus.Find(1),
                Place = place
            };
            new PrintTicketWindow(ticket).ShowDialog();
            GenerateHall(_session);
        }

        private string CreateRandNumber(string charSet)
        {
            Random random = new Random();
            var result = "";
            for (int i = 0; i < 8; i++)
                result += charSet[random.Next(charSet.Length)];
            return result;
        }

        private void Element_PlaceSelected(Place place, bool isBusy)
        {
            TBlockInfo.Text = $"Ряд № {place.RowNumber}, Место № {place.PlaceNumber}";
            TBlockPrice.Text = $"Цена {App.Context.TicketPrices.ToList().FirstOrDefault(x => x.Session == _session && x.HallSector == place.HallSector)?.Price}";
            TBlockCategory.Text = $"Тип места: {place.HallSector.Name}";
        }

    }
}
