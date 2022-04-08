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
    public partial class ScheduledAppointmentPatient : Window
    {
        public ObservableCollection<ScheduledAppointment> ScheduledAppointments { get; set; }
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }



        public ScheduledAppointmentPatient()
        {
            InitializeComponent();
            this.DataContext = this;
            var app = Application.Current as App;
            
            ScheduledAppointments = new ObservableCollection<ScheduledAppointment>(app.ScheduledAppointmentController.GetAll());
            

            Doctors = new ObservableCollection<Doctor>(app.DoctorController.GetAll());
            doctorCB.DataContext = Doctors;

            Patients = new ObservableCollection<Patient>(app.PatientController.GetAll());

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
            long doctor = (long)doctorCB.SelectedValue;

            //RoomId zakucan na fiksnu vrednost dok se lekaru ne dodeli radna prostorija
            var scheduledAppointment = new ScheduledAppointment(date,date,AppointmentType.REGULAR_APPOINTMENT, p, doctor, 1);

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
            scheduledAppointment.AppointmentType = AppointmentType.REGULAR_APPOINTMENT;
            scheduledAppointment.PatientId = p;
            scheduledAppointment.DoctorId = (long)doctorCB.SelectedValue;
            //RoomId zakucan na fiksnu vrednost dok se lekaru ne dodeli radna prostorija
            scheduledAppointment.RoomId = 1;
            scheduledAppointment.Patient = app.PatientController.GetById(scheduledAppointment.PatientId);
            scheduledAppointment.Doctor = app.DoctorController.GetById(scheduledAppointment.DoctorId);
            scheduledAppointment.Room = app.RoomController.GetById(scheduledAppointment.RoomId);
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
            doctorCB.SelectedValue = scheduledAppointment.DoctorId;
            Patient p = app.PatientController.GetById(scheduledAppointment.PatientId);
            patient.Text = p.Name + " " + p.Surname;

        }
    }
}
