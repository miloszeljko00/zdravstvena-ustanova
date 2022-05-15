using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace zdravstvena_ustanova.View
{
    /// <summary>
    /// Interaction logic for DeleteAppointment.xaml
    /// </summary>
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

            List<AntiTrollMechanism> atms = new List<AntiTrollMechanism>(app.AntiTrollMechanismController.GetAll());
            List<Account> acc = new List<Account>(app.AccountController.GetAll());
            bool found = false;

            if (atms.Count == 0)
            {
                List<DateTime> date = new List<DateTime>();
                date.Add(DateTime.Now);
                AntiTrollMechanism antiTrollMechanism = app.AntiTrollMechanismController.Create(new AntiTrollMechanism(0, app.LoggedInUser.Id, 1, date));
            }
            else
            {
                foreach (AntiTrollMechanism atm in atms)
                {
                    if (app.LoggedInUser.Id == atm.Patient.Id && atm.NumberOfDates < 5)
                    {
                        atm.NumberOfDates++;
                        atm.DatesOfCanceledAppointments.Add(DateTime.Now);
                        app.AntiTrollMechanismController.Update(atm);
                        found = true;
                        if (atm.NumberOfDates == 5 && (atm.DatesOfCanceledAppointments[4] - atm.DatesOfCanceledAppointments[0]).TotalDays <= 30)
                        {
                            Environment.Exit(0);
                            found = true;
                        }
                    }
                    else if (app.LoggedInUser.Id == atm.Patient.Id && atm.NumberOfDates == 5)
                    {
                        atm.DatesOfCanceledAppointments[0] = atm.DatesOfCanceledAppointments[1];
                        atm.DatesOfCanceledAppointments[1] = atm.DatesOfCanceledAppointments[2];
                        atm.DatesOfCanceledAppointments[2] = atm.DatesOfCanceledAppointments[3];
                        atm.DatesOfCanceledAppointments[3] = atm.DatesOfCanceledAppointments[4];
                        atm.DatesOfCanceledAppointments[4] = DateTime.Now;
                        app.AntiTrollMechanismController.Update(atm);
                        found = true;
                        if ((atm.DatesOfCanceledAppointments[4] - atm.DatesOfCanceledAppointments[0]).TotalDays <= 30)
                        {
                            Environment.Exit(0);
                            found = true;
                        }
                    }
                }
                if (!found)
                {
                    List<DateTime> date = new List<DateTime>();
                    date.Add(DateTime.Now);
                    AntiTrollMechanism antiTrollMechanism = app.AntiTrollMechanismController.Create(new AntiTrollMechanism(0, app.LoggedInUser.Id, 1, date));
                }
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
