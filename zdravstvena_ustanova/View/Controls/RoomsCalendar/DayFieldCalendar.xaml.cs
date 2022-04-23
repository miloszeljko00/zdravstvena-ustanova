using Model;
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

namespace zdravstvena_ustanova.View.Controls.RoomsCalendar
{
    /// <summary>
    /// Interaction logic for DayFieldCalendar.xaml
    /// </summary>
    public partial class DayFieldCalendar : UserControl
    {
        public int DayNumber { get; set; }
        public bool InCurrentMonth { get; set; }
        public DateTime Date { get; set; }

        public ObservableCollection<ScheduledAppointment> ScheduledAppointments { get; set; }

        public DayFieldCalendar(DateTime date, bool inCurrentMonth)
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;

            Date = date;
            DateTime firstAppointment = new DateTime(Date.Year, Date.Month, Date.Day, 8, 0, 0);
            DateTime lastAppointment = new DateTime(Date.Year, Date.Month, Date.Day, 21, 0, 0);

            this.DayNumber = Date.Day;
            this.InCurrentMonth = inCurrentMonth;
            if (!InCurrentMonth)
            {
                panel.Background = Brushes.LightGray;
                label.Foreground = Brushes.Gray;
                panel.Style = null;
            }
            else
            {
                UpdateScheduledAppointments(firstAppointment, lastAppointment);

                if (ScheduledAppointments.Count != 0)
                {
                    AppointmentsCountDisplay.Content = ScheduledAppointments.Count() + " termina";
                }
            }
  
            
        }
        private void UpdateScheduledAppointments(DateTime firstAppointment, DateTime lastAppointment)
        {
            var app = Application.Current as App;
            ScheduledAppointments = new ObservableCollection<ScheduledAppointment>
                (app.ScheduledAppointmentController.GetFromToDates(firstAppointment, lastAppointment));
        }

    }
}
