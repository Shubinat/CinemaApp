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

namespace CinemaApp.Pages.AdminPages
{
    /// <summary>
    /// Interaction logic for SessionsPage.xaml
    /// </summary>
    public partial class SessionsPage : Page
    {
        private Session selectedSession => DGridSessions.SelectedItem as Session;

        public SessionsPage()
        {
            InitializeComponent();
        }

        private void DGridSessions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Window editor = new Windows.SessionEditorWindow(selectedSession);
            if (editor.ShowDialog() == true)
            {
                UpdateGrid();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

        }
        private void UpdateGrid()
        {
            List<Session> users = App.Context.Sessions.ToList();

        }
    }
}
