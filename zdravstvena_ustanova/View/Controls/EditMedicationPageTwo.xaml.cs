using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.View.Controls
{
    /// <summary>
    /// Interaction logic for EditMedicationPageTwo.xaml
    /// </summary>
    public partial class EditMedicationPageTwo : UserControl
    {
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        public Medication Medication { get; set; }
        public ObservableCollection<Medication> Medications { get; set; }
        public EditMedication EditMedication { get; set; }

        public EditMedicationPageTwo(ObservableCollection<Medication> medications, Medication selectedMedication, EditMedication editMedication)
        {
            InitializeComponent(); 
            DataContext = this;
            Ingredients = new ObservableCollection<Ingredient>();
            Medication = selectedMedication;
            Medications = medications;
            EditMedication = editMedication;
            foreach (var ingredient in Medication.Ingredients)
            {
                Ingredients.Add(ingredient);
            }

            if (Medication.IsApproved)
            {
                ingredientNameTextBox.IsEnabled = false;
                AddIngredientsIcon.IsEnabled = false;
                RemoveIngredientsIcon.IsEnabled = false;
            }
        }

        private void RemoveIngredientsIcon_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var app = Application.Current as App;
            Ingredient ingredient = (Ingredient)IngredientsListView.SelectedItem;
            if (ingredient == null)
            {
                MessageBox.Show("Odaberi sastojak!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Ingredients.Remove(ingredient);
        }

        private void AddIngredientsIcon_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var app = Application.Current as App;
            Ingredient ingredient = new Ingredient(-1, ingredientNameTextBox.Text);
            bool alreadyAdded = app.IngredientController.CheckIfItsAlreadyContained(Ingredients, ingredient);

            if (alreadyAdded)
            {
                MessageBox.Show("Vec dodat sastojak!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Ingredients.Add(ingredient);
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;

            Medication.Ingredients.Clear();
            foreach (var ingredient in Ingredients)
            {
                Medication.Ingredients.Add(ingredient);
            }
            app.IngredientController.CreateIfNotSavedWithSameName(Medication.Ingredients);
            app.MedicationController.Update(Medication);
            Medications.Remove(Medication);
            Medications.Add(Medication);
            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow.Modal.Content = EditMedication;
        }
    }
}
