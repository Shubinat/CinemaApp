using CinemaApp.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CinemaApp.UserControls
{
    /// <summary>
    /// Логика взаимодействия для PlaceControl.xaml
    /// </summary>
    public partial class PlaceControl : UserControl
    {
        private Place _place;
        private Ticket _ticket;
        public delegate void PlaceSelectedEventHandler(Place place, bool isBusy);
        public delegate void PlaceClickedEventHandler(Place place);
        public event PlaceSelectedEventHandler PlaceSelected;
        public event PlaceClickedEventHandler PlaceClicked;
        public PlaceControl(Place place, Session session)
        {
            InitializeComponent();
            _place = place;
            _ticket = place.Tickets.ToList().Where(x => x.SessionID == session.ID).ToList().FirstOrDefault(x => x.StatusID == 1);
            if(_ticket == null)
            {
                    ImageConverter converter = new ImageConverter();
                    ImgPlace.DataContext = (byte[])converter.ConvertTo(Properties.Resources.place_free, typeof(byte[]));
            }
            else
            {
                ImageConverter converter = new ImageConverter();
                ImgPlace.DataContext = (byte[])converter.ConvertTo(Properties.Resources.place_busy, typeof(byte[]));
            }
        }

        private void ImgPlace_MouseEnter(object sender, MouseEventArgs e)
        {
            GridSelection.Visibility = Visibility.Visible;
            PlaceSelected?.Invoke(_place, _ticket != null);
        }

        private void ImgPlace_MouseLeave(object sender, MouseEventArgs e)
        {
            GridSelection.Visibility = Visibility.Collapsed;
        }

        private void ImgPlace_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(_ticket != null)
            {
                _ = MessageBox.Show("Это место занято!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                PlaceClicked?.Invoke(_place);
            }
        }


    }
}
