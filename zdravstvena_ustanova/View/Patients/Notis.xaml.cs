using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace zdravstvena_ustanova.View
{
    /// <summary>
    /// Interaction logic for Notis.xaml
    /// </summary>
    public partial class Notis : UserControl
    {
        public ObservableCollection<PrescribedMedicine> pm { get; set; }
        public Notis()
        {
            InitializeComponent();
            pm = new ObservableCollection<PrescribedMedicine>();
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
            therapyList.ItemsSource = pm;
        }

        private void entered(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PrescribedMedicineDetails pmd = new PrescribedMedicineDetails((PrescribedMedicine)therapyList.SelectedItem);
                pmd.ShowDialog();
            }
        }
    }
}
