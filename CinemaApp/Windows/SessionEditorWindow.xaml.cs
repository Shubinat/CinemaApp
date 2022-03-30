using CinemaApp.Entities;
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
    /// Interaction logic for SessionEditorWindow.xaml
    /// </summary>
    public partial class SessionEditorWindow : Window
    {
        private Session _session;
        public SessionEditorWindow(Session session)
        {
            InitializeComponent();
            CBoxHall.ItemsSource = App.Context.Halls.ToList();
            CBoxMovie.ItemsSource = App.Context.Movies.ToList();
            if (session!=null)
            {
                
                CBoxHall.Text = session.Hall.Name;
                CBoxMovie.Text = session.Movie.Name;
                DatePickerSession.SelectedDate = session.DateTime;
                TBoxEconom.Text = session.TicketPrices.First(p => p.SectorID == 2).Price.ToString();
                TBoxSimple.Text = session.TicketPrices.First(p => p.SectorID == 1).Price.ToString();
                TBoxVIP.Text = session.TicketPrices.First(p => p.SectorID == 3).Price.ToString();
                TBoxTimeSession.Text = session.Time;
                _session = session;
            }
            
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_session == null)
                {
                    _session = new Session()
                    {
                        HallID = (CBoxHall.SelectedItem as Hall).ID,
                        MovieID = (CBoxMovie.SelectedItem as Movie).ID,
                        DateTime = DatePickerSession.SelectedDate.Value,
                    };
                    _session.Time = TBoxTimeSession.Text;
                    App.Context.Sessions.Add(_session);
                    App.Context.SaveChanges();
                    var _ticketeconom = new TicketPrice()
                    {
                        SessionID = _session.ID,
                        SectorID = 2,
                        Price = Convert.ToDecimal(TBoxEconom.Text),
                    };
                    App.Context.TicketPrices.Add(_ticketeconom);
                    App.Context.SaveChanges();
                    var _tickesimple = new TicketPrice()
                    {
                        SessionID = _session.ID,
                        SectorID = 1,
                        Price = Convert.ToDecimal(TBoxSimple.Text),
                    };
                    App.Context.TicketPrices.Add(_tickesimple);
                    App.Context.SaveChanges();
                    var _ticketvip = new TicketPrice()
                    {
                        SessionID = _session.ID,
                        SectorID = 3,
                        Price = Convert.ToDecimal(TBoxVIP.Text),
                    };
                    App.Context.TicketPrices.Add(_ticketvip);
                    App.Context.SaveChanges();
                }
                else
                {
                    _session.HallID = (CBoxHall.SelectedItem as Hall).ID;
                    _session.MovieID = (CBoxMovie.SelectedItem as Movie).ID;
                    _session.DateTime = DatePickerSession.SelectedDate.Value;
                    _session.Time = TBoxTimeSession.Text;
                    _session.TicketPrices.First(p => p.SectorID == 2).Price = Convert.ToDecimal(TBoxEconom.Text);
                    _session.TicketPrices.First(p => p.SectorID == 1).Price = Convert.ToDecimal(TBoxSimple.Text);
                    _session.TicketPrices.First(p => p.SectorID == 3).Price = Convert.ToDecimal(TBoxVIP.Text);

                }
                App.Context.SaveChanges();
                DialogResult = true;
                Close();
            }
            catch
            {
                _ = MessageBox.Show("Произошла ошибка. Проаерьте правильность заполнения полей.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
