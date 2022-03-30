using Microsoft.Win32;
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
    /// Interaction logic for MovieEditorWindow.xaml
    /// </summary>
    public partial class MovieEditorWindow : Window
    {
        public MovieEditorWindow()
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

        }
    }
}
