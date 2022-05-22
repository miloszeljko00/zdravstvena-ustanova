using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.View.Controls
{
    /// <summary>
    /// Interaction logic for AddMedication.xaml
    /// </summary>
    public partial class AddMedication : UserControl
    {
        public Medication Medication { get; set; }
        public ObservableCollection<Medication> Medications { get; set; }
        public ObservableCollection<MedicationType> MedicationTypes { get; set; }
        public AddMedication(ObservableCollection<Medication> medications)
        {
            InitializeComponent();
            DataContext = this;
            Medication = new Medication(-1);
            Medications = medications;
            Medication.IsApproved = false;
            Medication.Ingredients = new List<Ingredient>();

            //TODO prodji kroz roomtypes i dodaj ih u combobox

            MedicationTypes = new ObservableCollection<MedicationType>();
            var app = Application.Current as App;
            foreach(var medicationType in app.MedicationTypeController.GetAll())
            {
                MedicationTypes.Add(medicationType);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if(medicationNameTextBox.Text == "" || medicationQuantityTextBox.Text == "" || medicationTypeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Popuni sva polja prvo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Medication.Name = medicationNameTextBox.Text;
            Medication.MedicationType = (MedicationType)medicationTypeComboBox.SelectedItem;
            Medication.Quantity = int.Parse(medicationQuantityTextBox.Text);

            MainWindow.Modal.Content = new AddMedicationPageTwo(Medication, Medications, this);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }

        private void medicationQuantityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
