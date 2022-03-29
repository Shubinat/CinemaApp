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
using System.Windows.Shapes;

namespace CinemaApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для UserEditor.xaml
    /// </summary>
    public partial class UserEditorWindow : Window
    {
        private User _user;
        public UserEditorWindow(User user)
        {
            InitializeComponent();
            CBoxRoles.ItemsSource = App.Context.Roles.ToList();
            if(user != null)
            {
                Title = "Редактирование пользователя";
                TBoxName.Text = user.Name;
                TBoxSurname.Text = user.Surname;
                TBoxPatronymic.Text = user.Patronymic;
                TBoxLogin.Text = user.Login;
                TBoxPassword.Text = user.Password;
                CBoxRoles.SelectedItem = user.Role;
                if (user.ID == App.AuthUser.ID)
                    CBoxRoles.IsEnabled = false;
            }
            _user = user;
            
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_user == null)
                {
                    _user = new User()
                    {
                        Name = TBoxName.Text,
                        Surname = TBoxSurname.Text,
                        Patronymic = TBoxPatronymic.Text,
                        Login = TBoxLogin.Text,
                        Password = TBoxPassword.Text,
                        Role = CBoxRoles.SelectedItem as Role,
                    };
                    App.Context.Users.Add(_user);
                }
                else
                {
                    _user.Login = TBoxLogin.Text;
                    _user.Password = TBoxPassword.Text;
                    _user.Surname = TBoxSurname.Text;
                    _user.Name = TBoxName.Text;
                    _user.Patronymic = TBoxPatronymic.Text;
                    _user.Role = CBoxRoles.SelectedItem as Role;
                }
                App.Context.SaveChanges();
                DialogResult = true;
                Close();
            }
            catch
            {
                _ =MessageBox.Show("Произошла ошибка. Проаерьте правильность заполнения полей.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
