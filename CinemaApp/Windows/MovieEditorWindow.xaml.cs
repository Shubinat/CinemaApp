using CinemaApp.Entities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for MovieEditorWindow.xaml
    /// </summary>
    public partial class MovieEditorWindow : Window
    {
        private Movie _movie;
        private byte[] _img;
        public MovieEditorWindow(Movie movie)
        {
            InitializeComponent();
            if (ImgPoster!=null)
            {
                BtnImage.Visibility = Visibility.Hidden;
            }
        }

        private void BtnImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.JPG;*.PNG)|*.JPG;*.PNG";
            if (ofd.ShowDialog() == true)
            {
                try
                {
                    ImgPoster.Source = new BitmapImage(new Uri(ofd.FileName));
                    _img = File.ReadAllBytes(ofd.FileName);
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть файл", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            BtnImage.Visibility = Visibility.Hidden;

        }

        private void ImgPoster_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.JPG;*.PNG)|*.JPG;*.PNG";
            if (ofd.ShowDialog() == true)
            {
                try
                {
                    ImgPoster.Source = new BitmapImage(new Uri(ofd.FileName));
                    _img = File.ReadAllBytes(ofd.FileName);

                }
                catch
                {
                    MessageBox.Show("Невозможно открыть файл", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            BtnImage.Visibility = Visibility.Hidden;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_movie == null)
                {
                    _movie = new Movie()
                    {
                        Name = TBoxName.Text,
                        Duration = Convert.ToDouble(TBoxDuration.Text),
                        StartRentalDate = DatePickerStart.SelectedDate.Value,
                        EndRentalDate = DatePickerEnd.SelectedDate.Value,
                        Distributor = TBoxDistributor.Text,
                        Poster = _img,
                    };
                    App.Context.Movies.Add(_movie);
                    App.Context.SaveChanges();
                    
                    
                }
                else
                {
                    _movie.Name = TBoxName.Text;
                    _movie.Duration = Convert.ToDouble(TBoxDuration.Text);
                    _movie.StartRentalDate = DatePickerStart.SelectedDate.Value;
                    _movie.EndRentalDate = DatePickerEnd.SelectedDate.Value;
                    _movie.Distributor = TBoxDistributor.Text;
                    _movie.Poster = _img;

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
