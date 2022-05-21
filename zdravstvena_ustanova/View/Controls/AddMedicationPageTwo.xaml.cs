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
    /// Interaction logic for AddMedicationPageTwo.xaml
    /// </summary>
    public partial class AddMedicationPageTwo : Page
    {
        ObservableCollection<Ingredient> Ingredients { get; set; }
        Medication Medication { get; set; }
        public AddMedicationPageTwo(Medication medication)
        {
            InitializeComponent();
            DataContext = this;
            Ingredients = new ObservableCollection<Ingredient>();
            Medication = medication;
        }

        private void AddIngredientsIcon_MouseDown(object sender, MouseButtonEventArgs e)
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

        private void RemoveIngredientsIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var app = Application.Current as App;
            Ingredient ingredient = (Ingredient)IngredientsListView.SelectedItem;
            if (ingredient == null)
            {
                MessageBox.Show("Vec dodat sastojak!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Ingredients.Remove(ingredient);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;

            foreach(var ingredient in Ingredients)
            {
                Medication.Ingredients.Add(ingredient);
            }
            app.IngredientController.CreateIfNotSavedWithSameName(Medication.Ingredients);
            app.MedicationController.Create(Medication);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
