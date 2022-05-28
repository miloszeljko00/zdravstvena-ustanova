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
    /// Interaction logic for EditMedication.xaml
    /// </summary>
    public partial class EditMedication : UserControl
    {
        public Medication Medication { get; set; }
        public ObservableCollection<Medication>? Medications { get; set; }
        public ObservableCollection<MedicationType> MedicationTypes { get; set; }
        public EditMedication(ObservableCollection<Medication>? medications, Medication selectedMedication)
        {
            InitializeComponent();
            DataContext = this;
            Medication = selectedMedication;
            Medications = medications;
            
            MedicationTypes = new ObservableCollection<MedicationType>();
            var app = Application.Current as App;
            foreach (var medicationType in app.MedicationTypeController.GetAll())
            {
                MedicationTypes.Add(medicationType);
            }

            medicationQuantityTextBox.Text = Medication.Quantity.ToString();
            medicationNameTextBox.Text = Medication.Name;
            foreach (var medicationType in MedicationTypes)
            {
                if (medicationType.Id == Medication.MedicationType.Id)
                {
                    medicationTypeComboBox.SelectedItem = medicationType;
                }
            }

            if (Medication.IsApproved)
            {
                medicationNameTextBox.IsEnabled = false;
                medicationTypeComboBox.IsEnabled = false;
            }
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (medicationNameTextBox.Text == "" || medicationQuantityTextBox.Text == "" || medicationTypeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Popuni sva polja prvo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Medication.Name = medicationNameTextBox.Text;
            Medication.MedicationType = (MedicationType)medicationTypeComboBox.SelectedItem;
            Medication.Quantity = int.Parse(medicationQuantityTextBox.Text);

            MainWindow.Modal.Content = new EditMedicationPageTwo(Medications, Medication, this);
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }

        private void MedicationQuantityTextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
