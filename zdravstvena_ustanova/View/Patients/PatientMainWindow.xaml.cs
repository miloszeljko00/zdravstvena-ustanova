using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;
using zdravstvena_ustanova.View.Pages;
using System.Globalization;

namespace zdravstvena_ustanova.View
{
    public partial class PatientMainWindow : Window
    {
        public DispatcherTimer dt { get; private set; }
        public DispatcherTimer tim { get; private set; }
        public PatientMainWindow()
        {
            InitializeComponent();
            CultureInfo provider = CultureInfo.InvariantCulture;
            dt = new DispatcherTimer();
            tim = new DispatcherTimer();
            dt.Interval = new TimeSpan(0,1,0);
            dt.IsEnabled = true;
            dt.Tick += new EventHandler(dt_Tick);
            tim.Interval = new TimeSpan(0,1,0);
            tim.IsEnabled = true;
            tim.Tick += new EventHandler(tim_Tick);


        }
        private void tim_Tick(object sender, EventArgs e)
        {
            tim.IsEnabled = false;
            var app = Application.Current as App;
            DateTime now = DateTime.Now;
            List<Note> notes = new List<Note>(app.NoteController.GetAll());
            foreach (Note n in notes)
            {
                if (n.Patient.Id == app.LoggedInUser.Id)
                {
                    var t = (n.Time.StartsWith("0")) ? n.Time.Substring(1) : n.Time;
                    if (t.Equals(now.Hour + ":" + now.Minute))
                    {
                        NoteNoti nn = new NoteNoti(n);
                        nn.ShowDialog();
                    }
                }
            }
            tim.IsEnabled = true;
        }

        private void dt_Tick(object sender, EventArgs e)
        {
            dt.IsEnabled = false;
            ObservableCollection<PrescribedMedicine> pm = new ObservableCollection<PrescribedMedicine>();
            var app = Application.Current as App;
            List<MedicalExamination> me = new List<MedicalExamination>(app.MedicalExaminationController.GetAll());
            List<ScheduledAppointment> sa = new List<ScheduledAppointment>(app.ScheduledAppointmentController.GetAll());
            foreach (MedicalExamination medEx in me)
            {
                foreach (ScheduledAppointment scApp in sa)
                {
                    if (app.LoggedInUser.Id == scApp.Patient.Id && medEx.ScheduledAppointment.Id == scApp.Id)
                    {
                        foreach (PrescribedMedicine preMed in medEx.PrescribedMedicine)
                        {
                            pm.Add(preMed);
                        }
                    }
                }
            }
            foreach(PrescribedMedicine preMed in pm)
            {
                if (DateTime.Compare(preMed.EndDate, DateTime.Now.Date) >= 0)
                {
                    PrescribedMedicineDetails pmd = new PrescribedMedicineDetails((PrescribedMedicine)preMed);
                    pmd.ShowDialog();
                }
            }
            dt.IsEnabled = true;
        }

        private void goToAppointments(object sender, RoutedEventArgs e)
        {
            this.content.Content = new Appointments();
        }

        private void goToLogin(object sender, RoutedEventArgs e)
        {
            dt.IsEnabled = false;
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void goToHelp(object sender, RoutedEventArgs e)
        {
            this.content.Content=new Help();
        }

        private void goToNotis(object sender, RoutedEventArgs e)
        {
            this.content.Content = new Notis();
        }
        private void goToNotes(object sender, RoutedEventArgs e)
        {
            this.content.Content = new Notes();
        }


        private void goToSurveys(object sender, RoutedEventArgs e)
        {
            this.content.Content = new Surveys();
        }

        private void goToAnamnesisReview(object sender, RoutedEventArgs e)
        {
            this.content.Content = new AnamnesisReview();
        }

        private void goToJustification(object sender, RoutedEventArgs e)
        {
            this.content.Content = new Justification();
        }

        private void goToPatientAccount(object sender, RoutedEventArgs e)
        {
            this.content.Content = new PatientAccount();
        }

        private void goToHealthRecord(object sender, RoutedEventArgs e)
        {
            this.content.Content = new HealthRecordPatient();
        }
    }
}
