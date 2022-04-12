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

namespace zdravstvena_ustanova.View
{
    public partial class ScheduledAppointmentPatient : Window
    {
        public ObservableCollection<ScheduledAppointment> ScheduledAppointments { get; set; }
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }

        public string selectedTime;

        public ScheduledAppointmentPatient()
        {
            InitializeComponent();
            this.DataContext = this;
            var app = Application.Current as App;
            
            ScheduledAppointments = new ObservableCollection<ScheduledAppointment>(app.ScheduledAppointmentController.GetAll());
            

            Doctors = new ObservableCollection<Doctor>(app.DoctorController.GetAll());
            doctorCB.DataContext = Doctors;

            Patients = new ObservableCollection<Patient>(app.PatientController.GetAll());

            string[] times = {"08:00", "09:00", "10:00", "11:00", "12:00",
                                "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00" };

            timeComboBox.Items.Clear();
            timeComboBox.ItemsSource = times;

        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker.SelectedDate == null || doctorCB.SelectedIndex == -1)
            {
                MessageBox.Show("Popuni sva polja prvo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            long p = 0;
            for (int i = 0; i < Patients.Count; i++)
            {
                if (patient.Text.Equals(Patients[i].Name + " " + Patients[i].Surname))
                {
                    p = Patients[i].Id;
                    break;
                }
                if (i == Patients.Count - 1)
                {
                    MessageBox.Show("Pacijent nije prepoznat!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            var app = Application.Current as App;

            DateTime date = (DateTime)datePicker.SelectedDate;
            string[] time = selectedTime.Split(":");
            int hour = int.Parse(time[0]);
            int minute = int.Parse(time[1]);

            DateTime startDate = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            DateTime endDate = new DateTime(date.Year, date.Month, date.Day, hour + 1, minute, 0);

            long doctor = (long)doctorCB.SelectedValue;

            //RoomId zakucan na fiksnu vrednost dok se lekaru ne dodeli radna prostorija
            var scheduledAppointment = new ScheduledAppointment(startDate, endDate, AppointmentType.REGULAR_APPOINTMENT, p, doctor, 1);

            scheduledAppointment = app.ScheduledAppointmentController.Create(scheduledAppointment);

            scheduledAppointment = app.ScheduledAppointmentController.GetById(scheduledAppointment.Id);

            ScheduledAppointments.Add(scheduledAppointment);
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            var scheduledAppointment = (ScheduledAppointment)dGScheduledAppointments.SelectedItem;
            if (scheduledAppointment == null)
            {
                MessageBox.Show("Odaberi sobu!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (datePicker.SelectedDate == null || doctorCB.SelectedIndex == -1)
            {
                MessageBox.Show("Popuni sva polja prvo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            long p = 0;
            for (int i = 0; i < Patients.Count; i++)
            {
                if (patient.Text.Equals(Patients[i].Name + " " + Patients[i].Surname))
                {
                    p = Patients[i].Id;
                    break;
                }
                if (i == Patients.Count - 1)
                {
                    MessageBox.Show("Pacijent nije prepoznat!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            var app = Application.Current as App;

            scheduledAppointment.Start = (DateTime)datePicker.SelectedDate;
            scheduledAppointment.End = (DateTime)datePicker.SelectedDate;

            DateTime date = (DateTime)datePicker.SelectedDate;
            string[] time = selectedTime.Split(":");
            int hour = int.Parse(time[0]);
            int minute = int.Parse(time[1]);

            scheduledAppointment.Start = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            scheduledAppointment.End = new DateTime(date.Year, date.Month, date.Day, hour + 1, minute, 0);

            scheduledAppointment.AppointmentType = AppointmentType.REGULAR_APPOINTMENT;
            scheduledAppointment.Patient.Id = p;
            scheduledAppointment.Doctor.Id = (long)doctorCB.SelectedValue;
            //RoomId zakucan na fiksnu vrednost dok se lekaru ne dodeli radna prostorija
            scheduledAppointment.Room.Id = app.DoctorController.GetById(scheduledAppointment.Doctor.Id).Room.Id;
            scheduledAppointment.Patient = app.PatientController.GetById(scheduledAppointment.Patient.Id);
            scheduledAppointment.Doctor = app.DoctorController.GetById(scheduledAppointment.Doctor.Id);
            scheduledAppointment.Room = app.RoomController.GetById(scheduledAppointment.Room.Id);
            app.ScheduledAppointmentController.Update(scheduledAppointment);

            CollectionViewSource.GetDefaultView(dGScheduledAppointments.ItemsSource).Refresh();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            var scheduledAppointment = (ScheduledAppointment)dGScheduledAppointments.SelectedItem;
            if (scheduledAppointment == null)
            {
                MessageBox.Show("Odaberi sobu!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var app = Application.Current as App;

            app.ScheduledAppointmentController.Delete(scheduledAppointment.Id);
            ScheduledAppointments.Remove(scheduledAppointment);

            CollectionViewSource.GetDefaultView(dGScheduledAppointments.ItemsSource).Refresh();
        }

        private void dGScheduledAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var scheduledAppointment = (ScheduledAppointment)dGScheduledAppointments.SelectedItem;

            if (scheduledAppointment == null)
            {
                return;
            }
            var app = Application.Current as App;

            datePicker.SelectedDate = scheduledAppointment.Start;
            string hour;
            string minute;
            FixTimeFormat(scheduledAppointment, out hour, out minute);

            selectedTime = hour + ":" + minute;
            timeComboBox.SelectedItem = selectedTime;
            doctorCB.SelectedValue = scheduledAppointment.Doctor.Id;
            Patient p = app.PatientController.GetById(scheduledAppointment.Patient.Id);
            patient.Text = p.Name + " " + p.Surname;
            
        }

        private static void FixTimeFormat(ScheduledAppointment scheduledAppointment, out string hour, out string minute)
        {
            if (scheduledAppointment.Start.Hour < 10)
            {
                hour = "0" + scheduledAppointment.Start.Hour.ToString();
            }
            else
            {
                hour = scheduledAppointment.Start.Hour.ToString();
            }

            if (scheduledAppointment.Start.Minute < 10)
            {
                minute = "0" + scheduledAppointment.Start.Minute.ToString();
            }
            else
            {
                minute = scheduledAppointment.Start.Minute.ToString();
            }
        }

        private void TimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedTime = (string)timeComboBox.SelectedItem;
        }
    }
}
