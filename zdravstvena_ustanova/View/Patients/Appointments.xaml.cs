using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Threading;

namespace zdravstvena_ustanova.View
{
    public partial class Appointments : UserControl
    {
        public List<ScheduledAppointment> ScheduledAppointments { get; set; }
        public List<DateTime> Dates { get; set; }
        public DispatcherTimer DemoTimer { get; private set; } = new DispatcherTimer();
        public int Phase { get; set; }
        public bool Demo { get; set; }

        public Appointments()
        {
            InitializeComponent();
            this.refresh();
        }

        public Appointments(bool isDemo)
        {
            InitializeComponent();
            this.refresh();
            Demo = isDemo;
            Phase = 0;
            DemoTimer.Interval = new TimeSpan(0, 0, 2);
            DemoTimer.Tick += new EventHandler(demoTimer_Tick);
            DemoTimer.IsEnabled = true;
        }

        private void demoTimer_Tick(object sender, EventArgs e)
        {
            if (Demo)
                DemoTimer.IsEnabled = true;
            else 
            { 
                DemoTimer.IsEnabled = false;
                return;
            }
            var app = Application.Current as App;
            switch (Phase)
            {
                case 0:
                    link1.Focus();
                    Phase++;
                    break;
                case 1:
                    TimePriority tp = new TimePriority(Demo);
                    tp.ShowDialog();
                    this.refresh();
                    Phase++;
                    break;
                case 2:
                    link2.Focus();
                    Phase++;
                    break;
                case 3:
                    DoctorPriority dp = new DoctorPriority(Demo);
                    dp.ShowDialog();
                    this.refresh();
                    Phase++;
                    break;
                case 4:
                    calendar.SelectedDate = Convert.ToDateTime("29.6.2022.");
                    calendar.Focus();
                    Phase++;
                    break;
                case 5:
                    ManageAppointment ma1 = new ManageAppointment(app.ScheduledAppointmentController.GetScheduledAppointmentsForDate((DateTime)calendar.SelectedDate, app.LoggedInUser.Id), Demo, 0);
                    ma1.ShowDialog();
                    calendar.SelectedDate = null;
                    this.refresh();
                    Phase++;
                    break;
                case 6:
                    calendar.SelectedDate = Convert.ToDateTime("30.6.2022.");
                    calendar.Focus();
                    Phase++;
                    break;
                case 7:
                    ManageAppointment ma2 = new ManageAppointment(app.ScheduledAppointmentController.GetScheduledAppointmentsForDate((DateTime)calendar.SelectedDate, app.LoggedInUser.Id), Demo, 2);
                    ma2.ShowDialog();
                    calendar.SelectedDate = null;
                    this.refresh();
                    Phase++;
                    break;
                default:
                    Phase = 0;
                    break;
            }
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

        private void isEsc(object sender, KeyEventArgs e)
        {
            Demo = false;
            var app = Application.Current as App;
            List<ScheduledAppointment> scheduledAppointments = new List<ScheduledAppointment>(app.ScheduledAppointmentController.GetAll());
            if (scheduledAppointments[scheduledAppointments.Count - 1].Start.ToString().Contains("29.6.2022.") || scheduledAppointments[scheduledAppointments.Count - 1].Start.ToString().Contains("30.6.2022."))
                app.ScheduledAppointmentController.Delete(scheduledAppointments[scheduledAppointments.Count - 1].Id);
            this.refresh();
        }
    }
}
