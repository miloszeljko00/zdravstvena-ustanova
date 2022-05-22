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
using zdravstvena_ustanova.View.Controls;

namespace zdravstvena_ustanova.View.Pages.ManagerPages
{
    public partial class MedicationPage : Page
    {
        public ObservableCollection<Medication> Medications { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        public MedicationPage()
        {
            InitializeComponent();
            DataContext = this;
            var app = Application.Current as App;

            Medications = new ObservableCollection<Medication>();
            Ingredients = new ObservableCollection<Ingredient>();

            foreach (var medication in app.MedicationController.GetAll())
            {
                Medications.Add(medication);
            }
        }

        private void RequestApproval_Click(object sender, RoutedEventArgs e)
        {
            if (MedicationDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Odaberi lek!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NavigationService.Navigate(new RequestMedicationApprovalPage((Medication)MedicationDataGrid.SelectedItem));
        }

        private void ActiveRequests_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ActiveMedicationApprovalRequestsOverviewPage());
        }

        private void FinishedRequests_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FinishedMedicationApprovalRequestsOverviewPage());

        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTextBox.Text == "")
            {
                // Create an ImageBrush.
                ImageBrush textImageBrush = new();
                textImageBrush.ImageSource =
                    new BitmapImage(
                        new Uri(App.ProjectPath + "/Resources/img/search-name.png")
                    );
                textImageBrush.AlignmentX = AlignmentX.Left;
                textImageBrush.Stretch = Stretch.None;

                // Use the brush to paint the button's background.
                SearchTextBox.Background = textImageBrush;
            }
            else
            {

                SearchTextBox.Background = null;
            }
        }

        private void AddRoomIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Modal.Content = new AddMedication(Medications);
            MainWindow.Modal.IsOpen = true;
        }
        private void EditRoomIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MedicationDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Odaberi lek!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MainWindow.Modal.Content = new EditMedication();
            MainWindow.Modal.IsOpen = true;
        }
        private void DeleteRoomIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MedicationDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Odaberi lek!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MainWindow.Modal.Content = new DeleteMedication();
            MainWindow.Modal.IsOpen = true;
        }

        private void IngredientsIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var ingredientsIcon = (Image)e.OriginalSource;
            var dataContext = ingredientsIcon.DataContext;
            var dataSource = (Medication)dataContext;

            Ingredients.Clear();
            foreach(var ingredient in dataSource.Ingredients)
            {
                Ingredients.Add(ingredient);
            }
        }
    }
}
