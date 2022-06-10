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
using System.Windows.Shapes;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.View.Windows.DoctorWindows.View;

namespace zdravstvena_ustanova.View.Windows.DoctorWindows
{
    /// <summary>
    /// Interaction logic for HolidayRequestsReviewWindow.xaml
    /// </summary>
    public partial class HolidayRequestsReviewWindow : Window
    {
        public ObservableCollection<HolidayRequest> HolidayRequests { get; set; }
        public ProfileAndPersonalDataView ProfileAndPersonalDataView { get; set; }
        public HolidayRequestsReviewWindow(List<HolidayRequest> holidayRequests, ProfileAndPersonalDataView profileAndPersonalDataView)
        {
            InitializeComponent();
            this.DataContext = this;
            var app = Application.Current as App;
            HolidayRequests = new ObservableCollection<HolidayRequest>(holidayRequests);
            ProfileAndPersonalDataView = profileAndPersonalDataView;

        }

        private void dataGridHolidayRequests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            var holidayRequest = (HolidayRequest)dataGridHolidayRequests.SelectedItem;
            HolidayRequestStatusReviewWindow holidayRequestStatusReviewWindow = new HolidayRequestStatusReviewWindow(holidayRequest, this);
            holidayRequestStatusReviewWindow.ShowDialog();
            dataGridHolidayRequests.SelectedCells.Clear();

        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Da li ste sigurni?", "Checkout", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
        private void Button_Click_CreateNewHolidayRequest(object sender, RoutedEventArgs e)
        {
            HolidayRequestFormWindow holidayRequestFormWindow = new HolidayRequestFormWindow(this, ProfileAndPersonalDataView);
            holidayRequestFormWindow.ShowDialog();
        }
    }
}
