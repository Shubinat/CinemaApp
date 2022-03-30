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
            List<Movie> movies = App.Context.Movies.ToList();
            movies.Insert(0, new Movie { Name = "Все фильмы" });
            CBFilter.ItemsSource = movies;
            CBFilter.SelectedIndex = 0;
        }

        private void DGridSessions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var enabled = selectedSession != null;
            BtnEdit.IsEnabled = enabled;
            BtnDelete.IsEnabled = enabled;
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
            Window editor = new Windows.SessionEditorWindow(null);
            if (editor.ShowDialog() == true)
            {
                UpdateGrid();
            }
        }
        private void UpdateGrid()
        {
            List<Session> sessions = App.Context.Sessions.ToList();
            if(CBFilter.SelectedIndex != 0)
                sessions = sessions.Where(session => session.Movie == CBFilter.SelectedItem).ToList();
            if (!string.IsNullOrWhiteSpace(TBSearch.Text))
                sessions = sessions.Where(session => session.Movie.Name.ToLower().Contains(TBSearch.Text.ToLower())).ToList();
            DGridSessions.ItemsSource = sessions;
        }

        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateGrid();
        }

        private void CBFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGrid();
        }
    }
}
