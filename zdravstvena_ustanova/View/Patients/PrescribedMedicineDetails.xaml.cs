using Model;
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
    /// Interaction logic for PrescribedMedicineDetails.xaml
    /// </summary>
    public partial class PrescribedMedicineDetails : Window
    {
        public PrescribedMedicine pm { get; set; }
        public PrescribedMedicineDetails(PrescribedMedicine preMed)

        {
            InitializeComponent();
            pm = preMed;
            name.Text = pm.Medication.Name;
            details.Text = "Broj dnevnih doza: " + pm.TimesPerDay + "\n";
            details.Text += "Razmak izmedju doza (u satima): " + pm.OnHours + "\n";
            details.Text += "Zavrsni datum upotrebe leka: " + pm.EndDate.ToString("dd.MM.yyyy.") + "\n";
            details.Text += "Opis leka: " + pm.Description;
        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
