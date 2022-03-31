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
    /// Interaction logic for TicketEditorWindow.xaml
    /// </summary>
    public partial class TicketEditorWindow : Window
    {
        private Ticket _ticket;
        public TicketEditorWindow(Ticket ticket)
        {
            InitializeComponent();
            CBoxSession.ItemsSource = App.Context.Sessions.ToList();
            CBoxPlace.ItemsSource = App.Context.Places.ToList();
            CBoxStatus.ItemsSource = App.Context.TicketStatus.ToList();
            if (ticket != null)
            {
                TBoxNumber.Text = ticket.Number.ToString();
                CBoxSession.Text = ticket.Session.DateTime.ToString();
                CBoxPlace.Text = ticket.Place.ID.ToString();
                CBoxStatus.Text = ticket.TicketStatu.Name;
                DatePickerRealese.SelectedDate = ticket.RealeseDate;

                _ticket = ticket;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_ticket == null)
                {
                    _ticket = new Ticket()
                    {
                        Number = TBoxNumber.Text,
                        SessionID = (CBoxSession.SelectedItem as Session).ID,
                        PlaceID = (CBoxPlace.SelectedItem as Place).ID,
                        StatusID = (CBoxStatus.SelectedItem as TicketStatu).ID,
                        RealeseDate = DatePickerRealese.SelectedDate.Value

                };
                    App.Context.Tickets.Add(_ticket);
                    App.Context.SaveChanges();
                   
                }
                else
                {
                    _ticket.Number = TBoxNumber.Text;
                    _ticket.SessionID = (CBoxSession.SelectedItem as Session).ID;
                    _ticket.PlaceID = (CBoxPlace.SelectedItem as Place).ID;
                    _ticket.StatusID = (CBoxStatus.SelectedItem as TicketStatu).ID;
                    _ticket.RealeseDate = DatePickerRealese.SelectedDate.Value;
                    

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
