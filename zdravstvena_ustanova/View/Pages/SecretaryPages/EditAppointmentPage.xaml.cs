using zdravstvena_ustanova.Model;
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
using zdravstvena_ustanova.Model.Enums;

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

        private bool isUnscheduled;

        public EditAppointmentPage(ScheduledAppointment sa, HomePagePatients hpp, bool isUnscheduled)
        {
            InitializeComponent();
            _scheduledAppointment = sa;
            _homePagePatients = hpp;
            this.isUnscheduled = isUnscheduled;
            var app = Application.Current as App;
            string[] times = {"08:00", "09:00", "10:00", "11:00", "12:00",
                                "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00" };

            Times = new ObservableCollection<string>(times);
            Rooms = new ObservableCollection<Room>(app.RoomController.GetAll());
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
            timeCB.ItemsSource = app.ScheduledAppointmentController.GetAppropriateTimes(_scheduledAppointment.Start.Date, _scheduledAppointment.Doctor.Id, _scheduledAppointment.Patient.Id, _scheduledAppointment.Room.Id, (int)_scheduledAppointment.Doctor.Shift + 1);
        }

        public void dateDP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if((DateTime)dateDP.SelectedDate < DateTime.Today)
            {
                validate.Text = "Ne možete pomeriti termin u prošlost!";
            }
            else
            {
                validate.Text = "";
            }
            var app = Application.Current as App;
            var r = roomCB.SelectedItem as Room;
            long roomId = -1;
            if (r != null)
                roomId = r.Id;
            List<string> times = app.ScheduledAppointmentController.GetAppropriateTimes((DateTime)dateDP.SelectedDate, _scheduledAppointment.Doctor.Id, _scheduledAppointment.Patient.Id, roomId, (int)_scheduledAppointment.Doctor.Shift + 1);
            timeCB.ItemsSource = times;
        }

        private void roomCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var app = Application.Current as App;
            var r = roomCB.SelectedItem as Room;
            long roomId = -1;
            if (r != null)
                roomId = r.Id;
            List<string> times = app.ScheduledAppointmentController.GetAppropriateTimes((DateTime)dateDP.SelectedDate, _scheduledAppointment.Doctor.Id, _scheduledAppointment.Patient.Id, roomId, (int)_scheduledAppointment.Doctor.Shift + 1);
            timeCB.ItemsSource = times;
        }

        private void Save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DateTime date = (DateTime)dateDP.SelectedDate;
            string[] time = timeCB.SelectedItem.ToString().Split(":");
            int hour = int.Parse(time[0]);
            int minute = int.Parse(time[1]);
            var app = Application.Current as App;
            if(isUnscheduled)
            {
                _scheduledAppointment.Start = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
                _scheduledAppointment.End = new DateTime(date.Year, date.Month, date.Day, hour + 1, minute, 0);
                _scheduledAppointment.Room.Id = (long)roomCB.SelectedValue;
                app.ScheduledAppointmentController.Create(_scheduledAppointment);
                app.UnScheduledAppointmentController.Delete(_scheduledAppointment.Id);
                _homePagePatients.SecretaryFrame.Content = new UnscheduledAppointmentsPage(_homePagePatients);
                return;
            }

            _scheduledAppointment.Start = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            _scheduledAppointment.End = new DateTime(date.Year, date.Month, date.Day, hour + 1, minute, 0);
            _scheduledAppointment.Room.Id = (long)roomCB.SelectedValue;
            app.ScheduledAppointmentController.Update(_scheduledAppointment);
            _homePagePatients.SecretaryFrame.Content = new SecretaryAppointmentPage(_homePagePatients);
            
        }

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(!isUnscheduled)
            {
                _homePagePatients.SecretaryFrame.Content = new SecretaryAppointmentPage(_homePagePatients);
                return;
            }
            _homePagePatients.SecretaryFrame.Content = new UnscheduledAppointmentsPage(_homePagePatients);
        }
    }
}
