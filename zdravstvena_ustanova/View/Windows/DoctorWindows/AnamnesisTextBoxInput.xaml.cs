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

namespace zdravstvena_ustanova.View.Windows.DoctorWindows
{
    /// <summary>
    /// Interaction logic for AnamnesisDiagnosis.xaml
    /// </summary>
    public partial class AnamnesisTextBoxInput : Window
    {
        public ScheduledAppointmentWindow ScheduledAppointmentWindow { get; set; }
        public AnamnesisTextBoxInput(ScheduledAppointmentWindow scheduledAppointmentWindow, string title)
        {
            InitializeComponent();
            ScheduledAppointmentWindow = scheduledAppointmentWindow;
            this.Title = title;
            if (this.Title.Equals("Anamnesis Diagnosis"))
            {
                TextBoxInput.Text = ScheduledAppointmentWindow.Anamnesis.Diagnosis;
            }
            else if (this.Title.Equals("Anamnesis Conclusion"))
            {
                TextBoxInput.Text = ScheduledAppointmentWindow.Anamnesis.Conclusion;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            MessageBoxResult answer = MessageBox.Show("Da li ste sigurni da zelite da zatvorite prozor?", "Zatvaranje anamneze", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
      
                this.Close();
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(this.Title.Equals("Anamnesis Diagnosis"))
            {
                ScheduledAppointmentWindow.Anamnesis.Diagnosis = new string(TextBoxInput.Text);
                Close();
            }
            else if(this.Title.Equals("Anamnesis Conclusion"))
            {
                ScheduledAppointmentWindow.Anamnesis.Conclusion = new string(TextBoxInput.Text);
                Close();
            }
        }
    }
}
