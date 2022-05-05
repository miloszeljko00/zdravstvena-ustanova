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

namespace zdravstvena_ustanova.View.Pages.ManagerPages
{
    /// <summary>
    /// Interaction logic for ManagerHome.xaml
    /// </summary>
    public partial class ManagerMainPage : Page
    {
        public LoginPage LoginPage { get; set; }
        public ManagerMainPage(LoginPage loginPage)
        {
            InitializeComponent();
            LoginPage = loginPage;
            AccountsDropDown.Visibility = Visibility.Hidden;
            ReportsDropDown.Visibility = Visibility.Hidden;
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

        private void warehouseButton_Click(object sender, RoutedEventArgs e)
        {
            homeButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            reportsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            warehouseButton.Background = new SolidColorBrush(Color.FromRgb(97, 164, 188));
            roomsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            itemsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            drugsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            pollsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            accountButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));

            ManagerMain.Content = new WarehouseInventoryOverviewPage();
        }

        private void ItemTransferReportsButton_Click(object sender, RoutedEventArgs e)
        {
            
            ReportsDropDown.Visibility = Visibility.Hidden;

            homeButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            reportsButton.Background = new SolidColorBrush(Color.FromRgb(97, 164, 188));
            warehouseButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            roomsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            itemsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            drugsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            pollsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            accountButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));

            ManagerMain.Content = new ItemTransferReportsPage();
        }

        private void RenovationReportsButton_Click(object sender, RoutedEventArgs e)
        {
            ReportsDropDown.Visibility = Visibility.Hidden;

            homeButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            reportsButton.Background = new SolidColorBrush(Color.FromRgb(97, 164, 188));
            warehouseButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            roomsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            itemsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            drugsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            pollsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            accountButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));

            ManagerMain.Content = new RenovationReportsPage();
        }

        private void reportsButton_LostFocus(object sender, RoutedEventArgs e)
        {
            ReportsDropDown.Visibility = Visibility.Hidden;
        }

        private void reportsButton_GotFocus(object sender, RoutedEventArgs e)
        {
            ReportsDropDown.Visibility = Visibility.Visible;
        }

        private void accountButton_GotFocus(object sender, RoutedEventArgs e)
        {
            AccountsDropDown.Visibility = Visibility.Visible;
        }

        private void accountButton_LostFocus(object sender, RoutedEventArgs e)
        {
            AccountsDropDown.Visibility = Visibility.Hidden;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            app.LoggedInUser = null;
            NavigationService.Navigate(LoginPage);
        }
    }
}
