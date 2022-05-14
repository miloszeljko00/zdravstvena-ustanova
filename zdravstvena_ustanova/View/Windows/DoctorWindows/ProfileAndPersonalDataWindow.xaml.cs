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
using System.Windows.Shapes;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.View.Windows.DoctorWindows
{
    /// <summary>
    /// Interaction logic for ProfileAndPersonalDataWindow.xaml
    /// </summary>
    public partial class ProfileAndPersonalDataWindow : Window
    {
        public Doctor Doctor { get; set; }
        public ProfileAndPersonalDataWindow()
        {
            InitializeComponent();
            DataContext = this;
            var app = Application.Current as App;
            Doctor = (Doctor)app.LoggedInUser;
            NameTextBox.Text = Doctor.Name;
            SurnameTextBox.Text = Doctor.Surname;
            DateOfBirthTextBox.Text = Doctor.DateOfBirth.ToString();
            PhoneNumberTextBox.Text = Doctor.PhoneNumber;
            EmailTextBox.Text = Doctor.Email;
            var specialty = app.SpecialtyController.GetById(Doctor.Specialty.Id);
            SpecializationTextBox.Text = specialty.Name;
            ExperienceTextBox.Text=Doctor.Experience.ToString();
            UsernameTextBox.Text = Doctor.Account.Username;


        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_Submit(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_HolidayRequest(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            Doctor myDoctor = (Doctor)app.LoggedInUser;
            var holidayRequests = app.HolidayRequestController.GetAll();
            var sign = 0;
            foreach (HolidayRequest hr in holidayRequests)
            {
                if (hr.Doctor.Id == myDoctor.Id)
                {
                    HolidayRequestStatusReviewWindow holidayRequestStatusReviewWindow = new HolidayRequestStatusReviewWindow(hr);
                    holidayRequestStatusReviewWindow.ShowDialog();
                    sign = 1;
                }
            }
            if(sign==0)
            {
                HolidayRequestFormWindow holidayRequest = new HolidayRequestFormWindow();
                holidayRequest.ShowDialog();
            }
            
        }
    }
}
