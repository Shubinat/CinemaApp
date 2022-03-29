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

namespace CinemaApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FrameMain.Navigate(new Pages.AuthPage());
            
        }

        private void BtnNavBack_Click(object sender, RoutedEventArgs e)
        {
            if(FrameMain.CanGoBack)
                FrameMain.GoBack();
        }

        private void FrameMain_ContentRendered(object sender, EventArgs e)
        {
            if(FrameMain.Content is Pages.AuthPage)
                BtnNavBack.Visibility = Visibility.Collapsed;
            else
                BtnNavBack.Visibility = Visibility.Visible;

            Title = "СИНЕМА 5 - " + (FrameMain.Content as Page).Title; 
        }
    }
}
