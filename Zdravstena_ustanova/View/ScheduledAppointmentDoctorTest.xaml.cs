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
            dComboBox.DataContext = Doctors;

            Patients = new ObservableCollection<Patient>(app.PatientController.GetAll());
            pComboBox.DataContext = Patients;

            Rooms = new ObservableCollection<Room>(app.RoomController.GetAll());
            sComboBox.DataContext = Rooms;
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker1.SelectedDate == null || datePicker2.SelectedDate == null ||
                typeComboBox.SelectedIndex == -1 || dComboBox.SelectedIndex == -1 ||
                pComboBox.SelectedIndex == -1 || sComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Popuni sva polja prvo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var app = Application.Current as App;

            DateTime startDate = (DateTime)datePicker1.SelectedDate;
            DateTime endDate = (DateTime)datePicker2.SelectedDate;
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
            if (datePicker1.SelectedDate == null || datePicker2.SelectedDate == null ||
                typeComboBox.SelectedIndex == -1 || dComboBox.SelectedIndex == -1 ||
                pComboBox.SelectedIndex == -1 || sComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Popuni sva polja prvo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var app = Application.Current as App;

            scheduledAppointment.Start = (DateTime)datePicker1.SelectedDate;
            scheduledAppointment.End = (DateTime)datePicker2.SelectedDate;
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
            datePicker2.SelectedDate = scheduledAppointment.End;
            typeComboBox.SelectedValue = scheduledAppointment.AppointmentType;
            dComboBox.SelectedValue = scheduledAppointment.DoctorId;
            pComboBox.SelectedValue = scheduledAppointment.PatientId;
            sComboBox.SelectedValue = scheduledAppointment.RoomId;


        }
    }
}
