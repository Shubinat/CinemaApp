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
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        private User selectedUser => DGridUsers.SelectedItem as User;
        private Role selectedRole => CBoxRoles.SelectedItem as Role;
        public UsersPage()
        {
            InitializeComponent();
            List<Role> roles = App.Context.Roles.ToList();
            roles.Insert(0, new Role() { Name = "Все роли" });
            CBoxRoles.ItemsSource = roles;
            CBoxRoles.SelectedIndex = 0;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Window editor = new Windows.UserEditorWindow(selectedUser);
            if (editor.ShowDialog() == true)
            {
                UpdateGrid();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
           if(MessageBox.Show("Вы действительно хотите удалить данного пользователя?", 
                "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                App.Context.Users.Remove(selectedUser);
                App.Context.SaveChanges();
                UpdateGrid();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Window editor = new Windows.UserEditorWindow(null);
            if (editor.ShowDialog() == true)
            {
                UpdateGrid();
            }
        }

        private void DGridUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var enabled = selectedUser != null;
            BtnEdit.IsEnabled = enabled;
            BtnDelete.IsEnabled = enabled && selectedUser.ID != App.AuthUser.ID;
        }

        private void UpdateGrid()
        {
            List<User> users = App.Context.Users.ToList();
            if (CBoxRoles.SelectedIndex != 0)
            {
                users = users.Where(x => x.Role.ID == selectedRole.ID).ToList();
            }
            if (!string.IsNullOrWhiteSpace(TBoxSearch.Text))
            {
                users = users.Where(x => x.FullName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            }

            if(users.Count > 0)
            {
                GridEmpty.Visibility = Visibility.Collapsed;
                DGridUsers.Visibility = Visibility.Visible;
                DGridUsers.ItemsSource = users;
            }
            else
            {
                GridEmpty.Visibility = Visibility.Visible;
                DGridUsers.Visibility = Visibility.Collapsed;
            }
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateGrid();
        }

        private void CBoxRoles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGrid();
        }
    }
}
