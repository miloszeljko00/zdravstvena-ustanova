using Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using zdravstvena_ustanova.View.Controls.RoomsCalendar;

namespace zdravstvena_ustanova.View.Controls
{
    /// <summary>
    /// Interaction logic for ScheduleRenovation.xaml
    /// </summary>
    public partial class ScheduleRenovation : UserControl
    {
        public Room Room { get; set; }
        public RoomsCalendarControl RoomsCalendarControl { get; set; }

        public ScheduleRenovation(Room room, RoomsCalendarControl roomsCalendarControl)
        {
            InitializeComponent();
            DataContext = this;
            Room = room;
            RoomsCalendarControl = roomsCalendarControl;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if(RenovationDescriptionTextBox.Text == "" || StartDatePicker.SelectedDate == null || EndDatePicker.SelectedDate == null){
                MessageBox.Show("Popuni prvo sva polja!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateTime startDate = (DateTime)StartDatePicker.SelectedDate;
            startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 8, 0, 0);
            DateTime endDate = (DateTime)EndDatePicker.SelectedDate;
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 21, 0, 0);
            string description = RenovationDescriptionTextBox.Text;
            var app = Application.Current as App;

            int numberOfAppointmentsInSelectedPeriod = app.RenovationAppointmentController.
                                                       NumberOfScheduledAppointmentsFromToForRoom(Room, startDate, endDate);

            if(app.RenovationAppointmentController.HasRenovationFromTo(Room, startDate, endDate))
            {
                MessageBoxResult answer = MessageBox.Show("U izabranom periodu već postoji zakazano renoviranje!",                                             
                                                "Renoviranje postoji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (numberOfAppointmentsInSelectedPeriod != 0)
            {
                MessageBoxResult answer = MessageBox.Show("U izabranom periodu postoji "
                                                + numberOfAppointmentsInSelectedPeriod
                                                + " zakazanih termina, otkazati sve termine?",
                                                "Otkazivanje zakazanih termina", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (answer == MessageBoxResult.No) return;
            }

            RenovationAppointment renovationAppointment = new RenovationAppointment(Room, startDate, endDate, description);


            renovationAppointment = app.RenovationAppointmentController.ScheduleRenovation(renovationAppointment);

            RoomsCalendarControl.DisplayCalendarForMonth(RoomsCalendarControl.DisplayedMonth);

            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }
    }
}
