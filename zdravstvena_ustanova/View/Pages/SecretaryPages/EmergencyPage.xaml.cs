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
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using zdravstvena_ustanova.View.Model;

namespace zdravstvena_ustanova.View.Pages.SecretaryPages
{
    /// <summary>
    /// Interaction logic for EmergencyPage.xaml
    /// </summary>
    public partial class EmergencyPage : Page
    {
        private HomePagePatients _homePagePatients;

        public ObservableCollection<Doctor> Doctors { get; set; }

        public ObservableCollection<Room> Rooms { get; set; }

        public ObservableCollection<Patient> Patients { get; set; }

        public ObservableCollection<Specialty> Specialties { get; set; }

        public ObservableCollection<ScheduledAppointment> ScheduledAppointments { get; set; }

        public ObservableCollection<MovableAppointment> MovableAppointments { get; set; }

        public ScheduledAppointment EmergencyAppointment { get; set; }

        public ICollectionView AppointmentsView { get; set; }

        private bool guest;
        public EmergencyPage(HomePagePatients hpp)
        {
            InitializeComponent();
            DataContext = this;
            _homePagePatients = hpp;

            var app = Application.Current as App;
            guest = false;

            Doctors = new ObservableCollection<Doctor>(app.DoctorController.GetAll());
            Patients = new ObservableCollection<Patient>(app.PatientController.GetAll());
            Specialties = new ObservableCollection<Specialty>(app.SpecialtyController.GetAll());
            ScheduledAppointments = new ObservableCollection<ScheduledAppointment>(app.ScheduledAppointmentController.GetAll());
            Rooms = new ObservableCollection<Room>(app.RoomController.GetAll());
            MovableAppointments = new ObservableCollection<MovableAppointment>();
            EmergencyAppointment = new ScheduledAppointment(-2);
            EmergencyAppointment.AppointmentType = AppointmentType.EMERGENCY_APPOINTMENT;
            createMovableAppointments();
            //AppointmentView = new CollectionViewSource { Source = ScheduledAppointments }.View;
            AppointmentsView = new CollectionViewSource { Source = MovableAppointments }.View;
            AppointmentsView.Filter = i =>
            {
                if (specialtyCB.SelectedIndex == -1)
                    return true;
                MovableAppointment ma = i as MovableAppointment;
                if (ma.ScheduledAppointment.Doctor.Specialty.Id == (long)specialtyCB.SelectedValue)
                    return true;
                else
                    return false;
            };
            patientCB.ItemsSource = Patients;
            specialtyCB.ItemsSource = Specialties;
        }

        private void createMovableAppointments()
        {
            DateTime today = DateTime.Now;
            today = today.AddMinutes(-today.Minute);
            today = today.AddHours(1);
            today = today.AddSeconds(-today.Second);
            // today = today.AddMilliseconds(-today.Millisecond);
            foreach (ScheduledAppointment sa in ScheduledAppointments)
            {
                if (DateTime.Compare(sa.Start.Date, today.Date) != 0 || sa.Start.Hour != today.Hour)
                    continue;
                var nextTime = findNextTime(sa, today);
                MovableAppointments.Add(new MovableAppointment(sa, nextTime));
            }
        }

        private DateTime findNextTime(ScheduledAppointment sa, DateTime today)
        {
            //;
            DateTime ret = today;
            while(true)
            {
                bool find = true;
                if (today.Hour == 21) { today = today.AddDays(1); today = today.AddHours(-14); }
                /*if(ScheduledAppointments.Where(i => i.Doctor.Id == sa.Doctor.Id && i.Start.Date == sa.Start.Date && i.Start.Hour == sa.Start.Hour).Single()==null)
                {
                    ret = today;
                    break;
                }*/
                foreach(ScheduledAppointment scheduledAppointment in ScheduledAppointments)
                {
                    if (scheduledAppointment.Start.Date != today.Date || today.Hour != scheduledAppointment.Start.Hour)
                        continue;
                    if (scheduledAppointment.Doctor.Id == sa.Doctor.Id)
                    {
                        find = false;
                        break;
                    }
                    if (scheduledAppointment.Room.Id == sa.Room.Id)
                    {
                        find = false;
                        break;
                    }

                }
                if(find)
                {
                    ret = today;
                    break;
                }

                today = today.AddHours(1);

            }
            return ret;
        }

        private void Guest_Checked(object sender, RoutedEventArgs e)
        {
            if (Guest.IsChecked == true)
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

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _homePagePatients.SecretaryFrame.Content = new SecretaryAppointmentPage(_homePagePatients);
        }

        private void specialtyCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var app = Application.Current as App;
            AppointmentsView.Refresh();
            var doctors0 = app.DoctorController.GetAll();
           // var doctors1 = new ObservableCollection<Doctor>(doctors0);
            var rooms = app.RoomController.GetAll();
            DateTime today = DateTime.Now;
            today = today.AddMinutes(-today.Minute);
            today = today.AddHours(1);
            today = today.AddSeconds(-today.Second);
            today = today.AddMilliseconds(-today.Millisecond);

            //collection.Remove(collection.Where(i => i.Id == instance.Id).Single());
            foreach (Doctor d in doctors0)
            {
                if (d.Specialty.Id != (long)specialtyCB.SelectedValue)
                {
                    Doctors.Remove(Doctors.Where(i => i.Id == d.Id).Single());
                }
            }
            foreach (ScheduledAppointment sa in ScheduledAppointments)
            {
                if (DateTime.Compare(sa.Start.Date, today.Date) != 0 || sa.Start.Hour != today.Hour)
                    continue;

                foreach(Doctor d in Doctors)
                {
                    if(d.Id == sa.Doctor.Id)
                    {
                        Doctors.Remove(d);
                        break;
                    }
                }

                foreach(Room r in Rooms)
                {
                    if(r.Id == sa.Room.Id)
                    {
                        Rooms.Remove(r);
                        break;
                    }
                }
                
            }
            if (Doctors.Count == 0 || Rooms.Count == 0)
                appointmentTB.Text = "Nema slobodnih termina";

            else
            {
                appointmentTB.Text = "Hitan slucaj resava doktor " + Doctors.First().Name + " " + Doctors.First().Surname + " u sobi "
                                    + Rooms.First().Name;
                EmergencyAppointment.Start = today;
                EmergencyAppointment.End = EmergencyAppointment.Start.AddHours(1);
                EmergencyAppointment.Doctor = Doctors.First();
                EmergencyAppointment.Room = Rooms.First();
            }
                //appointmentTB.Text = "Hitan slucaj resava doktor " + Doctors.First().Name +" " + Doctors.First().Surname + " u sobi "////
                                 //   + Rooms.First().Name;

            Doctors = new ObservableCollection<Doctor>(app.DoctorController.GetAll());
            Rooms = new ObservableCollection<Room>(app.RoomController.GetAll());
            
        }

        private void Save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var app = Application.Current as App;
            if (!guest)
            {
                EmergencyAppointment.Patient = patientCB.SelectedItem as Patient;
            }
            else
            {
                Patient patient = new Patient(-1);
                patient.Name = guestNameTB.Text;
                patient.Surname = guestSurnameTB.Text;
                patient = app.PatientController.Create(patient);
                Account account = new Account(username.Text, password.Text, true, patient, AccountType.GUEST);
                account = app.AccountController.Create(account);
                patient.Account = account;
                app.PatientController.Update(patient);
                EmergencyAppointment.Patient = patient;
            }
            if(EmergencyAppointment.Doctor == null)
            {
                MovableAppointment ma = (MovableAppointment)dataGridScheduledAppointments.SelectedItem;
                EmergencyAppointment.Doctor = ma.ScheduledAppointment.Doctor;
                EmergencyAppointment.Room = ma.ScheduledAppointment.Room;
                EmergencyAppointment.Start = ma.ScheduledAppointment.Start;
                EmergencyAppointment.End = ma.ScheduledAppointment.End;
                ScheduledAppointment movedScheduledAppointment = ma.ScheduledAppointment;
                movedScheduledAppointment.Start = ma.NewTime;
                movedScheduledAppointment.End = movedScheduledAppointment.Start.AddHours(1);
                app.ScheduledAppointmentController.Update(movedScheduledAppointment);
                
            }
            if (EmergencyAppointment.Patient == null)
                return; //Dodaj validaciju
            app.ScheduledAppointmentController.Create(EmergencyAppointment);
            _homePagePatients.SecretaryFrame.Content = new SecretaryAppointmentPage(_homePagePatients);
        }
    }
}
