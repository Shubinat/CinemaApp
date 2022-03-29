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

namespace CinemaApp.UserControls
{
    /// <summary>
    /// Логика взаимодействия для PasswordBoxWithMask.xaml
    /// </summary>
    public partial class PasswordBoxWithMask : UserControl
    {
        private bool _isPassword = false;
        public bool IsPassword
        {
            get
            {
                return _isPassword;
            }
            set
            {
                _isPassword = value;
                ChangeMode();
            }
        }

        public string Text { get; set; }

        private void ChangeMode()
        {
            if (_isPassword)
            {
                TBox.Visibility = Visibility.Collapsed;
                PBox.Visibility = Visibility.Visible;
                PBox.Focus();
                string text = TBox.Text;
                PBox.Password = text;
            }
            else
            {
                TBox.Visibility = Visibility.Visible;
                TBox.Focus();
                PBox.Visibility = Visibility.Collapsed;
                string text = PBox.Password;
                TBox.Text = text;
            }
            
        }

        public PasswordBoxWithMask()
        {
            InitializeComponent();
            IsPassword = true;
        }

        private void TBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = TBox.Text;
        }

        private void PBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Text = PBox.Password;
        }
    }
}
