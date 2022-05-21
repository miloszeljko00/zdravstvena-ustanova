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
    public partial class AddMedication : Page
    {
        public Medication Medication { get; set; }
        public ObservableCollection<MedicationType> MedicationTypes { get; set; }
        public AddMedication()
        {
            InitializeComponent();
            Medication = new Medication(-1);
            Medication.IsApproved = false;
            Medication.Ingredients = new List<Ingredient>();
           
            //TODO prodji kroz roomtypes i dodaj ih u combobox

            foreach (var roomType in roomTypes)
            {
                RoomTypes.Add(roomType);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if(medicationNameTextBox.Text == "" || medicationQuantityTextBox.Text == "" || medicationTypeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Popuni sva polja prvo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            MainWindow.Modal.Content = new AddMedicationPageTwo(Medication);
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
