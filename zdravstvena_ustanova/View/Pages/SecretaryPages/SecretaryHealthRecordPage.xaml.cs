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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;

namespace zdravstvena_ustanova.View.Pages.SecretaryPages
{
    /// <summary>
    /// Interaction logic for SecretaryHealthRecordPage.xaml
    /// </summary>
    public partial class SecretaryHealthRecordPage : Page
    {
        private Patient patient;
        public SecretaryHealthRecordPage(Patient patient)
        {
            InitializeComponent();
            this.patient = patient;
            patientTB.Text = patient.Name + " " + patient.Surname;
        }
    }
}
