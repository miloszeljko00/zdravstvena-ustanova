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

namespace zdravstvena_ustanova.View.Pages.ManagerPages
{
    /// <summary>
    /// Interaction logic for ManagerHome.xaml
    /// </summary>
    public partial class ManagerMainPage : Page
    {
        public ManagerMainPage()
        {
            InitializeComponent();
        }


        private void roomsButton_Click(object sender, RoutedEventArgs e)
        {
            homeButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            reportsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            warehouseButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            roomsButton.Background = new SolidColorBrush(Color.FromRgb(97, 164, 188));
            itemsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            drugsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            pollsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            accountButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));


            ManagerMain.Content = new ManagerRoomsPage();
        }

        private void itemsButton_Click(object sender, RoutedEventArgs e)
        {
            homeButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            reportsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            warehouseButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            roomsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            itemsButton.Background = new SolidColorBrush(Color.FromRgb(97, 164, 188));
            drugsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            pollsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            accountButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));

            ManagerMain.Content = new ManagerItemsPage();
        }
    }
}