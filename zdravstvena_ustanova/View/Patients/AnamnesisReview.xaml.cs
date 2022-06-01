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
        public string Doctor { get; set; }

        public AnamnesisInfo()
        {

        }
    }
    public partial class AnamnesisReview : UserControl
    {
        public ObservableCollection<AnamnesisInfo> AnamnesisInfos { get; set; }
        public ObservableCollection<AnamnesisInfo> FilteredInfos { get; set; }
        public AnamnesisReview()
        {
            InitializeComponent();
            AnamnesisInfos = new ObservableCollection<AnamnesisInfo>();
            var app = Application.Current as App;
            List<Anamnesis> anamnesis = new List<Anamnesis>(app.HealthRecordController.GetAnamnesisForPatient(app.LoggedInUser.Id));
            foreach (Anamnesis a in anamnesis)
            {
                AnamnesisInfo aInfo = new AnamnesisInfo();
                aInfo.Anamnesis = a;
                AnamnesisInfos.Add(aInfo);
            }
            foreach (AnamnesisInfo ai in AnamnesisInfos)
            {
                ai.ScheduledAppointment = app.MedicalExaminationController.GetScheduledAppointmentForAnamnesis(ai.Anamnesis.Id);
            }
            foreach (AnamnesisInfo ai in AnamnesisInfos)
            {
                ai.Doctor = "dr " + ai.ScheduledAppointment.Doctor.Name + " " + ai.ScheduledAppointment.Doctor.Surname;
            }
            anamnesisList.ItemsSource = AnamnesisInfos;
        }

        private void enterDetails(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space && anamnesisList.SelectedIndex != -1)
            {
                AnamnesisDetails a = new AnamnesisDetails(((AnamnesisInfo)anamnesisList.SelectedItem).Anamnesis);
                a.ShowDialog();
            }
        }

        private void search(object sender, RoutedEventArgs e)
        {
            FilteredInfos = new ObservableCollection<AnamnesisInfo>();
            if (trazi.Text == "")
                anamnesisList.ItemsSource = AnamnesisInfos;
            else
            {
                foreach (AnamnesisInfo ai in AnamnesisInfos)
                {
                    if (ai.Doctor.ToLower().Contains(trazi.Text.ToLower()) || ai.ScheduledAppointment.Start.ToString().ToLower().StartsWith(trazi.Text.ToLower()))
                        FilteredInfos.Add(ai);
                }
                anamnesisList.ItemsSource = FilteredInfos;
            }

        }
    }
}
