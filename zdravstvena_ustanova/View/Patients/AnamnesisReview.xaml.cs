using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace zdravstvena_ustanova.View
{
    public class AnamnesisInfo
    {
        public Anamnesis Anamnesis { get; set; }
        public ScheduledAppointment ScheduledAppointment { get; set; }
        public string Doctors { get; set; }

        public AnamnesisInfo()
        {

        }
    }
    public partial class AnamnesisReview : UserControl
    {
        public ObservableCollection<AnamnesisInfo> ai { get; set; }
        public ObservableCollection<AnamnesisInfo> filtered { get; set; }
        public AnamnesisReview()
        {
            InitializeComponent();
            ai = new ObservableCollection<AnamnesisInfo>();
            var app = Application.Current as App;
            List<HealthRecord> hrs = new List<HealthRecord>(app.HealthRecordController.GetAll());
            List<MedicalExamination> mes = new List<MedicalExamination>(app.MedicalExaminationController.GetAll());
            foreach (HealthRecord hr in hrs)
            {
                if (app.LoggedInUser.Id == hr.Patient.Id)
                {
                    foreach (Anamnesis a in hr.Anamnesis)
                    {
                        AnamnesisInfo aInfo = new AnamnesisInfo();
                        aInfo.Anamnesis = a;
                        ai.Add(aInfo);
                    }
                }
            }
            foreach (AnamnesisInfo aInfo in ai)
            {
                foreach(MedicalExamination me in mes)
                {
                    if(me.Anamnesis.Id == aInfo.Anamnesis.Id)
                    {
                        aInfo.ScheduledAppointment = me.ScheduledAppointment;
                    }
                }
            }
            foreach (AnamnesisInfo aInfo in ai)
            {
                aInfo.Doctors = "dr " + aInfo.ScheduledAppointment.Doctor.Name + " " + aInfo.ScheduledAppointment.Doctor.Surname;
            }

            anamnesisList.ItemsSource = ai;
        }

        private void entered(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space && anamnesisList.SelectedIndex != -1)
            {
                AnamnesisDetails a = new AnamnesisDetails(((AnamnesisInfo)anamnesisList.SelectedItem).Anamnesis);
                a.ShowDialog();
            }
        }

        private void pretraga(object sender, RoutedEventArgs e)
        {
            filtered = new ObservableCollection<AnamnesisInfo>();
            if (trazi.Text == "")
                anamnesisList.ItemsSource = ai;
            else
            {
                foreach (AnamnesisInfo aInfo in ai)
                {
                    if (aInfo.Doctors.ToLower().Contains(trazi.Text.ToLower()) || aInfo.ScheduledAppointment.Start.ToString().ToLower().StartsWith(trazi.Text.ToLower()))
                        filtered.Add(aInfo);
                }
                anamnesisList.ItemsSource = filtered;
            }

        }
    }
}
