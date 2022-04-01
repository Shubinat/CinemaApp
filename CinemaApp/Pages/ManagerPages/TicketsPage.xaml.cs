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
using Excel = Microsoft.Office.Interop.Excel;

namespace CinemaApp.Pages.ManagerPages
{
    /// <summary>
    /// Логика взаимодействия для TicketsPage.xaml
    /// </summary>
    public partial class TicketsPage : Page
    {
        private Ticket selectedTicket => DGridTickets.SelectedItem as Ticket;

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
            var enabled = selectedTicket != null;
            BtnEdit.IsEnabled = enabled;
        }

        private void CBoxStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGrid();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Window editor = new Windows.TicketEditorWindow(selectedTicket);
            if (editor.ShowDialog() == true)
            {
                UpdateGrid();
            }
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Window editor = new Windows.TicketEditorWindow(null);
            if (editor.ShowDialog() == true)
            {
                UpdateGrid();
            }
        }

        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {
            var list = DGridTickets.ItemsSource as List<Ticket>;

            if (GridEmpty.Visibility != Visibility.Visible)
            {
                Excel.Application app = new Excel.Application();
                Excel.Workbook wb = app.Workbooks.Add();
                Excel.Worksheet sheet = wb.Worksheets.Add();

                sheet.Cells[1, 1] = "Номер";
                sheet.Cells[1, 2] = "Фильм";
                sheet.Cells[1, 3] = "Кинозал";
                sheet.Cells[1, 4] = "Дата";
                sheet.Cells[1, 5] = "Время";
                sheet.Cells[1, 6] = "Цена";
                sheet.Cells[1, 7] = "Статус";

                Excel.Range headers = sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, 7]];
                headers.Font.Bold = true;

                int rowIndex = 2;
                for (int i = 0; i < list.Count; i++)
                {
                    var ticket = list[i];
                    sheet.Cells[rowIndex, 1] = ticket.Number;
                    sheet.Cells[rowIndex, 2] = ticket.Session.Movie.Name;
                    sheet.Cells[rowIndex, 3] = ticket.Session.Hall.Name;
                    sheet.Cells[rowIndex, 4] = ticket.Session.Date;
                    sheet.Cells[rowIndex, 5] = ticket.Session.Time;
                    sheet.Cells[rowIndex, 6] = ticket.TicketStatu.Name;
                    sheet.Cells[rowIndex, 7] = ticket.Price;
                    rowIndex++;
                }
                Excel.Range range = sheet.Range[sheet.Cells[rowIndex, 1], sheet.Cells[rowIndex, 6]];
                range.Merge();
                range.Value = "ИТОГО:";
                range.Font.Bold = true;
                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                sheet.Cells[rowIndex, 7] = list.Where(x => x.StatusID == 1).Sum(x => x.Price);

                sheet.UsedRange.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                    sheet.UsedRange.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                    sheet.UsedRange.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                    sheet.UsedRange.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                    sheet.UsedRange.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle =
                    sheet.UsedRange.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;

                sheet.Columns.AutoFit();

                app.Visible = true;
            }
            else
            {
                _ = MessageBox.Show("Для экспорта нет записей.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }   
        }
    }
}
