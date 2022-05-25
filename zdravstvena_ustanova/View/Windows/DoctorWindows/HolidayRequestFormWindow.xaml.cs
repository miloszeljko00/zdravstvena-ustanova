using System;
using System.Collections.Generic;
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
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.View.Windows.DoctorWindows
{
    /// <summary>
    /// Interaction logic for HolidayRequestWindow.xaml
    /// </summary>
    public partial class HolidayRequestFormWindow : Window, INotifyPropertyChanged
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

        public HolidayRequest HolidayRequest { get; set; }
        public int ConstructorCheck { get; set; }
        public HolidayRequestsReviewWindow HolidayRequestsReviewWindow { get; set; }
        public ProfileAndPersonalDataWindow ProfileAndPersonalDataWindow { get; set; }
        public HolidayRequestFormWindow()
        {
            InitializeComponent();
            DataContext = this;
            ConstructorCheck = 0;
        }
        public HolidayRequestFormWindow(HolidayRequestsReviewWindow holidayRequestsReviewWindow, ProfileAndPersonalDataWindow profileAndPersonalDataWindow)
        {
            InitializeComponent();
            DataContext = this;
            ConstructorCheck = 1;
            holidayRequestsReviewWindow.Close();
            HolidayRequestsReviewWindow = holidayRequestsReviewWindow;
            ProfileAndPersonalDataWindow = profileAndPersonalDataWindow;
        }

        private void Button_Click_Submit_HolidayRequest(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;

            if (startDate_datePicker.SelectedDate == null || endDate_datePicker.SelectedDate == null)
            {
                MessageBox.Show("Morate uneti vremenski interval!");
                return;
            }
            DateTime startDate = (DateTime)startDate_datePicker.SelectedDate;
            DateTime endDate = (DateTime)endDate_datePicker.SelectedDate;
            string cause = causeForHoliday_textBox.Text;
            
            if (endDate<=startDate)
            {
                MessageBox.Show("Morate uneti validan vremenski period!");
                return;
            }
            if(startDate.Year==DateTime.Now.Year)
            {
                if (startDate.Month == DateTime.Now.Month)
                {
                    if(startDate.Day - 2 <= DateTime.Now.Day)
                    {
                        MessageBox.Show("Morate zakazati odmor minimum 3 dana ranije!");
                        return;
                    }
                }
            }
            if(endDate<=startDate)
            {
                MessageBox.Show("Niste uneli validne vremenske intervale!");
                return;
            }
            bool isUrgent = (bool)isUrgent_comboBox.IsChecked;
            Doctor myDoctor = (Doctor)app.LoggedInUser;
            if (!isUrgent)
            {
                List<HolidayRequest> holidayRequestsFromBase = app.HolidayRequestController.GetAll().ToList();
                foreach (HolidayRequest hr in holidayRequestsFromBase)
                {
                    if (hr.Doctor.Specialty.Id == myDoctor.Specialty.Id)
                    {
                        if ((hr.StartDate <= startDate && startDate <= hr.EndDate) || (hr.StartDate <= endDate && endDate <= hr.EndDate) || (hr.StartDate <= startDate && endDate <= hr.EndDate) || (startDate<=hr.StartDate && hr.EndDate<=endDate))
                        {
                            if (hr.HolidayRequestStatus == zdravstvena_ustanova.Model.Enums.HolidayRequestStatus.ONHOLD || hr.HolidayRequestStatus == zdravstvena_ustanova.Model.Enums.HolidayRequestStatus.ACCEPTED)
                            {
                                MessageBox.Show("Nazalost trenutno ne mozete zakazati odmor. " +
                               "Kolega vase specijalnosti je vec zatrazio odmor u tom vremenskom periodu.");
                                return;
                            }
                        }
                    }
                }
            }
            HolidayRequest = new HolidayRequest(cause, startDate, endDate, zdravstvena_ustanova.Model.Enums.HolidayRequestStatus.ONHOLD, isUrgent, myDoctor, "");
            app = Application.Current as App;
            app.HolidayRequestController.Create(HolidayRequest);
            if(ConstructorCheck==0)
            {
                this.Close();
                return;
            }
            var refreshedHolidayRequests = ProfileAndPersonalDataWindow.RefreshPropertyHolidayRequests(HolidayRequestsReviewWindow);
            var holidayRequestsReviewWindowNew = new HolidayRequestsReviewWindow(refreshedHolidayRequests, ProfileAndPersonalDataWindow);
            holidayRequestsReviewWindowNew.ShowDialog();
            this.Close();
        }

        private void Button_Click_Cancel_HolidayRequest(object sender, RoutedEventArgs e)
        {
            var refreshedHolidayRequests = ProfileAndPersonalDataWindow.RefreshPropertyHolidayRequests(HolidayRequestsReviewWindow);
            var holidayRequestsReviewWindowNew = new HolidayRequestsReviewWindow(refreshedHolidayRequests, ProfileAndPersonalDataWindow);
            holidayRequestsReviewWindowNew.ShowDialog();
            this.Close();
        }
    }
}
