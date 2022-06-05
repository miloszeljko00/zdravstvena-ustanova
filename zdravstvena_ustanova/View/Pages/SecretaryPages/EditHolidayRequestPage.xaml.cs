using System;
using System.Collections.Generic;
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
        public EditHolidayRequestPage(HomePagePatients hpp, HolidayRequest hr)
        {
            InitializeComponent();
            DataContext = this;
            _homePagePatients = hpp;
            holidayRequest = hr;
            doctorTB.Text = holidayRequest.Doctor.Name + " " + holidayRequest.Doctor.Surname;
            startTB.Text = holidayRequest.StartDate.ToString();
            endTB.Text = holidayRequest.EndDate.ToString();
            causeTB.Text = holidayRequest.Cause;
            statusCB.ItemsSource = Enum.GetValues(typeof(HolidayRequestStatus)).Cast<HolidayRequestStatus>();
            statusCB.SelectedValue = holidayRequest.HolidayRequestStatus;
            reasonTB.Text = holidayRequest.ReasonForDenial;
        }

        private void Save_MouseDown(object sender, MouseButtonEventArgs e)
        {
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
    }
}
