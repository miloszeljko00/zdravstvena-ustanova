using Model;
using Model.Enums;
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

namespace zdravstvena_ustanova.View.Pages.SecretaryPages
{
    /// <summary>
    /// Interaction logic for SecretaryAppointmentPage.xaml
    /// </summary>
    public partial class SecretaryAppointmentPage : Page
    {
        public ObservableCollection<ScheduledAppointment> ScheduledAppointments { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }
        public ICollectionView AppointmentView { get; set; }

        private bool da;

        private int selectedType;

        public int selectedDoctor;
        public int SelectedDoctor
        {
            get
            {
                return this.selectedDoctor;
            }
            set
            {
                this.selectedDoctor = value;
                if (AppointmentView != null)
                {
                    da = true;
                    AppointmentView.Refresh(); 
                }
                    
            }
        }
        //public int selectedDate;
        public int selectedRoom { get; set; }
        public int SelectedRoom
        {
            get
            {
                return this.selectedRoom;
            }
            set
            {
                this.selectedRoom = value;
                if (AppointmentView != null)
                {
                    da = true;
                    AppointmentView.Refresh();
                    
                }
                    
            }
        }
        public int SelectedType
        {
            get
            {
                return this.selectedType;
            }
            set
            {
                da = true;
                this.selectedType = value;
                if (AppointmentView != null)
                {
                    AppointmentView.Refresh();
                }
                    
            }
        }

        private HomePagePatients _homePagePatients;

        public SecretaryAppointmentPage(HomePagePatients hpp)
        {
            InitializeComponent();
            selectedDoctor = -1;
            selectedRoom = -1;
            selectedType = -1;
            this.DataContext = this;
            var app = Application.Current as App;
            da = true;
            _homePagePatients = hpp;

            typeComboBox.ItemsSource = Enum.GetValues(typeof(AppointmentType)).Cast<AppointmentType>();
            ScheduledAppointments = new ObservableCollection<ScheduledAppointment>(app.ScheduledAppointmentController.GetAll());
            AppointmentView = new CollectionViewSource { Source = ScheduledAppointments }.View;
            AppointmentView.Filter = app1 =>
            {
                ScheduledAppointment sa = app1 as ScheduledAppointment;
                ComboBoxItem typeItem  = dComboBox.SelectedItem as ComboBoxItem;
                long did = -1;
                if(typeItem != null)
                    did = (long)typeItem.Content;
                if ((int)sa.AppointmentType != selectedType && selectedType != -1)
                    return false;
                if (selectedDoctor != -1)
                    if (sa.Doctor.Id != selectedDoctor + 1)
                        return false;
                if (sa.Room.Id != selectedRoom + 1 && selectedRoom != -1)
                     return false;
                return true;

            };

            dataGridScheduledAppointments.SelectedIndex = -1;
            Doctors = new ObservableCollection<Doctor>(app.DoctorController.GetAll());
            dComboBox.ItemsSource = Doctors;

            Rooms = new ObservableCollection<Room>(app.RoomController.GetAll());
            sComboBox.ItemsSource = Rooms;

            string[] times = {"08:00", "09:00", "10:00", "11:00", "12:00",
                                "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00" };
        }

        private void data1GridScheduledAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           /* if(da)
            {
                da = false;
                dataGridScheduledAppointments.SelectedIndex = -1;
               // return;
            }
            if (dataGridScheduledAppointments.SelectedIndex == -1)
                return;
            ScheduledAppointment sa = dataGridScheduledAppointments.SelectedValue as ScheduledAppointment;
            if(sa != null)
            {
                _homePagePatients.SecretaryFrame.Content = new EditAppointmentPage(sa, _homePagePatients);
            }*/

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        { 
            var editIcon = (Image)e.OriginalSource;
            var dataContext = editIcon.DataContext;
            ScheduledAppointment sa = dataContext as ScheduledAppointment;

            if (sa != null)
            {
                _homePagePatients.SecretaryFrame.Content = new EditAppointmentPage(sa, _homePagePatients);
            }
        }
    }
}
