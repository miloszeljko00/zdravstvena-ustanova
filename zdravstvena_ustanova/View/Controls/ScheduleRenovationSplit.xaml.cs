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
    /// Interaction logic for ScheduleRenovationSplit.xaml
    /// </summary>
    public partial class ScheduleRenovationSplit : UserControl
    {
        public ScheduleRenovation ScheduleRenovation { get; set; }
        public ObservableCollection<RoomType> RoomTypes { get; set; }

        public ScheduleRenovationSplit(ScheduleRenovation scheduleRenovation)
        {
            InitializeComponent();
            DataContext = this;
            ScheduleRenovation = scheduleRenovation;

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


            string firstRoomName = firstRoomNameTextBox.Text;
            int firstRoomFloor = int.Parse(firstRoomFloorTextBox.Text);
            RoomType firstRoomType = (RoomType)firstRoomTypeComboBox.SelectedItem;
            Room firstRoom = new Room(firstRoomName, firstRoomFloor, firstRoomType);

            string secondRoomName = secondRoomNameTextBox.Text;
            int secondRoomFloor = int.Parse(secondRoomFloorTextBox.Text);
            RoomType secondRoomType = (RoomType)secondRoomTypeComboBox.SelectedItem;
            Room secondRoom = new Room(secondRoomName, secondRoomFloor, secondRoomType);

            DateRange dateRange = ScheduleRenovation.DateRange;

            RenovationAppointment renovationAppointment;

            if ((bool)FirstRoomRadioButton.IsChecked)
            {
                renovationAppointment  = new RenovationAppointment(
                        ScheduleRenovation.Room, firstRoom, secondRoom, dateRange.Start, dateRange.End,
                        ScheduleRenovation.Description, ScheduleRenovation.RenovationType);
            }
            else
            {
                renovationAppointment = new RenovationAppointment(
                        ScheduleRenovation.Room, secondRoom, firstRoom, dateRange.Start, dateRange.End,
                        ScheduleRenovation.Description, ScheduleRenovation.RenovationType);
            }

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
