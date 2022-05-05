using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace zdravstvena_ustanova.View.Controls
{
    /// <summary>
    /// Interaction logic for DeleteRoomControl.xaml
    /// </summary>
    public partial class DeleteRoomControl : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<RoomType> RoomTypes { get; set; }
        public ObservableCollection<Room> Rooms;
        public DataGrid RoomsDataGrid { get; set; }

        #region NotifyProperties
        private string _roomName;
        public string RoomName
        {
            get
            {
                return _roomName;
            }
            set
            {
                if (value != _roomName)
                {
                    _roomName = value;
                    OnPropertyChanged("RoomName");
                }
            }
        }
        private RoomType _roomType;
        public RoomType RoomType
        {
            get
            {
                return _roomType;
            }
            set
            {
                if (value != _roomType)
                {
                    _roomType = value;
                    OnPropertyChanged("RoomType");
                }
            }
        }
        private int _roomFloor;
        public int RoomFloor
        {
            get
            {
                return _roomFloor;
            }
            set
            {
                if (value != _roomFloor)
                {
                    _roomFloor = value;
                    OnPropertyChanged("RoomFloor");
                }
            }
        }
        #endregion
        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public DeleteRoomControl(ObservableCollection<Room> rooms, DataGrid roomsDataGrid)
        {
            InitializeComponent(); 
            DataContext = this;
            Rooms = rooms;
            RoomsDataGrid = roomsDataGrid;
            var roomTypes = Enum.GetValues(typeof(RoomType)).Cast<RoomType>();
            RoomTypes = new ObservableCollection<RoomType>();


            foreach (var roomType in roomTypes)
            {
                RoomTypes.Add(roomType);
            }

            var selectedRoom = (Room)RoomsDataGrid.SelectedItem;

            RoomName = selectedRoom.Name;
            RoomType = selectedRoom.RoomType;
            RoomFloor = selectedRoom.Floor;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var room = (Room)RoomsDataGrid.SelectedItem;
            if (room == null)
            {
                MessageBox.Show("Odaberi sobu!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var app = Application.Current as App;

            app.RoomController.Delete(room.Id);
            Rooms.Remove(room);

            CollectionViewSource.GetDefaultView(RoomsDataGrid.ItemsSource).Refresh();

            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }
    }
}
