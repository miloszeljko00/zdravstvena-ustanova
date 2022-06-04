using zdravstvena_ustanova.Model;
using System;
using System.Windows;
using System.Windows.Threading;

namespace zdravstvena_ustanova.View
{
    public partial class ManageAppointment : Window
    {
        public ScheduledAppointment ScheduledAppointment { get; set; }
        public DispatcherTimer DemoTimer { get; private set; }
        public int Phase { get; set; }
        public bool Demo { get; set; }
        public ManageAppointment(ScheduledAppointment sa)
        {
            InitializeComponent();
            ScheduledAppointment = sa;
            var app = Application.Current as App;
            Doctor doc = app.DoctorController.GetById(ScheduledAppointment.Doctor.Id);
            doctor.Text = doc.Name + " " + doc.Surname;
            time.Text = ScheduledAppointment.Start.Hour + ":00";
            if(!(DateTime.Compare(ScheduledAppointment.Start.Date,DateTime.Now.Date)<=0 || DateTime.Compare(ScheduledAppointment.Start.Date, DateTime.Now.AddDays(1).Date) == 0))
            {
                change.IsEnabled = true;
                delete.IsEnabled = true;
            }
            else
            {
                change.IsEnabled = false;
                delete.IsEnabled = false;
            }
        }
        public ManageAppointment(ScheduledAppointment sa, bool isDemo, int phase)
        {
            InitializeComponent();
            ScheduledAppointment = sa;
            var app = Application.Current as App;
            Doctor doc = app.DoctorController.GetById(ScheduledAppointment.Doctor.Id);
            doctor.Text = doc.Name + " " + doc.Surname;
            time.Text = ScheduledAppointment.Start.Hour + ":00";
            if (!(DateTime.Compare(ScheduledAppointment.Start.Date, DateTime.Now.Date) <= 0 || DateTime.Compare(ScheduledAppointment.Start.Date, DateTime.Now.AddDays(1).Date) == 0))
            {
                change.IsEnabled = true;
                delete.IsEnabled = true;
            }
            else
            {
                change.IsEnabled = false;
                delete.IsEnabled = false;
            }

            Demo = isDemo;
            Phase = phase;
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
                    change.Focus();
                    Phase++;
                    break;
                case 1:
                    this.Close();
                    ChangeAppointment ca = new ChangeAppointment(ScheduledAppointment, Demo);
                    ca.ShowDialog();
                    Phase += 3;
                    break;
                case 2:
                    delete.Focus();
                    Phase++;
                    break;
                case 3:
                    DeleteAppointment da = new DeleteAppointment(this, ScheduledAppointment, Demo);
                    da.ShowDialog();
                    Phase++;
                    break;
                default:
                    DemoTimer.IsEnabled = false;
                    break;
            }
        }
        private void goToChangeAppointment(object sender, RoutedEventArgs e)

        {
            this.Close();
            ChangeAppointment ca = new ChangeAppointment(ScheduledAppointment);
            ca.ShowDialog();
        }

        private void goToDeleteAppointment(object sender, RoutedEventArgs e)
        {
            DeleteAppointment da = new DeleteAppointment(this, ScheduledAppointment);
            da.ShowDialog();
        }

        private void goToAppointments(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
