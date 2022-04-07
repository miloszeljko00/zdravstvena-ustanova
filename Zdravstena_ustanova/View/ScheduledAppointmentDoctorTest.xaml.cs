using Model;
using Model.Enums;
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
using System.Windows.Shapes;

namespace Zdravstena_ustanova.View
{
    public partial class ScheduledAppointmentDoctorTest : Window
    {
        public ObservableCollection<ScheduledAppointment> ScheduledAppointments { get; set; }
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }



        public ScheduledAppointmentDoctorTest()
        {
            InitializeComponent();
            this.DataContext = this;
            var app = Application.Current as App;

            typeComboBox.ItemsSource = Enum.GetValues(typeof(AppointmentType)).Cast<AppointmentType>();
            ScheduledAppointments = new ObservableCollection<ScheduledAppointment>(app.ScheduledAppointmentController.GetAll());

            Doctors = new ObservableCollection<Doctor>(app.DoctorController.GetAll());




        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text == "" || floorTextBox.Text == "" || typeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Popuni sva polja prvo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var app = Application.Current as App;

            string roomName = nameTextBox.Text;
            int roomFloor = int.Parse(floorTextBox.Text);
            RoomType roomType = (RoomType)typeComboBox.SelectedIndex;

            var room = new Room(roomName, roomFloor, roomType);

            room = app.RoomController.Create(room);

            Rooms.Add(room);
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            var room = (Room)dataGridRooms.SelectedItem;
            if (room == null)
            {
                MessageBox.Show("Odaberi sobu!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (nameTextBox.Text == "" || floorTextBox.Text == "" || typeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Popuni sva polja prvo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var app = Application.Current as App;

            room.Name = nameTextBox.Text;
            room.Floor = int.Parse(floorTextBox.Text);
            room.RoomType = (RoomType)typeComboBox.SelectedIndex;

            app.RoomController.Update(room);

            CollectionViewSource.GetDefaultView(dataGridRooms.ItemsSource).Refresh();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            var room = (Room)dataGridRooms.SelectedItem;
            if (room == null)
            {
                MessageBox.Show("Odaberi sobu!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var app = Application.Current as App;

            app.RoomController.Delete(room.Id);
            Rooms.Remove(room);

            CollectionViewSource.GetDefaultView(dataGridRooms.ItemsSource).Refresh();
        }

        private void floorTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void dataGridRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var room = (Room)dataGridRooms.SelectedItem;

            if (room == null)
            {
                return;
            }

            typeComboBox.SelectedValue = room.RoomType;
            nameTextBox.Text = room.Name;
            floorTextBox.Text = room.Floor.ToString();
        }
    }
}
