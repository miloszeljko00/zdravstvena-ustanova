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
        public HolidayRequestStatusReviewWindow(HolidayRequest holidayRequest)
        {
            InitializeComponent();
            DataContext = this;
            CauseOfRequestTextBox.Text = holidayRequest.Cause;
            StartDateDatePicker.SelectedDate = holidayRequest.StartDate;
            StartDateDatePicker.IsEnabled = false;
            EndDateDatePicker.SelectedDate= holidayRequest.EndDate;
            EndDateDatePicker.IsEnabled = false;
            if ((bool)holidayRequest.IsUrgent)
            {
                IsUrgentCheckBox.IsChecked = true;
            }
            else IsUrgentCheckBox.IsChecked = false;
            RequestStatusTextBox.Text = holidayRequest.HolidayRequestStatus.ToString();
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_Submit(object sender, RoutedEventArgs e)
        {

        }
    }
}
