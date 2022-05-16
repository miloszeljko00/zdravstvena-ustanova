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
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.View.Controls;

namespace zdravstvena_ustanova.View.Pages.ManagerPages
{
    /// <summary>
    /// Interaction logic for MedicationPage.xaml
    /// </summary>
    public partial class MedicationPage : Page
    {
        public MedicationPage()
        {
            InitializeComponent();
        }

        private void RequestApproval_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ActiveRequests_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FinishedRequests_Click(object sender, RoutedEventArgs e)
        {

        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTextBox.Text == "")
            {
                // Create an ImageBrush.
                ImageBrush textImageBrush = new();
                textImageBrush.ImageSource =
                    new BitmapImage(
                        new Uri(App.ProjectPath + "/Resources/img/search-name.png")
                    );
                textImageBrush.AlignmentX = AlignmentX.Left;
                textImageBrush.Stretch = Stretch.None;

                // Use the brush to paint the button's background.
                SearchTextBox.Background = textImageBrush;
            }
            else
            {

                SearchTextBox.Background = null;
            }
        }

        private void AddRoomIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Modal.Content = new AddMedication();
            MainWindow.Modal.IsOpen = true;
        }
        private void EditRoomIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (RoomsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Odaberi lek!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MainWindow.Modal.Content = new EditMedication();
            MainWindow.Modal.IsOpen = true;
        }
        private void DeleteRoomIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (RoomsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Odaberi lek!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MainWindow.Modal.Content = new DeleteMedication();
            MainWindow.Modal.IsOpen = true;
        }

        private void IngredientsIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var calendarIcon = (Image)e.OriginalSource;
            var dataContext = calendarIcon.DataContext;
            var dataSource = (Medication)dataContext;
            long roomId = dataSource.Id;

            NavigationService.Navigate(new RoomCalendarOverview(roomId));
        }
    }
}
