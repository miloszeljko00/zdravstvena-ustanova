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

namespace zdravstvena_ustanova.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SecretaryAppointments : Window
    {
        public ObservableCollection<ScheduledAppointment> ScheduledAppointments { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }

        public ICollectionView PostsView { get; set; }

        private int selectedType;
        public int SelectedType
        {
            get
            {
                return this.selectedType;
            }
            set
            {
                this.selectedType = value;
                if(PostsView != null)
                    PostsView.Refresh();

            }
        }
        public int SelectedDoctor{ get; set; }
        //public int selectedDate;
        public int selectedRoom{ get; set; }
        public string selectedTime{ get; set; }

        
        public SecretaryAppointments()
        {
            InitializeComponent();
            
            SelectedDoctor = -1;
            //selectedDate = -1;
            selectedRoom = -1;
            this.DataContext = this;
            var app = Application.Current as App;

            typeComboBox.ItemsSource = Enum.GetValues(typeof(AppointmentType)).Cast<AppointmentType>();
            ScheduledAppointments = new ObservableCollection<ScheduledAppointment>(app.ScheduledAppointmentController.GetAll());
            PostsView = new CollectionViewSource { Source = ScheduledAppointments }.View;
            SelectedType = -1;
            PostsView.Filter = app1 =>
            {
                ScheduledAppointment sa = app1 as ScheduledAppointment;
                if ((int)sa.AppointmentType != SelectedType && SelectedType != -1)
                    return false;
                //if (sa.Doctor.Id != SelectedDoctor && SelectedType != -1)
                  //  return false;
                //if (sa.Room.Id != selectedRoom && SelectedType != -1)
                   // return false;
                return true;

            };
            

            Doctors = new ObservableCollection<Doctor>(app.DoctorController.GetAll());
            dComboBox.DataContext = Doctors;

            Rooms = new ObservableCollection<Room>(app.RoomController.GetAll());
            sComboBox.DataContext = Rooms;

            string[] times = {"08:00", "09:00", "10:00", "11:00", "12:00",
                                "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00" };

            
        }


        private void create_Click(object sender, RoutedEventArgs e)
        {

        }

        private void update_Click(object sender, RoutedEventArgs e)
        {

        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
