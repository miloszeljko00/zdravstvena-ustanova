using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;
using System.Globalization;

namespace zdravstvena_ustanova.View
{
    public partial class PatientMainWindow : Window
    {
        public DispatcherTimer NotiTimer { get; private set; }
        public DispatcherTimer NoteTimer { get; private set; }
        public PatientMainWindow()
        {
            InitializeComponent();
            CultureInfo provider = CultureInfo.InvariantCulture;
            NotiTimer = new DispatcherTimer();
            NoteTimer = new DispatcherTimer();
            NotiTimer.Interval = new TimeSpan(0,1,0);
            NotiTimer.IsEnabled = true;
            NotiTimer.Tick += new EventHandler(notiTimer_Tick);
            NoteTimer.Interval = new TimeSpan(0,1,0);
            NoteTimer.IsEnabled = true;
            NoteTimer.Tick += new EventHandler(noteTimer_Tick);


        }
        private void noteTimer_Tick(object sender, EventArgs e)
        {
            NoteTimer.IsEnabled = false;
            var app = Application.Current as App;
            DateTime now = DateTime.Now;
            List<Note> notes = new List<Note>(app.NoteController.GetNotesByPatient(app.LoggedInUser.Id));
            foreach (Note n in notes)
            {
                var t = (n.Time.StartsWith("0")) ? n.Time.Substring(1) : n.Time;
                if (t.Equals(now.Hour + ":" + ((now.Minute.ToString().Length == 1) ? ("0" + now.Minute) : now.Minute)))
                {
                    NoteNoti nn = new NoteNoti(n);
                    nn.ShowDialog();
                }
            }
            NoteTimer.IsEnabled = true;
        }

        private void notiTimer_Tick(object sender, EventArgs e)
        {
            NotiTimer.IsEnabled = false;
            ObservableCollection<PrescribedMedicine> pm = new ObservableCollection<PrescribedMedicine>();
            var app = Application.Current as App;
            List<MedicalExamination> medicalExaminations = new List<MedicalExamination>(app.MedicalExaminationController.GetAll());
            List<ScheduledAppointment> scheduledAppointments = new List<ScheduledAppointment>(app.ScheduledAppointmentController.GetScheduledAppointmentsForPatient(app.LoggedInUser.Id));
            foreach (MedicalExamination me in medicalExaminations)
            {
                foreach (ScheduledAppointment sa in scheduledAppointments)
                {
                    if (me.ScheduledAppointment.Id == sa.Id)
                    {
                        foreach (PrescribedMedicine preMed in me.PrescribedMedicine)
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
            NotiTimer.IsEnabled = true;
        }

        private void goToAppointments(object sender, RoutedEventArgs e)
        {
            this.content.Content = new Appointments();
        }

        private void goToLogin(object sender, RoutedEventArgs e)
        {
            NotiTimer.IsEnabled = false;
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
