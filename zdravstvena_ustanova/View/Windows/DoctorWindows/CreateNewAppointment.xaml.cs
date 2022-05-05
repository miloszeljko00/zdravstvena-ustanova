using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
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
using System.Windows.Shapes;
using zdravstvena_ustanova.View.Model;

namespace zdravstvena_ustanova.View.Windows.DoctorWindows
{
    /// <summary>
    /// Interaction logic for CreateNewAppointment.xaml
    /// </summary>
    public partial class CreateNewAppointment : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }
        public DoctorHomePageWindow DoctorHomePageWindow { get; set; }


        #region NotifyProperties
        private Patient _selectedPatient;
        public Patient SelectedPatient
        {
            get
            {
                return _selectedPatient;
            }
            set
            {
                if (value != _selectedPatient)
                {
                    _selectedPatient = value;
                    OnPropertyChanged("SelectedPatient");
                }
            }
        }
        private Room _selectedRoom;
        public Room SelectedRoom
        {
            get
            {
                return _selectedRoom;
            }
            set
            {
                if (value != _selectedRoom)
                {
                    _selectedRoom = value;
                    OnPropertyChanged("SelectedRoom");
                }
            }
        }
        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                if (value != _selectedDate)
                {
                    _selectedDate = value;
                    OnPropertyChanged("SelectedDate");
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

        public CreateNewAppointment()
        {
            InitializeComponent();
            DataContext = this;
            Patients = new ObservableCollection<Patient>();
            var app = Application.Current as App;
            var patients = app.PatientController.GetAll();
            foreach(Patient p in patients)
            {
                Patients.Add(p);
            }
          
            Rooms = new ObservableCollection<Room>();
            var rooms = app.RoomController.GetAll();
            foreach (Room r in rooms)
            {
                Rooms.Add(r);
            }
            SelectedRoom = ((Doctor)app.LoggedInUser).Room;
            rComboBox.Text = SelectedRoom.Name;

            typeOfAppointment.ItemsSource = Enum.GetValues(typeof(AppointmentType)).Cast<AppointmentType>();
        }
        public CreateNewAppointment(DoctorHomePageWindow dhpw)
        {
            InitializeComponent();
            DataContext = this;
            DoctorHomePageWindow = dhpw;
            Patients = new ObservableCollection<Patient>();
            var app = Application.Current as App;
            var patients = app.PatientController.GetAll();
            var dg = dhpw.dataGridScheduledAppointments;
            foreach (Patient p in patients)
            {
                Patients.Add(p);
            }

            Rooms = new ObservableCollection<Room>();
            var rooms = app.RoomController.GetAll();
            foreach (Room r in rooms)
            {
                Rooms.Add(r);
            }
            SelectedRoom = ((Doctor)app.LoggedInUser).Room;
            rComboBox.Text = SelectedRoom.Name;

            typeOfAppointment.ItemsSource = Enum.GetValues(typeof(AppointmentType)).Cast<AppointmentType>();

            var selectedCellIndex = (int)dg.SelectedCells[0].Column.DisplayIndex;
            AppointmentsWeeklyByHour awbh = (AppointmentsWeeklyByHour)dg.SelectedCells[0].Item;
            DateTime date;


            switch (selectedCellIndex)
            {
                case 0:
                    return;
                case 1:
                    date = awbh.DateOfWeekStart;
                    break;
                case 2:
                    date = awbh.DateOfWeekStart.AddDays(1);
                    break;
                case 3:
                    date = awbh.DateOfWeekStart.AddDays(2);
                    break;
                case 4:
                    date = awbh.DateOfWeekStart.AddDays(3);
                    break;
                case 5:
                    date = awbh.DateOfWeekStart.AddDays(4);
                    break;
                case 6:
                    date = awbh.DateOfWeekStart.AddDays(5);
                    break;
                case 7:
                    date = awbh.DateOfWeekStart.AddDays(6);
                    break;
                default:
                    return;
            }
            SelectedDate = new DateTime(date.Year, date.Month, date.Day);
            TimeComboBox.Text = date.Hour.ToString();
            var currentDateTime = DateTime.Now;
            if(SelectedDate.Year<currentDateTime.Year)
            {
                MessageBox.Show("Ne mozete zakazivati termine u proslost!");
                Close();
                return;
            } else if(SelectedDate.Month<currentDateTime.Month)
            {
                MessageBox.Show("Ne mozete zakazivati termine u proslost!");
                Close();
                return;
            } else if (SelectedDate.Day < currentDateTime.Month)
            {
                MessageBox.Show("Ne mozete zakazivati termine u proslost!");
                Close();
                return;
            } else
            {
                this.ShowDialog();
            }
        }

        private void Button_Click_Submit(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            string selectedTime = ((ComboBoxItem)TimeComboBox.SelectedItem).Content.ToString();
            int startTime = int.Parse(selectedTime);
            int endTime = int.Parse(selectedTime) + 1;
            DateTime startDate = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, startTime, 0, 0);
            DateTime endDate = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, endTime, 0, 0);
            if(SelectedPatient==null)
            {
                if(typeOfAppointment.SelectedItem==null)
                {
                    MessageBox.Show("Morate odabrati pacijenta i tip pregleda!");
                    return;
                }
                MessageBox.Show("Morate odabrati pacijenta!");
                return;
            }
            else if(typeOfAppointment.SelectedItem==null)
            {
                MessageBox.Show("Morate odabrati tip pregleda!");
                return;
            }
            else
            {
                ScheduledAppointment sa = new ScheduledAppointment(startDate, endDate, (AppointmentType)typeOfAppointment.SelectedItem, SelectedPatient.Id, app.LoggedInUser.Id, SelectedRoom.Id);
                sa = app.ScheduledAppointmentController.Create(sa);
            }


            DoctorHomePageWindow.UpdateCalendar();
            
            this.Close();
            
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Da li ste sigurni?", "Checkout", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(answer==MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
