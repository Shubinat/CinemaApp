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

namespace CinemaApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void BtnChangePasswordMode_Click(object sender, RoutedEventArgs e)
        {
            TBoxPassword.IsPassword = !TBoxPassword.IsPassword;
            if (TBoxPassword.IsPassword)
            {
                TBlockIndicator.Visibility = Visibility.Collapsed;
            }
            else
            {
                TBlockIndicator.Visibility = Visibility.Visible;
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(TBoxLogin.Text) && !string.IsNullOrWhiteSpace(TBoxPassword.Text))
            {
                if (App.Context.Users.FirstOrDefault(x => x.Login == TBoxLogin.Text && x.Password == TBoxPassword.Text) is User user)
                {
                    switch (user.RoleID)
                    {
                        case 1:
                            NavigationService.Navigate(new AdminPages.MenuPage());
                            break;
                        case 2:
                            NavigationService.Navigate(new ManagerPages.MenuPage());
                            break;
                        case 3:
                            NavigationService.Navigate(new AccountantPages.MenuPage());
                            break;
                        default:
                            _ = MessageBox.Show("Для данной роли не предумотрен функционал.", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                    }
                    App.AuthUser = user;
                }
                else
                {
                    _ = MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                _ = MessageBox.Show("Заполните оба поля.", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TBoxPassword.PBox.Password = "";
            TBoxPassword.TBox.Text = "";
        }
    }
}
