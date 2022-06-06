using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace zdravstvena_ustanova.View.Pages.SecretaryPages
{
    /// <summary>
    /// Interaction logic for MoveAppointmentsPage.xaml
    /// </summary>
    public partial class MoveAppointmentsPage : Page
    {
        public ObservableCollection<ScheduledAppointment> ScheduledAppointments { get; set; }

        private HomePagePatients _homePagePatients;

        private HolidayRequest holidayRequest;
  
        public MoveAppointmentsPage(HomePagePatients hpp, IEnumerable<ScheduledAppointment> sa, HolidayRequest hr)
        {
            InitializeComponent();
            this.DataContext = this;
            var app = Application.Current as App;
            _homePagePatients = hpp;
            ScheduledAppointments = (ObservableCollection<ScheduledAppointment>?)sa;
            holidayRequest = hr;
        }

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _homePagePatients.SecretaryFrame.Content = new EditHolidayRequestPage(_homePagePatients, holidayRequest);
            //vidi gde se vraca
        }

        private void dataGridScheduledAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sa = dataGridScheduledAppointments.SelectedItem as ScheduledAppointment;
            if (sa == null)
                return;
            var app = Application.Current as App;
            List<Doctor> doctors = (List<Doctor>)app.ScheduledAppointmentController.FindReplacementDoctors(sa);
            if (doctors.Count == 0)
            {
                messTB.Text = "Nema slobodnih doktora! Pomerite termin nakon povratka lekara.";
                doctorsCB.SelectedIndex = -1;
                doctorsCB.IsEnabled = false;
                return;
            }
            doctorsCB.IsEnabled = true;
            doctorsCB.ItemsSource = doctors;

        }

        private void doctorsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*var sa = dataGridScheduledAppointments.SelectedItem as ScheduledAppointment;
            if (sa == null)
                return; 
            ScheduledAppointments.Remove(sa);
            sa.Doctor = (Doctor)doctorsCB.SelectedItem;
            ScheduledAppointments.Add(sa);*/

        }

        private void Save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var app = Application.Current as App;
            var sa = dataGridScheduledAppointments.SelectedItem as ScheduledAppointment;
            if (sa == null)
                return;
            if (doctorsCB.SelectedItem == null)
                return;
            ScheduledAppointments.Remove(sa);
            sa.Doctor = (Doctor)doctorsCB.SelectedItem;
            ScheduledAppointments.Add(sa);
            app.ScheduledAppointmentController.Update(sa);

        }
    }

}

    
