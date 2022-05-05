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
    /// Interaction logic for NewAppointment.xaml
    /// </summary>
    public partial class NewAppointment : Page
    {
        private ScheduledAppointment _scheduledAppointment;

        private HomePagePatients _homePagePatients;

        public ObservableCollection<Room> Rooms { get; set; }

        public ObservableCollection<string> Times { get; set; }

        public ObservableCollection<Doctor> Doctors { get; set; }

        public ObservableCollection<Patient> Patients { get; set; }

        private bool guest;

        public ICollectionView RoomView { get; set; }
        public NewAppointment(HomePagePatients hpp)
        {
            InitializeComponent();
            DataContext = this;
            _homePagePatients = hpp;
            guest = false;
            var app = Application.Current as App;
            Rooms = new ObservableCollection<Room>(app.RoomController.GetAll());
            Doctors = new ObservableCollection<Doctor>(app.DoctorController.GetAll());
            Patients = new ObservableCollection<Patient>(app.PatientController.GetAll());
            RoomView = new CollectionViewSource { Source = Rooms }.View;
            typeCB.ItemsSource = Enum.GetValues(typeof(AppointmentType)).Cast<AppointmentType>();
            RoomView.Filter = r =>
            {
                if (typeCB.SelectedIndex == -1)
                    return true;
                Room room = r as Room;
                if ((AppointmentType)typeCB.SelectedItem == AppointmentType.LABORATORY_APPOINTMENT)
                    if (room.RoomType == RoomType.LABORATORY)
                        return true;
                if ((AppointmentType)typeCB.SelectedItem == AppointmentType.REGULAR_APPOINTMENT || (AppointmentType)typeCB.SelectedItem == AppointmentType.SPECIALIST_APPOINTMENT)
                    if (room.RoomType == RoomType.REGULAR_APPOINTMENT_ROOM)
                        return true;
                if ((AppointmentType)typeCB.SelectedItem == AppointmentType.OPERATION_APPOINTMENT)
                    if (room.RoomType == RoomType.OPERATING_ROOM)
                        return true;
                return false;
                //TODO dodati hitan slucaj
            };
            dateDP.SelectedDate = DateTime.Today;
            roomCB.ItemsSource = RoomView;
            roomCB.SelectedItem = null;
            doctorCB.ItemsSource = Doctors;
            patientCB.ItemsSource = Patients;
            //roomCB.ItemsSource = Rooms;
            
            

        }

        private void Guest_Checked(object sender, RoutedEventArgs e)
        {
            if(Guest.IsChecked == true)
            {
                guestNameTB.IsEnabled = true;
                guestSurnameTB.IsEnabled = true;
                username.IsEnabled = true;
                password.IsEnabled = true;
                nameLabel.IsEnabled = true;
                usernameLabel.IsEnabled = true;
                passwordLabel.IsEnabled = true;

                patientLabel.IsEnabled = false;
                patientCB.IsEnabled = false;
                patientCB.SelectedIndex = -1;
                guest = true;
            }
            else
            {
                guestNameTB.IsEnabled = false;
                guestSurnameTB.IsEnabled = false;
                username.IsEnabled = false;
                password.IsEnabled = false;
                nameLabel.IsEnabled = false;
                usernameLabel.IsEnabled = false;
                passwordLabel.IsEnabled = false;

                patientLabel.IsEnabled = true;
                patientCB.IsEnabled = true;
                guest = false;

            }
        }

        private void typeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            valiDate.Text = "";
            RoomView.Refresh();
        }

        private void username_LostFocus(object sender, RoutedEventArgs e)
        {
            //TODO unique username i ipis na neku labelu
        }

        private void doctorCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            valiDate.Text = "";
           // if (roomCB.SelectedItem == null)
             //   return;
            var app = Application.Current as App;
            var r = roomCB.SelectedItem as Room;
            long roomId = -1;
            if (r != null)
                roomId = r.Id;
            var d = doctorCB.SelectedItem as Doctor;
            long doctorId = -1;
            int shift = 0;
            if (d != null)
            {
                doctorId = d.Id;
                shift = (int)d.Shift + 1;
            }

                
            var p = patientCB.SelectedItem as Patient;
            long patientId = -1;
            if (p != null)
                patientId = p.Id;
            List<string> times = app.ScheduledAppointmentController.GetAppropriateTimes((DateTime)dateDP.SelectedDate, doctorId, patientId, roomId, shift);
            timeCB.ItemsSource = times;
        }

        private void dateDP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            valiDate.Text = "";
            if ((DateTime)dateDP.SelectedDate < DateTime.Today)
            {
                valiDate.Text = "Ne možete zakazati termin u prošlosti!";
            }
            else
            {
                valiDate.Text = "";
            }
            var app = Application.Current as App;


            var r = roomCB.SelectedItem as Room;
            long roomId = -1;
            if (r != null)
                roomId = r.Id;
            var d = doctorCB.SelectedItem as Doctor;
            long doctorId = -1;
            int shift = 0;
            if (d != null)
            {
                doctorId = d.Id;
                shift = (int)d.Shift + 1;
            }
            var p = patientCB.SelectedItem as Patient;
            long patientId = -1;
            if (p != null)
                patientId = p.Id;
            List<string> times = app.ScheduledAppointmentController.GetAppropriateTimes((DateTime)dateDP.SelectedDate, doctorId, patientId, roomId, shift);
            timeCB.ItemsSource = times;

        }

        private void roomCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            valiDate.Text = "";
            if (roomCB.SelectedItem == null)
                return;
            var app = Application.Current as App;
            var r = roomCB.SelectedItem as Room;
            long roomId = -1;
            if (r != null)
                roomId = r.Id;
            var d = doctorCB.SelectedItem as Doctor;
            long doctorId = -1;
            int shift = 0;
            if (d != null)
            {
                doctorId = d.Id;
                shift = (int)d.Shift + 1;
            }
            var p = patientCB.SelectedItem as Patient;
            long patientId = -1;
            if (p != null)
                patientId = p.Id;

            //if(dateDP.SelectedDate == null)
             //   dateDP.SelectedDate = DateTime.Today;
            List<string> times = app.ScheduledAppointmentController.GetAppropriateTimes((DateTime)dateDP.SelectedDate, doctorId, patientId, roomId, shift);
            timeCB.ItemsSource = times;
        }


        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _homePagePatients.SecretaryFrame.Content = new SecretaryAppointmentPage(_homePagePatients);
        }

        private void Save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var app = Application.Current as App;
            
            var p = patientCB.SelectedItem as Patient;
            if (p == null && guest == false)
            {
                valiDate.Text = "Izaberi pacijenta!";
                return;
            }

            var d = doctorCB.SelectedItem as Doctor;
            if (d == null)
            {
                valiDate.Text = "Izaberi doktora!";
                return;
            }

            if(typeCB.SelectedItem == null)
            {
                valiDate.Text = "Izaberi tip pregleda!";
            }

            var r = roomCB.SelectedItem as Room;
            if (r == null)
            {
                valiDate.Text = "Izaberi sobu!";
                return;
            }

            if (timeCB.SelectedItem == null)
            {
                valiDate.Text = "Izaberi vreme!";
                return;
            }

           
            _scheduledAppointment = new ScheduledAppointment(-1);

            DateTime date = (DateTime)dateDP.SelectedDate;
            string[] time = timeCB.SelectedItem.ToString().Split(":");
            int hour = int.Parse(time[0]);
            int minute = int.Parse(time[1]);

            _scheduledAppointment.Start = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            _scheduledAppointment.End = new DateTime(date.Year, date.Month, date.Day, hour + 1, minute, 0);
            _scheduledAppointment.Room = r;
            _scheduledAppointment.Doctor = d;

            if(guest)
            {
                Patient patient = new Patient(-1);
                patient.Name = guestNameTB.Text;
                patient.Surname = guestSurnameTB.Text;
                patient = app.PatientController.Create(patient);
                Account account = new Account(username.Text, password.Text, true, patient, AccountType.GUEST);
                account = app.AccountController.Create(account);
                patient.Account = account;
                app.PatientController.Update(patient);
                p = patient;
            }

            _scheduledAppointment.Patient = p;
            _scheduledAppointment.AppointmentType = (AppointmentType)typeCB.SelectedItem;
            _scheduledAppointment = app.ScheduledAppointmentController.Create(_scheduledAppointment);
            _homePagePatients.SecretaryFrame.Content = new SecretaryAppointmentPage(_homePagePatients);

        }
    }
}
