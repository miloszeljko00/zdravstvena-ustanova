using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace zdravstvena_ustanova.View
{
   public partial class DeleteAppointment : Window
    {
        private Window parent;
        public ScheduledAppointment ScheduledAppointment { get; set; }
        public DispatcherTimer DemoTimer { get; private set; }
        public int Phase { get; set; }
        public bool Demo { get; set; }
        public DeleteAppointment(Window parent, ScheduledAppointment sa)
        {
            InitializeComponent();
            this.parent = parent;
            ScheduledAppointment = sa;
        }
        public DeleteAppointment(Window parent, ScheduledAppointment sa, bool isDemo)
        {
            InitializeComponent();
            this.parent = parent;
            ScheduledAppointment = sa;

            Demo = isDemo;
            Phase = 0;
            DemoTimer = new DispatcherTimer();
            DemoTimer.Interval = new TimeSpan(0, 0, 2);
            DemoTimer.IsEnabled = true;
            DemoTimer.Tick += new EventHandler(demoTimer_Tick);
        }
        private void demoTimer_Tick(object sender, EventArgs e)
        {
            switch (Phase)
            {
                case 0:
                    yes.Focus();
                    Phase++;
                    break;
                case 1:
                    var app = Application.Current as App;
                    app.ScheduledAppointmentController.Delete(ScheduledAppointment.Id);
                    this.Close();
                    this.parent.Close();
                    Phase++;
                    break;
                default:
                    DemoTimer.IsEnabled = false;
                    break;
            }
        }
        private void goToAppointments(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;

            AntiTrollMechanism atm = app.AntiTrollMechanismController.GetAntiTrollMechanismByPatient(app.LoggedInUser.Id);

            if (atm == null)
            {
                List<DateTime> date = new List<DateTime>();
                date.Add(DateTime.Now);
                AntiTrollMechanism antiTrollMechanism = app.AntiTrollMechanismController.Create(new AntiTrollMechanism(0, app.LoggedInUser.Id, 1, date));
            }
            else
            {
                app.AntiTrollMechanismController.Update(atm);
            }
            app.ScheduledAppointmentController.Delete(ScheduledAppointment.Id);
            this.Close();
            this.parent.Close();
        }

        private void goToManageAppointment(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
