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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;

namespace zdravstvena_ustanova.View.Pages.ManagerPages
{
    /// <summary>
    /// Interaction logic for ManagerHome.xaml
    /// </summary>
    public partial class ManagerMainPage : Page
    {
        public LoginPage LoginPage { get; set; }
        private readonly Duration _dropdownAnimationDuration;
        public ManagerMainPage(LoginPage loginPage)
        {
            InitializeComponent();
            LoginPage = loginPage;
            _dropdownAnimationDuration = new Duration(TimeSpan.FromSeconds(0.2));
        }


        private void roomsButton_Click(object sender, RoutedEventArgs e)
        {
            /*homeButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            reportsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            warehouseButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            roomsButton.Background = new SolidColorBrush(Color.FromRgb(97, 164, 188));
            itemsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            drugsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            pollsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            accountButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            */


            ManagerMain.Content = new ManagerRoomsPage();
        }

        private void itemsButton_Click(object sender, RoutedEventArgs e)
        {
            /*homeButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            reportsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            warehouseButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            roomsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            itemsButton.Background = new SolidColorBrush(Color.FromRgb(97, 164, 188));
            drugsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            pollsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            accountButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            */

            ManagerMain.Content = new ManagerItemsPage();
        }

        private void warehouseButton_Click(object sender, RoutedEventArgs e)
        {
            /*
            homeButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            reportsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            warehouseButton.Background = new SolidColorBrush(Color.FromRgb(97, 164, 188));
            roomsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            itemsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            drugsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            pollsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            accountButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            */

            ManagerMain.Content = new WarehouseInventoryOverviewPage();
        }

        private void ItemTransferReportsButton_Click(object sender, RoutedEventArgs e)
        {
            /*
            homeButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            reportsButton.Background = new SolidColorBrush(Color.FromRgb(97, 164, 188));
            warehouseButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            roomsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            itemsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            drugsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            pollsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            accountButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            */

            ManagerMain.Content = new ItemTransferReportsPage();
        }

        private void RenovationReportsButton_Click(object sender, RoutedEventArgs e)
        {
            /*
            homeButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            reportsButton.Background = new SolidColorBrush(Color.FromRgb(97, 164, 188));
            warehouseButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            roomsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            itemsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            drugsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            pollsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            accountButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            */

            ManagerMain.Content = new RenovationReportsPage();
        }

        private void reportsButton_LostFocus(object sender, RoutedEventArgs e)
        {
            DoubleAnimation heightAnimation = new DoubleAnimation(0, _dropdownAnimationDuration)
            {
                AccelerationRatio = 0.2
            };
            ReportsDropDown.BeginAnimation(HeightProperty, heightAnimation);
        }

        private void reportsButton_GotFocus(object sender, RoutedEventArgs e)
        {
            DoubleAnimation heightAnimation = new DoubleAnimation(320, _dropdownAnimationDuration)
            {
                AccelerationRatio = 0.2
            };
            ReportsDropDown.BeginAnimation(HeightProperty, heightAnimation);
        }

        private void accountButton_GotFocus(object sender, RoutedEventArgs e)
        {
            DoubleAnimation heightAnimation = new DoubleAnimation(320, _dropdownAnimationDuration)
            {
                AccelerationRatio = 0.2
            };
            AccountsDropDown.BeginAnimation(HeightProperty, heightAnimation);
        }

        private void accountButton_LostFocus(object sender, RoutedEventArgs e)
        {
            DoubleAnimation heightAnimation = new DoubleAnimation(0, _dropdownAnimationDuration)
            {
                AccelerationRatio = 0.2
            };
            AccountsDropDown.BeginAnimation(HeightProperty, heightAnimation);

        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            app.LoggedInUser = null;
            LoginPage.Mw.WindowStyle = WindowStyle.SingleBorderWindow;
            LoginPage.Mw.ResizeMode = ResizeMode.CanResize;
            NavigationService.Navigate(LoginPage);
        }

        private void drugsButton_Click(object sender, RoutedEventArgs e)
        {
            /*
            homeButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            reportsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            warehouseButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            roomsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            itemsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            drugsButton.Background = new SolidColorBrush(Color.FromRgb(97, 164, 188));
            pollsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            accountButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            */

            ManagerMain.Content = new MedicationPage();
        }

        private void pollsButton_Click(object sender, RoutedEventArgs e)
        {
            /*
            homeButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            reportsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            warehouseButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            roomsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            itemsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            drugsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            pollsButton.Background = new SolidColorBrush(Color.FromRgb(97, 164, 188));
            accountButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            */

            ManagerMain.Content = new PollsOverviewPage();
        }

        private void RoomReportsButton_OnClick(object sender, RoutedEventArgs e)
        {
            /*
            homeButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            reportsButton.Background = new SolidColorBrush(Color.FromRgb(97, 164, 188));
            warehouseButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            roomsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            itemsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            drugsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            pollsButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            accountButton.Background = new SolidColorBrush(Color.FromRgb(247, 226, 226));
            */

            ManagerMain.Content = new RoomsOccupancyPage();
        }

        private void DarkModeButton_OnClick(object sender, RoutedEventArgs e)
        {

            var paletteHelper = new PaletteHelper();
            //Retrieve the app's existing theme
            ITheme theme = paletteHelper.GetTheme();

            if (theme.GetBaseTheme() == BaseTheme.Dark)
            {
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                theme.SetBaseTheme(Theme.Dark);
            }

            paletteHelper.SetTheme(theme);
        }
    }
}
