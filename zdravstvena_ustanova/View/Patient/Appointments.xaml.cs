using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace zdravstvena_ustanova.View
{
    public partial class Appointments : UserControl
    {
        public List<ScheduledAppointment> ScheduledAppointments { get; set; }
        public List<DateTime> dates { get; set; }
        public Appointments()
        {
            InitializeComponent();
            this.refresh();
        }
        
        private void goToTimePriority(object sender, RoutedEventArgs e)
        {
            TimePriority tp = new TimePriority();
            tp.ShowDialog();
            this.refresh();
        }
        private void goToDoctorPriority(object sender, RoutedEventArgs e)
        {
            DoctorPriority dp = new DoctorPriority();
            dp.ShowDialog();
            this.refresh();
        }
        private void goToManageAppointment(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(calendar.SelectedDate != null) {
                foreach (ScheduledAppointment sa in ScheduledAppointments)
                {
                    if (sa.Start.Date.Equals((DateTime)calendar.SelectedDate))
                    {
                        ManageAppointment ma = new ManageAppointment(sa);
                        ma.ShowDialog();
                        break;
                    }
                }   
            }
            calendar.SelectedDate = null;
            this.refresh();
            
        }

        public void refresh()
        {
            Dictionary<string, Color> d = new Dictionary<string, Color>();
            var app = Application.Current as App;
            dates = new List<DateTime>();
            ScheduledAppointments = new List<ScheduledAppointment>();
            List<ScheduledAppointment> schApp = new List<ScheduledAppointment>(app.ScheduledAppointmentController.GetAll());
            foreach (ScheduledAppointment sa in schApp)
            {
                if (sa.Patient.Id == app.LoggedInUser.Id)
                {
                    ScheduledAppointments.Add(sa);
                    dates.Add(sa.Start);
                    d.Add(sa.Start.Date.ToString("MM/dd/yyyy"), Colors.Red);
                }
            }


            Style style = new Style(typeof(CalendarDayButton));

            foreach (KeyValuePair<string, Color> item in d)
            {
                DataTrigger trigger = new DataTrigger()
                {
                    Value = item.Key,
                    Binding = new Binding("Date")
                };
                trigger.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush(item.Value)));
                style.Triggers.Add(trigger);
            }

            calendar.CalendarDayButtonStyle = style;
        }
    }
}
