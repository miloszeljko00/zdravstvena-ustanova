using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    /// Interaction logic for AddMedicineToTherapy.xaml
    /// </summary>
    public partial class AddMedicineToTherapy : Window
    {
        public ObservableCollection<PrescribedMedicine> PrescribedMedicine { get; set; }
        public ObservableCollection<Medication> Medications { get; set; }
        public AddMedicineToTherapy(ObservableCollection<PrescribedMedicine> pm)
        {
            InitializeComponent();
            DataContext = this;
            var app = Application.Current as App;
            Medications = new ObservableCollection<Medication>(app.MedicationController.GetAll());
            medComboBox.ItemsSource = Medications;
            PrescribedMedicine = pm;

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Medication med = (Medication)medComboBox.SelectedItem;
            serial.Text = med.Id.ToString();
        }

        private void createPrescribedMedicine(object sender, RoutedEventArgs e)
        {
            //public PrescribedMedicine(long id, int timesPerDay, int onHours, DateTime endDate, string description, Medication medication)
            Medication med = (Medication)medComboBox.SelectedItem;
            int tpd = int.Parse(timesPerDay.Text);
            int oh = int.Parse(onHours.Text);
            DateTime ed = (DateTime)endDate.SelectedDate;
            string desc = description.Text;
            var app = Application.Current as App;
            PrescribedMedicine.Add(new PrescribedMedicine(0, tpd, oh, ed, desc, med));
            this.Close();


        }
    }
}
