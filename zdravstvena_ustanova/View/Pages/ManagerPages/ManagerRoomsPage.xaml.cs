using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using zdravstvena_ustanova.View.Controls;

namespace zdravstvena_ustanova.View.Pages.ManagerPages
{
    public partial class ManagerRoomsPage : Page
    {
        public ObservableCollection<Room> Rooms { get; set; }

        public ManagerRoomsPage()
        {
            InitializeComponent(); 
            DataContext = this;

            var app = Application.Current as App;
           
            Rooms = new ObservableCollection<Room>(app.RoomController.GetAll());
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

        private void InventoryOverviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsRoomSelected()) return;
            var room = GetSelectedRoom();
            NavigationService.Navigate(new InventoryOverviewPage(room));
        }

        public bool IsRoomSelected()
        {
            if (RoomsDataGrid.SelectedItem == null) return false;
            return true;
        }
        public Room GetSelectedRoom()
        {
            return (Room)RoomsDataGrid.SelectedItem;
        }
        private void AddRoomIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Modal.Content = new AddRoomControl(Rooms);
            MainWindow.Modal.IsOpen = true;
        }
        private void EditRoomIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(RoomsDataGrid.SelectedItem == null) return;

            MainWindow.Modal.Content = new EditRoomControl(Rooms, RoomsDataGrid);
            MainWindow.Modal.IsOpen = true;
        }
        private void DeleteRoomIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (RoomsDataGrid.SelectedItem == null) return;

            MainWindow.Modal.Content = new DeleteRoomControl(Rooms, RoomsDataGrid);
            MainWindow.Modal.IsOpen = true;
        }

        private void CalendarIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {

            NavigationService.Navigate(new RoomCalendarOverview());
        }
    }
}
