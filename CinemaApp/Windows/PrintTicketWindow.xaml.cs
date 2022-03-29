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
    /// Логика взаимодействия для PrintTicketWindow.xaml
    /// </summary>
    public partial class PrintTicketWindow : Window
    {
        private Ticket _ticket;
        public PrintTicketWindow(Ticket ticket)
        {
            InitializeComponent();
            DataContext = ticket;
            _ticket = ticket;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if(printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(GridPrintTicket, _ticket.Number);
                App.Context.Tickets.Add(_ticket);
                App.Context.SaveChanges();
                Close();
            }
        }
    }
}
