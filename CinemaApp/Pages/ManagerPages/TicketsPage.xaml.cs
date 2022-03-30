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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CinemaApp.Pages.ManagerPages
{
    /// <summary>
    /// Логика взаимодействия для TicketsPage.xaml
    /// </summary>
    public partial class TicketsPage : Page
    {
        public TicketsPage()
        {
            InitializeComponent();
            List<Entities.TicketStatu> ticketStatus = App.Context.TicketStatus.ToList();
            ticketStatus.Insert(0, new Entities.TicketStatu { Name = "Все статусы" });
            CBoxStatus.ItemsSource = ticketStatus;
            CBoxStatus.SelectedIndex = 0;
        }

        private void UpdateGrid()
        {
            DGridTickets.Visibility = Visibility.Collapsed;
            GridEmpty.Visibility = Visibility.Collapsed;
            List<Entities.Ticket> tickets = App.Context.Tickets.ToList();


            if (!string.IsNullOrWhiteSpace(TBoxSearch.Text))
            {
                tickets = tickets.Where(x => x.Number.ToLower().Contains(TBoxSearch.Text.ToLower()) ||
                x.Session.Movie.Name.ToLower().Contains(TBoxSearch.Text.ToLower()) ||
                x.Session.Hall.Name.ToLower().Contains(TBoxSearch.Text.ToLower()) ||
                x.Session.Time.ToLower().Contains(TBoxSearch.Text.ToLower()) ||
                x.Session.Date.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            }

            if (CBoxStatus.SelectedIndex != 0)
            {
                tickets = tickets.Where(x => x.TicketStatu == CBoxStatus.SelectedItem as TicketStatu).ToList();
            }

            if(tickets.Count > 0)
            {
                DGridTickets.Visibility = Visibility.Visible;
                DGridTickets.ItemsSource = tickets.OrderBy(x => x.RealeseDate).ThenBy(x => x.Session.HallID).ToList();
            }
            else
            {
                GridEmpty.Visibility = Visibility.Visible;
            }
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateGrid();
        }


        private void DGridTickets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CBoxStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGrid();
        }
    }
}
