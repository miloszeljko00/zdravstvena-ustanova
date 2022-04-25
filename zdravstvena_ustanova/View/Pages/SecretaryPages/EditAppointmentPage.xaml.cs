using Model;
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
using Model.Enums;

namespace zdravstvena_ustanova.View.Pages.SecretaryPages
{
    /// <summary>
    /// Interaction logic for EditAppointmentPage.xaml
    /// </summary>
    public partial class EditAppointmentPage : Page
    {
        private ScheduledAppointment _scheduledAppointment;

        private HomePagePatients _homePagePatients;

        public ObservableCollection<Room> Rooms { get; set; }

        public ObservableCollection<string> Times { get; set; }

        public ICollectionView RoomView { get; set; }

        public ICollectionView TimeView { get; set; }

        public EditAppointmentPage(ScheduledAppointment sa, HomePagePatients hpp)
        {
            InitializeComponent();
            _scheduledAppointment = sa;
            _homePagePatients = hpp;
            var app = Application.Current as App;
            string[] times = {"08:00", "09:00", "10:00", "11:00", "12:00",
                                "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00" };

            Times = new ObservableCollection<string>(times);
            Rooms = new ObservableCollection<Room>(app.RoomController.GetAll());
            roomCB.ItemsSource = Rooms;
            typeCB.Text = _scheduledAppointment.AppointmentType.ToString();

            patientTB.Text = _scheduledAppointment.Patient.Name + " " + _scheduledAppointment.Patient.Surname;
            doctorTB.Text = _scheduledAppointment.Doctor.Name + " " + _scheduledAppointment.Doctor.Surname;
            dateDP.SelectedDate = sa.Start;
            RoomView = new CollectionViewSource { Source = Rooms}.View;
            TimeView = new CollectionViewSource { Source = times}.View;
            roomCB.ItemsSource = Rooms;
            RoomView.Filter = r =>
            {
                Room room = r as Room;
                if (_scheduledAppointment.AppointmentType == AppointmentType.LABORATORY_APPOINTMENT)
                    if (room.RoomType == RoomType.LABORATORY)
                        return true;
                if(_scheduledAppointment.AppointmentType == AppointmentType.REGULAR_APPOINTMENT || _scheduledAppointment.AppointmentType == AppointmentType.SPECIALIST_APPOINTMENT)
                    if (room.RoomType == RoomType.REGULAR_APPOINTMENT_ROOM)
                        return true;
                if(_scheduledAppointment.AppointmentType == AppointmentType.OPERATION_APPOINTMENT)
                    if (room.RoomType == RoomType.OPERATING_ROOM)
                        return true;
                return false;
                //TODO dodati hitan slucaj
            };
            TimeView.Filter = s =>
            {
                if (s.Equals("-1"))
                    return false;
                return true;
            };
            roomCB.ItemsSource = RoomView;
            roomCB.SelectedValue = _scheduledAppointment.Room.Id;
            timeCB.ItemsSource = TimeView;
        }

        public void dateDP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var app = Application.Current as App;
            var r = roomCB.SelectedItem as Room;
            List<string> times = app.ScheduledAppointmentController.GetAppropriateTimes((DateTime)dateDP.SelectedDate, _scheduledAppointment.Doctor, _scheduledAppointment.Patient, r);
            timeCB.ItemsSource = times;
        }

        private void roomCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var app = Application.Current as App;
            var r = roomCB.SelectedItem as Room;
            List<string> times = app.ScheduledAppointmentController.GetAppropriateTimes((DateTime)dateDP.SelectedDate, _scheduledAppointment.Doctor, _scheduledAppointment.Patient, r);
            timeCB.ItemsSource = times;
        }

        private void Save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            DateTime date = (DateTime)dateDP.SelectedDate;
            string[] time = timeCB.SelectedItem.ToString().Split(":");
            int hour = int.Parse(time[0]);
            int minute = int.Parse(time[1]);

            _scheduledAppointment.Start = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            _scheduledAppointment.End = new DateTime(date.Year, date.Month, date.Day, hour + 1, minute, 0);
            _scheduledAppointment.Room.Id = (long)roomCB.SelectedValue;
            var app = Application.Current as App;
            app.ScheduledAppointmentController.Update(_scheduledAppointment);
            _homePagePatients.SecretaryFrame.Content = new SecretaryAppointmentPage(_homePagePatients);
            
        }

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _homePagePatients.SecretaryFrame.Content = new SecretaryAppointmentPage(_homePagePatients);
        }
    }
}
