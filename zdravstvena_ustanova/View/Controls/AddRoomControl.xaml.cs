using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace zdravstvena_ustanova.View.Controls
{
    public partial class AddRoomControl : UserControl
    {
        public ObservableCollection<RoomType> RoomTypes { get; set; }
        public ObservableCollection<Room> Rooms;

        public AddRoomControl(ObservableCollection<Room> rooms)
        {
            InitializeComponent();
            DataContext = this;
            Rooms = rooms;
            var roomTypes = Enum.GetValues(typeof(RoomType)).Cast<RoomType>();
            RoomTypes = new ObservableCollection<RoomType>();

            foreach (var roomType in roomTypes)
            {
                RoomTypes.Add(roomType);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (roomNameTextBox.Text == "" || roomFloorTextBox.Text == "" || roomTypeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Popuni sva polja prvo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var app = Application.Current as App;

            string roomName = roomNameTextBox.Text;
            RoomType roomType = (RoomType)roomTypeComboBox.SelectedIndex;
            int roomFloor = int.Parse(roomFloorTextBox.Text);

            var room = new Room(roomName, roomFloor, roomType);

            room = app.RoomController.Create(room);

            Rooms.Add(room);
            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }
        private void floorTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
