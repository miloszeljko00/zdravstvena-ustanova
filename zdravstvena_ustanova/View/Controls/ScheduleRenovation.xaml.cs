using zdravstvena_ustanova.Model;
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
        public DateRange DateRange { get; set; }
        public string Description { get; set; }
        public RenovationType RenovationType { get; set; }

        public ScheduleRenovation(Room room, RoomsCalendarControl roomsCalendarControl)
        {
            InitializeComponent();
            DataContext = this;
            Room = room;
            RoomsCalendarControl = roomsCalendarControl;

            var app = Application.Current as App;

            RenovationTypeComboBox.ItemsSource = app.RenovationTypeController.GetAll();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if(RenovationDescriptionTextBox.Text == "" || StartDatePicker.SelectedDate == null ||
                EndDatePicker.SelectedDate == null || RenovationTypeComboBox.SelectedItem == null){
                MessageBox.Show("Popuni prvo sva polja!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Description = RenovationDescriptionTextBox.Text;
            DateTime startDate = (DateTime)StartDatePicker.SelectedDate;
            startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 8, 0, 0);
            DateTime endDate = (DateTime)EndDatePicker.SelectedDate;
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 21, 0, 0);
            RenovationType = (RenovationType)RenovationTypeComboBox.SelectedItem;
            try
            {
                DateRange = new DateRange(startDate, endDate);
            }catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (startDate.CompareTo(DateTime.Now) <= 0)
            {
                MessageBox.Show("Datum početka renoviranja ne može biti u prošlosti!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (endDate.CompareTo(DateTime.Now) <= 0)
            {
                MessageBox.Show("Datum završetka renoviranja ne može biti u prošlosti!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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

            if(RenovationType.Id == 1)
            {
                RenovationAppointment renovationAppointment = new RenovationAppointment(Room, startDate, endDate, Description, RenovationType);
                renovationAppointment = app.RenovationAppointmentController.ScheduleRenovation(renovationAppointment);

                RoomsCalendarControl.DisplayCalendarForMonth(RoomsCalendarControl.DisplayedMonth);

                MainWindow.Modal.IsOpen = false;
                MainWindow.Modal.Content = null;
            }
            else if(RenovationType.Id == 2)
            {
                //TODO display new content in control for room merging
                MainWindow.Modal.Content = new ScheduleRenovationMerge(this);

            }else if(RenovationType.Id == 3)
            {
                //TODO display new content in control for room splitting
                MainWindow.Modal.Content = new ScheduleRenovationSplit(this);
            }


            
        }

        private void RenovationTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(((RenovationType)RenovationTypeComboBox.SelectedItem).Id == 1)
            {
                OkButton.Content = "Potvrdi";
            }
            else
            {
                OkButton.Content = "Dalje";
            }
        }
    }
}
