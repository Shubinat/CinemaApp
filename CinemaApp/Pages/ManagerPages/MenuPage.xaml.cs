﻿using System;
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
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var now = DateTime.Now;
            string time;
            if (now.Hour >= 6 && now.Hour < 11)
                time = "Доброе утро";
            else if (now.Hour >= 11 && now.Hour < 17)
                time = "Добрый день";
            else if (now.Hour >= 17 && now.Hour < 21)
                time = "Добрый вечер";
            else
                time = "Доброй ночи";

            TBlockHello.Text = time + ", " + App.AuthUser.FullName + "!";
        }

        private void BtnMovies_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MoviesPage());
        }

        private void BtnSessions_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SessionsPage());
        }

        private void BtnTickets_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TicketsPage());
        }
    }
}
