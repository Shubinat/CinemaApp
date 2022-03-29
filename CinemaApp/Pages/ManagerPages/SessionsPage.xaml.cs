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
    /// Логика взаимодействия для SessionsPage.xaml
    /// </summary>
    public partial class SessionsPage : Page
    {
        private Movie selectedMovie => CBoxMovies.SelectedItem as Movie;
        private Session selectedSession => LViewSessions.SelectedItem as Session;
        public SessionsPage()
        {
            InitializeComponent();
            DataBind();
            CBoxMovies.SelectedIndex = 0;
        }
        public SessionsPage(Movie movie)
        {
            InitializeComponent();
            DataBind();
            CBoxMovies.SelectedItem = movie;

        }

        private void DataBind()
        {
            List<Movie> movies = App.Context.Movies.ToList();
            movies.Insert(0, new Movie() { Name = "Все фильмы" });
            CBoxMovies.ItemsSource = movies;
            DPickerRentalDate.SelectedDate = DateTime.Now;
        }

        private void CBoxMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }

        private void DPickerRentalDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }
        
        private void UpdateList()
        {
            var sessions = App.Context.Sessions.ToList();

            if(CBoxMovies.SelectedIndex != 0 && CBoxMovies.SelectedIndex != -1)
                sessions = sessions.Where(x => x.MovieID == selectedMovie.ID).ToList();
            if(DPickerRentalDate.SelectedDate.HasValue)
                sessions = sessions.Where(x => x.DateTime.Date == DPickerRentalDate.SelectedDate.Value.Date).ToList();

            LViewSessions.ItemsSource = sessions;
        }

        private void LViewSessions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(selectedSession != null)
            {
                new Windows.PlacesWindow(selectedSession).ShowDialog();
                LViewSessions.SelectedItem = null;
            }
        }
    }
}
