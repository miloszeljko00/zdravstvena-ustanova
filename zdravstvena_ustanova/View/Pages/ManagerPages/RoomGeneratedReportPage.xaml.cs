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
using LiveCharts;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.View.Model;

namespace zdravstvena_ustanova.View.Pages.ManagerPages
{
    /// <summary>
    /// Interaction logic for RoomGeneratedReportPage.xaml
    /// </summary>
    public partial class RoomGeneratedReportPage : Page
    {
        public Room Room { get; set; }
        public ObservableCollection<AppointmentsCountOnDay> AppointmentsCountOnDays { get; set; }
        public ChartValues<int> NumberOfAppointments { get; set; }
        public ChartValues<string> Dates { get; set; }
        public RoomGeneratedReportPage(Room room, DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            DataContext = this;
            Room = room;
            var app = Application.Current as App;
            DateTime date = new DateTime(startDate.Year, startDate.Month, startDate.Day);
            //AppointmentsCountOnDays = new ObservableCollection<AppointmentsCountOnDay>();
            Dates = new ChartValues<string>();
            NumberOfAppointments = new ChartValues<int>();
            while (date.CompareTo(endDate) < 0)
            {
                var appointments = (List<ScheduledAppointment>)app.ScheduledAppointmentController.GetFromToDatesForRoom(date, date.AddDays(1), Room.Id);
                //var appointmentCountOnDay = new AppointmentsCountOnDay(date, appointments.Count);
                //AppointmentsCountOnDays.Add(appointmentCountOnDay);
                Dates.Add(date.ToString("dd.MM.yyyy"));
                NumberOfAppointments.Add(appointments.Count);
                date = date.AddDays(1);
            }
        }

        private void PrintButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
