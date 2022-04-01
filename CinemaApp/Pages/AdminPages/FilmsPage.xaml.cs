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
    /// Interaction logic for FilmsPage.xaml
    /// </summary>
    public partial class FilmsPage : Page
    {
        private Movie selectedMovie => DGridMovies.SelectedItem as Movie;

        public FilmsPage()
        {
            InitializeComponent();
            CBSort.ItemsSource = new List<string>() { "Без сортировки", "Название (А-Я)", "Название (Я-А)", "Длительность (возр.)", "Длительность (убыв.)" }; 
            CBSort.SelectedIndex = 0;
        }

        private void DGridMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var enabled = selectedMovie != null;
            BtnEdit.IsEnabled = enabled;
            BtnDelete.IsEnabled = enabled;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Window editor = new Windows.MovieEditorWindow(selectedMovie);
            if (editor.ShowDialog() == true)
            {
                UpdateGrid();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данный фильм?",
   "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                App.Context.Movies.Remove(selectedMovie);
                App.Context.SaveChanges();
                UpdateGrid();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Window editor = new Windows.MovieEditorWindow(null);
            if (editor.ShowDialog() == true)
            {
                UpdateGrid();
            }
        }
        private void UpdateGrid()
        {
            GridEmpty.Visibility = Visibility.Collapsed;
            DGridMovies.Visibility = Visibility.Collapsed;

            List<Movie> movies = App.Context.Movies.ToList();

            if (!string.IsNullOrWhiteSpace(TBSearch.Text))
            {
                movies = movies.Where(x => x.Name.ToLower().Contains(TBSearch.Text.ToLower()) ||
                                           x.Distributor.ToLower().Contains(TBSearch.Text.ToLower())).ToList();
            }


            switch (CBSort.SelectedIndex)
            {
                case 1:
                    movies = movies.OrderBy(x => x.Name).ToList();
                    break;
                case 2:
                    movies = movies.OrderByDescending(x => x.Name).ToList();
                    break;
                case 3:
                    movies = movies.OrderBy(x => x.Duration).ToList();
                    break;
                case 4:
                    movies = movies.OrderByDescending(x => x.Duration).ToList();
                    break;
                default:
                    break;
            }

            if(movies.Count > 0)
            {
                DGridMovies.Visibility = Visibility.Visible;
            }
            else
            {
                GridEmpty.Visibility = Visibility.Visible;
            }

            DGridMovies.ItemsSource = movies;
        }

        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateGrid();
        }

        private void CBSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGrid();
        }
    }
}
