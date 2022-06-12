using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using zdravstvena_ustanova.View.Pages.ManagerPages;

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
        public long RoomId { get; set; }

        public List<ScheduledAppointment> ScheduledAppointments { get; set; }
        public RenovationAppointment RenovationAppointment { get; set; }
        public RoomCalendarOverview? RoomCalendarOverview { get; set; }

        public DayFieldCalendar(DateTime date, bool inCurrentMonth, long roomId, RoomCalendarOverview roomCalendarOverview)
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;

            RoomId = roomId;
            Date = date;
            RoomCalendarOverview = roomCalendarOverview;

            DateTime firstAppointmentDate = new DateTime(Date.Year, Date.Month, Date.Day, 8, 0, 0);
            DateTime lastAppointmentDate = new DateTime(Date.Year, Date.Month, Date.Day, 21, 0, 0);

            this.DayNumber = Date.Day;
            this.InCurrentMonth = inCurrentMonth;
            if (!InCurrentMonth)
            {
                panel.Background = Brushes.LightGray;
                label.Foreground = Brushes.Gray;
                panel.Style = null;
                ScheduledAppointments = new List<ScheduledAppointment>();
            }
            else
            {
                UpdateScheduledAppointments(firstAppointmentDate, lastAppointmentDate);
                UpdateRenovationAppointments(firstAppointmentDate, lastAppointmentDate);

                if (ScheduledAppointments.Count != 0)
                {
                    if (ManagerMainPage.CurrentLanguage == "en-US")
                    {
                        AppointmentsCountDisplay.Content = ScheduledAppointments.Count() + " appointments";
                    }
                    else
                    {
                        AppointmentsCountDisplay.Content = ScheduledAppointments.Count() + " termina";
                    }
                }
                if(RenovationAppointment != null)
                {
                    AppointmentsCountDisplay.Visibility = Visibility.Collapsed;
                    label.Visibility = Visibility.Collapsed;
                    UnderConstruction.Visibility = Visibility.Visible;
                    panel.Background = Brushes.Red;
                }
            }
  
            
        }

        private void UpdateRenovationAppointments(DateTime firstAppointmentDate, DateTime lastAppointmentDate)
        {
            var app = Application.Current as App;
            RenovationAppointment = app.RenovationAppointmentController.GetIfContainsDateForRoom(Date, RoomId).FirstOrDefault();
        }

        private void UpdateScheduledAppointments(DateTime firstAppointment, DateTime lastAppointment)
        {
            var app = Application.Current as App;
            ScheduledAppointments = new List<ScheduledAppointment>
                (app.ScheduledAppointmentController.GetFromToDatesForRoom(firstAppointment, lastAppointment, RoomId));
        }

        private void panel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!InCurrentMonth) return;

            WrapPanel wrapPanel = (WrapPanel)Parent;
            foreach (DayFieldCalendar dayFieldCalendar in wrapPanel.Children)
            {
                if (dayFieldCalendar.panel.Background == Brushes.Gray)
                {
                    dayFieldCalendar.panel.Background = null;
                    
                    if (dayFieldCalendar.ScheduledAppointments.Count != 0)
                    {
                        dayFieldCalendar.AppointmentsCountDisplay.Content = dayFieldCalendar.ScheduledAppointments.Count() + " termina";
                    }
                    if (dayFieldCalendar.RenovationAppointment != null)
                    {
                        dayFieldCalendar.panel.Background = Brushes.Red;
                        if (ManagerMainPage.CurrentLanguage == "en-US")
                        {
                            dayFieldCalendar.AppointmentsCountDisplay.Content = "Renovation";
                        }
                        else
                        {
                            dayFieldCalendar.AppointmentsCountDisplay.Content = "Renoviranje";
                        }
                        dayFieldCalendar.AppointmentsCountDisplay.Foreground = Brushes.White;
                        dayFieldCalendar.label.Foreground = Brushes.White;
                    }
                }
            }
            panel.Background = Brushes.Gray;
            if (ScheduledAppointments.Count != 0) 
            {
                RoomCalendarOverview.ScheduledAppointments = new ObservableCollection<ScheduledAppointment>(ScheduledAppointments);
                
            }
            if (RenovationAppointment != null) 
            {
                RoomCalendarOverview.SelectedRenovationAppointment = RenovationAppointment;
                RoomCalendarOverview.IsRenovationSelected = true;
                RoomCalendarOverview.RenovationAppointment = RenovationAppointment;
                RoomCalendarOverview.infoPanel.Children.Clear();
                RoomCalendarOverview.infoPanel.Children.Add(new RenovationInfoControl(RoomCalendarOverview));
            }
            else
            {
                RoomCalendarOverview.SelectedRenovationAppointment = null;
                RoomCalendarOverview.IsRenovationSelected = false;
                RoomCalendarOverview.infoPanel.Children.Clear();
                RoomCalendarOverview.infoPanel.Children.Add(new SelectedScheduledAppointmentsListControl(RoomCalendarOverview));
            }
            
        }
    }
}
