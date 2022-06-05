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
    /// Interaction logic for HolidayRequestStatusReviewWindow.xaml
    /// </summary>
    public partial class HolidayRequestStatusReviewWindow : Window
    {
        public HolidayRequestsReviewWindow HolidayRequestsReviewWindow { get; set; }
        public HolidayRequestStatusReviewWindow(HolidayRequest holidayRequest, HolidayRequestsReviewWindow holidayRequestsReviewWindow)
        {
            InitializeComponent();
            DataContext = this;
            HolidayRequestsReviewWindow = holidayRequestsReviewWindow;
            ReasonForDenialTextBox.Text = holidayRequest.ReasonForDenial;
            StartDateDatePicker.SelectedDate = holidayRequest.StartDate;
            StartDateDatePicker.IsEnabled = false;
            EndDateDatePicker.SelectedDate= holidayRequest.EndDate;
            EndDateDatePicker.IsEnabled = false;
            RequestStatusTextBox.Text = holidayRequest.HolidayRequestStatus.ToString();
            RequestStatusTextBox.IsEnabled = false;
            ReasonForDenialTextBox.IsReadOnly = true;
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Da li ste sigurni?", "Checkout", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void Button_Click_Submit(object sender, RoutedEventArgs e)
        {

        }
    }
}
