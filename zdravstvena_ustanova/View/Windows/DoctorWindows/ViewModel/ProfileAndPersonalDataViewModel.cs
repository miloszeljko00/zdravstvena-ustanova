using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using zdravstvena_ustanova.Commands;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.View.Windows.DoctorWindows.View;

namespace zdravstvena_ustanova.View.Windows.DoctorWindows.ViewModel
{
    public class ProfileAndPersonalDataViewModel : INotifyPropertyChanged
    {
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
        public Doctor Doctor { get; set; }
        public ProfileAndPersonalDataView ProfileAndPersonalDataView { get; set; }
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand OpenAnotherWindowCommand { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Specialization { get; set; }
        public string Experience { get; set; }
        public string Username { get; set; }
        public ProfileAndPersonalDataViewModel(ProfileAndPersonalDataView profileAndPersonalDataView)
        {
            var app = Application.Current as App;
            Doctor = (Doctor)app.LoggedInUser;
            ProfileAndPersonalDataView = profileAndPersonalDataView;
            CloseCommand = new RelayCommand(param => ExecuteClose());
            OpenAnotherWindowCommand = new RelayCommand(param => ExecuteOpen());
            FirstName = Doctor.Name;
            LastName = Doctor.Surname;
            DateOfBirth = Doctor.DateOfBirth.ToString();
            PhoneNumber = Doctor.PhoneNumber;
            EmailAddress = Doctor.Email;
            var specialty = app.SpecialtyController.GetById(Doctor.Specialty.Id);
            Specialization = specialty.Name;
            Experience = Doctor.Experience.ToString();
            Username = Doctor.Account.Username;
        }
        private void ExecuteOpen()
        {
            var app = Application.Current as App;
            Doctor myDoctor = (Doctor)app.LoggedInUser;
            var holidayRequests = app.HolidayRequestController.GetAll();
            var myDoctorHolidayRequests = new List<HolidayRequest>();
            var sign = 0;
            foreach (HolidayRequest hr in holidayRequests)
            {
                if (hr.Doctor.Id == myDoctor.Id)
                {
                    myDoctorHolidayRequests.Add(hr);
                    //HolidayRequestStatusReviewWindow holidayRequestStatusReviewWindow = new HolidayRequestStatusReviewWindow(hr);
                    //holidayRequestStatusReviewWindow.ShowDialog();
                    if (sign == 1)
                    {
                        continue;
                    }
                    else sign = 1;
                }
            }

            if (sign == 0)
            {
                HolidayRequestFormWindow holidayRequest = new HolidayRequestFormWindow(ProfileAndPersonalDataView);
                holidayRequest.ShowDialog();
            }
            else
            {
                HolidayRequestsReviewWindow holidayRequestsReviewWindow = new HolidayRequestsReviewWindow(myDoctorHolidayRequests, ProfileAndPersonalDataView);
                holidayRequestsReviewWindow.ShowDialog();
            }
        }
        private void ExecuteClose()
        {
            MessageBoxResult answer = MessageBox.Show("Da li ste sigurni?", "Checkout", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                ProfileAndPersonalDataView.Close();
            }
        }
        public List<HolidayRequest> RefreshPropertyHolidayRequests(HolidayRequestsReviewWindow holidayRequestsReviewWindow)
        {
            var app = Application.Current as App;
            var myDoctorHolidayRequests = new List<HolidayRequest>();
            var holidayRequestsFromBase = app.HolidayRequestController.GetAll();
            Doctor myDoctor = (Doctor)app.LoggedInUser;
            foreach (HolidayRequest hr in holidayRequestsFromBase)
            {
                if (hr.Doctor.Id == myDoctor.Id)
                {
                    myDoctorHolidayRequests.Add(hr);
                }
            }
            holidayRequestsReviewWindow.HolidayRequests = new System.Collections.ObjectModel.ObservableCollection<HolidayRequest>(myDoctorHolidayRequests);
            return myDoctorHolidayRequests;
        }
    }
}
