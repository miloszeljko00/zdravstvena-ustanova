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
using zdravstvena_ustanova.Model.Enums;

namespace zdravstvena_ustanova.View.Pages.SecretaryPages
{
    /// <summary>
    /// Interaction logic for EditHolidayRequestPage.xaml
    /// </summary>
    public partial class EditHolidayRequestPage : Page
    {
        private HomePagePatients _homePagePatients;

        private HolidayRequest holidayRequest;

        public ObservableCollection<ScheduledAppointment> ScheduledAppointments { get; set; }
        public EditHolidayRequestPage(HomePagePatients hpp, HolidayRequest hr)
        {
            InitializeComponent();
            DataContext = this;
            _homePagePatients = hpp;
            holidayRequest = hr;
            var app = Application.Current as App;
            doctorTB.Text = holidayRequest.Doctor.Name + " " + holidayRequest.Doctor.Surname;
            startTB.Text = holidayRequest.StartDate.ToString();
            endTB.Text = holidayRequest.EndDate.ToString();
            causeTB.Text = holidayRequest.Cause;
            statusCB.ItemsSource = Enum.GetValues(typeof(HolidayRequestStatus)).Cast<HolidayRequestStatus>();
            statusCB.SelectedValue = holidayRequest.HolidayRequestStatus;
            reasonTB.Text = holidayRequest.ReasonForDenial;
            ScheduledAppointments = new ObservableCollection<ScheduledAppointment>(app.ScheduledAppointmentController.GetFromToDatesForDoctor(holidayRequest.StartDate, holidayRequest.EndDate, holidayRequest.Doctor.Id));
            if (ScheduledAppointments.Count == 0)
            {
                appointmentsTB.Visibility = Visibility.Hidden;
                change.Visibility = Visibility.Hidden;
            }
            
            appointmentsTB.Text = appointmentsTB.Text + ScheduledAppointments.Count.ToString();
        }

        private void Save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(ScheduledAppointments.Count != 0)
            {
                SolidColorBrush orangeBrush = new SolidColorBrush(Colors.DarkOrange);
                rectangle.Stroke = orangeBrush;
                return;
            }
            holidayRequest.HolidayRequestStatus = (HolidayRequestStatus)statusCB.SelectedValue;
            holidayRequest.ReasonForDenial = reasonTB.Text;
            var app = Application.Current as App;
            app.HolidayRequestController.Update(holidayRequest);
            _homePagePatients.SecretaryFrame.Content = new HolidayRequestPage(_homePagePatients);
        }

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _homePagePatients.SecretaryFrame.Content = new HolidayRequestPage(_homePagePatients);
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            _homePagePatients.SecretaryFrame.Content = new MoveAppointmentsPage(_homePagePatients, ScheduledAppointments, holidayRequest);
        }
    }
}
