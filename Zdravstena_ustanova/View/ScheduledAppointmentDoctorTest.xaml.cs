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
        public string selectedTime;


        public ScheduledAppointmentDoctorTest()
        {
            InitializeComponent();
            this.DataContext = this;
            var app = Application.Current as App;
            
            typeComboBox.ItemsSource = Enum.GetValues(typeof(AppointmentType)).Cast<AppointmentType>();
            ScheduledAppointments = new ObservableCollection<ScheduledAppointment>(app.ScheduledAppointmentController.GetAll());
            

            Doctors = new ObservableCollection<Doctor>(app.DoctorController.GetAll());
            dComboBox.DataContext = Doctors;

            Patients = new ObservableCollection<Patient>(app.PatientController.GetAll());
            pComboBox.DataContext = Patients;

            Rooms = new ObservableCollection<Room>(app.RoomController.GetAll());
            sComboBox.DataContext = Rooms;

            string[] times = {"08:00", "09:00", "10:00", "11:00", "12:00",
                                "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00" };

            timeComboBox.Items.Clear();
            timeComboBox.ItemsSource = times;
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker1.SelectedDate == null || selectedTime == null ||
                typeComboBox.SelectedIndex == -1 || dComboBox.SelectedIndex == -1 ||
                pComboBox.SelectedIndex == -1 || sComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Popuni sva polja prvo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var app = Application.Current as App;

            DateTime date = (DateTime)datePicker1.SelectedDate;
            string[] time = selectedTime.Split(":");
            int hour = int.Parse(time[0]);
            int minute = int.Parse(time[1]);

            DateTime startDate = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            DateTime endDate = new DateTime(date.Year, date.Month, date.Day, hour+1, minute, 0);

            AppointmentType appointmentType = (AppointmentType)typeComboBox.SelectedIndex;
            long patientId = (long)pComboBox.SelectedValue;
            long doctorId = (long)dComboBox.SelectedValue;
            long roomId = (long)sComboBox.SelectedValue;


            var scheduledAppointment = new ScheduledAppointment(startDate,endDate,appointmentType, patientId, doctorId, roomId);

            scheduledAppointment = app.ScheduledAppointmentController.Create(scheduledAppointment);

            scheduledAppointment = app.ScheduledAppointmentController.GetById(scheduledAppointment.Id);

            ScheduledAppointments.Add(scheduledAppointment);
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            var scheduledAppointment = (ScheduledAppointment)dataGridScheduledAppointments.SelectedItem;
            if (scheduledAppointment == null)
            {
                MessageBox.Show("Odaberi sobu!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (datePicker1.SelectedDate == null || selectedTime == null ||
                typeComboBox.SelectedIndex == -1 || dComboBox.SelectedIndex == -1 ||
                pComboBox.SelectedIndex == -1 || sComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Popuni sva polja prvo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var app = Application.Current as App;

            DateTime date = (DateTime)datePicker1.SelectedDate;
            string[] time = selectedTime.Split(":");
            int hour = int.Parse(time[0]);
            int minute = int.Parse(time[1]);

            scheduledAppointment.Start = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            scheduledAppointment.End = new DateTime(date.Year, date.Month, date.Day, hour + 1, minute, 0);

            scheduledAppointment.AppointmentType = (AppointmentType)typeComboBox.SelectedIndex;
            scheduledAppointment.PatientId = (long)pComboBox.SelectedValue;
            scheduledAppointment.DoctorId = (long)dComboBox.SelectedValue;
            scheduledAppointment.RoomId = (long)sComboBox.SelectedValue;
            scheduledAppointment.Patient = app.PatientController.GetById(scheduledAppointment.PatientId);
            scheduledAppointment.Doctor = app.DoctorController.GetById(scheduledAppointment.DoctorId);
            scheduledAppointment.Room = app.RoomController.GetById(scheduledAppointment.RoomId);
            app.ScheduledAppointmentController.Update(scheduledAppointment);

            CollectionViewSource.GetDefaultView(dataGridScheduledAppointments.ItemsSource).Refresh();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            var scheduledAppointment = (ScheduledAppointment)dataGridScheduledAppointments.SelectedItem;
            if (scheduledAppointment == null)
            {
                MessageBox.Show("Odaberi sobu!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var app = Application.Current as App;

            app.ScheduledAppointmentController.Delete(scheduledAppointment.Id);
            ScheduledAppointments.Remove(scheduledAppointment);

            CollectionViewSource.GetDefaultView(dataGridScheduledAppointments.ItemsSource).Refresh();
        }

        private void dataGridScheduledAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var scheduledAppointment = (ScheduledAppointment)dataGridScheduledAppointments.SelectedItem;

            if (scheduledAppointment == null)
            {
                return;
            }
            datePicker1.SelectedDate = scheduledAppointment.Start;
            string hour;
            string minute;
            FixTimeFormat(scheduledAppointment, out hour, out minute);

            selectedTime = hour + ":" + minute;
            timeComboBox.SelectedItem = selectedTime;
            typeComboBox.SelectedValue = scheduledAppointment.AppointmentType;
            dComboBox.SelectedValue = scheduledAppointment.DoctorId;
            pComboBox.SelectedValue = scheduledAppointment.PatientId;
            sComboBox.SelectedValue = scheduledAppointment.RoomId;


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
        private void timeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedTime = (string)timeComboBox.SelectedItem;
        }
    }
}
