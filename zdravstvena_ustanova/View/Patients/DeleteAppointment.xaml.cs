using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace zdravstvena_ustanova.View
{
   public partial class DeleteAppointment : Window
    {
        private Window parent;
        public ScheduledAppointment ScheduledAppointment { get; set; }
        public DeleteAppointment(Window parent, ScheduledAppointment sa)
        {
            InitializeComponent();
            this.parent = parent;
            ScheduledAppointment = sa;
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
