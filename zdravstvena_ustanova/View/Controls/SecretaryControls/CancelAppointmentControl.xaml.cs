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

namespace zdravstvena_ustanova.View.Controls.SecretaryControls
{
    /// <summary>
    /// Interaction logic for CancelAppointmentControl.xaml
    /// </summary>
    public partial class CancelAppointmentControl : UserControl, INotifyPropertyChanged
    {

        public DataGrid AppointmentsDataGrid { get; set; }

        public ObservableCollection<ScheduledAppointment> ScheduledAppointments { get; set; }

        private bool isUnscheduled;
         #region NotifyProperties
        private string _patientName;
        public string PatientName
        {
            get
            {
                return _patientName;
            }
            set
            {
                if (value != _patientName)
                {
                    _patientName = value;
                    OnPropertyChanged("PatientName");
                }
            }
        }
        private string _doctorName;
        public string DoctorName
        {
            get
            {
                return _doctorName;
            }
            set
            {
                if (value != _doctorName)
                {
                    _doctorName = value;
                    OnPropertyChanged("DoctorName");
                }
            }
        }

        private DateTime _appointmentDate;
        public DateTime AppointmentDate
        {
            get
            {
                return _appointmentDate;
            }
            set
            {
                if (value != _appointmentDate)
                {
                    _appointmentDate = value;
                    OnPropertyChanged("AppointmentDate");
                }
            }
        }
        #endregion
        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public CancelAppointmentControl(ObservableCollection<ScheduledAppointment> scheduledAppointments, DataGrid appointmentsDataGrid, bool isUnscheduled)
        {
            InitializeComponent();
            DataContext = this;
            AppointmentsDataGrid = appointmentsDataGrid;
            ScheduledAppointments = scheduledAppointments;
            this.isUnscheduled = isUnscheduled;
            ScheduledAppointment sa = AppointmentsDataGrid.SelectedItem as ScheduledAppointment;
            PatientName = sa.Patient.Name + " " + sa.Patient.Surname;
            DoctorName = sa.Doctor.Name + " " + sa.Doctor.Surname;
            AppointmentDate = sa.Start;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var appointment = (ScheduledAppointment)AppointmentsDataGrid.SelectedItem;
            if (appointment == null)
            {
                MessageBox.Show("Odaberi termin!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var app = Application.Current as App;

            if(isUnscheduled)
                app.UnScheduledAppointmentController.Delete(appointment.Id);
            else
                app.ScheduledAppointmentController.Delete(appointment.Id);

            ScheduledAppointments.Remove(appointment);

            CollectionViewSource.GetDefaultView(AppointmentsDataGrid.ItemsSource).Refresh();

            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }

    }
}
