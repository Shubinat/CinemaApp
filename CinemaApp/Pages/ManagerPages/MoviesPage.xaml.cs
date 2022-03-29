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
    /// Логика взаимодействия для MoviesPage.xaml
    /// </summary>
    public partial class MoviesPage : Page
    {
        public MoviesPage()
        {
            InitializeComponent();
            
            CBoxStatus.ItemsSource = new string[] {"Все фильмы", "В прокате", "Не в прокате", "Скоро" };
            CBoxStatus.SelectedIndex = 0;
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        private void CBoxStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }
        private void UpdateList()
        {
            List<Entities.Movie> movies = App.Context.Movies.ToList();

            if(CBoxStatus.SelectedIndex != 0)
                movies = movies.Where(x => x.RentalText == CBoxStatus.SelectedItem as string).ToList();
            if(!string.IsNullOrWhiteSpace(TBoxSearch.Text))
                movies = movies.Where(x => x.Name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            LViewMovies.ItemsSource = movies;
        }

        private void LViewMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(LViewMovies.SelectedItem is Movie movie)
            {
                NavigationService.Navigate(new SessionsPage(movie));
                LViewMovies.SelectedItem = null;
            }
        }
    }
}
