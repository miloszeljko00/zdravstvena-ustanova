using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Input;

namespace zdravstvena_ustanova.View
{
    public partial class Appointments : UserControl
    {
        public List<ScheduledAppointment> ScheduledAppointments { get; set; }
        public List<DateTime> Dates { get; set; }
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

        private void refresh()
        {
            Dictionary<string, Color> datesToColor = new Dictionary<string, Color>();
            var app = Application.Current as App;
            Dates = new List<DateTime>();
            ScheduledAppointments = new List<ScheduledAppointment>(app.ScheduledAppointmentController.GetScheduledAppointmentsForPatient(app.LoggedInUser.Id));
            foreach (ScheduledAppointment sa in ScheduledAppointments)
            {
                Dates.Add(sa.Start);
                datesToColor.Add(sa.Start.Date.ToString("MM/dd/yyyy"), Colors.Red);
            }


            Style style = new Style(typeof(CalendarDayButton));

            foreach (KeyValuePair<string, Color> item in datesToColor)
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

        private void goToManageAppointment(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var app = Application.Current as App;
            if (calendar.SelectedDate != null && e.Key == Key.Space)
            {
                ManageAppointment ma = new ManageAppointment(app.ScheduledAppointmentController.GetScheduledAppointmentsForDate((DateTime)calendar.SelectedDate, app.LoggedInUser.Id));
                ma.ShowDialog(); 
            }
            calendar.SelectedDate = null;
            this.refresh();
        }
    }
}
