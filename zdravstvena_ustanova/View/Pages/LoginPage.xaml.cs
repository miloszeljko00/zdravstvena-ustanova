﻿using Model;
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
using zdravstvena_ustanova.View.Pages.ManagerPages;
using zdravstvena_ustanova.View.Pages.SecretaryPages;

namespace zdravstvena_ustanova.View.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            if (username == null || password == null) return;

            var app = Application.Current as App;
            var user = app.AccountController.Login(username, password);

            if (user == null) return;

            app.LoggedInUser = user;

            if (app.LoggedInUser is Manager) NavigationService.Navigate(new ManagerMainPage());
            if (app.LoggedInUser is Secretary) {NavigationService.Navigate(new HomePagePatients()); }  
            if (app.LoggedInUser is Doctor) { }     // TODO redirect on Doctor homescreen
            if (app.LoggedInUser is Patient) { }    // TODO redirect on Patient homescreen

        }
    }
}