using zdravstvena_ustanova.Model;
using System.Windows;

namespace zdravstvena_ustanova.View
{
    public partial class PrescribedMedicineDetails : Window
    {
        public PrescribedMedicine PrescribedMedicine { get; set; }
        public PrescribedMedicineDetails(PrescribedMedicine preMed)

        {
            InitializeComponent();
            PrescribedMedicine = preMed;
            name.Text = PrescribedMedicine.Medication.Name;
            details.Text = "Broj dnevnih doza: " + PrescribedMedicine.TimesPerDay + "\n";
            details.Text += "Razmak izmedju doza (u satima): " + PrescribedMedicine.OnHours + "\n";
            details.Text += "Zavrsni datum upotrebe leka: " + PrescribedMedicine.EndDate.ToString("dd.MM.yyyy.") + "\n";
            details.Text += "Opis leka: " + PrescribedMedicine.Description;
        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
