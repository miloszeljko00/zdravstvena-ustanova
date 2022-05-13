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
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;

namespace zdravstvena_ustanova.View.Controls
{
    /// <summary>
    /// Interaction logic for ScheduleRenovationMerge.xaml
    /// </summary>
    public partial class ScheduleRenovationMerge : UserControl
    {
        public ScheduleRenovation ScheduleRenovation { get; set; }
        public ObservableCollection<RoomType> RoomTypes { get; set; }

        public ScheduleRenovationMerge(ScheduleRenovation scheduleRenovation)
        {
            InitializeComponent();
            DataContext = this;
            ScheduleRenovation = scheduleRenovation;
            var app = Application.Current as App;

            List<Room> rooms = (List<Room>)app.RoomController.GetAll();
            Room currentRoom = ScheduleRenovation.Room;

            rooms = rooms.FindAll(room => room.Id != currentRoom.Id);

            MergingRoomComboBox.ItemsSource = rooms;

            var roomTypes = Enum.GetValues(typeof(RoomType)).Cast<RoomType>();
            RoomTypes = new ObservableCollection<RoomType>();

            foreach (var roomType in roomTypes)
            {
                RoomTypes.Add(roomType);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;

            Room MergingRoom = (Room)MergingRoomComboBox.SelectedItem;

            if(app.RenovationAppointmentController.HasRenovationFromTo(MergingRoom,
                ScheduleRenovation.DateRange.Start, DateTime.MaxValue))
            {
                MessageBoxResult answer = MessageBox.Show("Prostorija koju spajate ima zakazano renoviranje nakon datuma spajanja!",
                                                "Renoviranje postoji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string finalRoomName = RoomNameTextBox.Text;
            int finalRoomFloor = int.Parse(RoomFloorTextBox.Text);
            RoomType roomType = (RoomType)RoomTypeComboBox.SelectedItem;
            Room finalRoom = new Room(finalRoomName, finalRoomFloor, roomType);
            DateRange dateRange = ScheduleRenovation.DateRange;


            RenovationAppointment renovationAppointment = new RenovationAppointment(ScheduleRenovation.Room,
                MergingRoom, finalRoom, dateRange.Start, dateRange.End, 
                ScheduleRenovation.Description, ScheduleRenovation.RenovationType);

            app.RenovationAppointmentController.ScheduleRenovation(renovationAppointment);
            ScheduleRenovation.RoomsCalendarControl.DisplayCalendarForMonth(ScheduleRenovation.RoomsCalendarControl.DisplayedMonth);

            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Modal.Content = ScheduleRenovation;
        }
        private void floorTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
