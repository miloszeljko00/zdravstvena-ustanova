using zdravstvena_ustanova.Model;
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
using zdravstvena_ustanova.Controller;

namespace zdravstvena_ustanova.View.Windows.DoctorWindows
{
    /// <summary>
    /// Interaction logic for AddMedicineToTherapy.xaml
    /// </summary>
    public partial class AddMedicineToTherapy : Window
    {
        public ObservableCollection<PrescribedMedicine> PrescribedMedicine { get; set; }
        public ScheduledAppointment ActiveScheduledAppointment { get; set; }
        public Patient PatientFromActiveAppointment { get; set; }
        public ObservableCollection<Medication> Medications { get; set; }
        public SolidColorBrush Brush { get; set; }
        public AddMedicineToTherapy(ObservableCollection<PrescribedMedicine> pm, ScheduledAppointment scheduledAppointment)
        {
            InitializeComponent();
            Brush = (SolidColorBrush)allergensAlertTextBox.BorderBrush;
            DataContext = this;
            var app = Application.Current as App;
            Medications = new ObservableCollection<Medication>(app.MedicationController.GetAll());
            medComboBox.ItemsSource = Medications;
            PrescribedMedicine = pm;
            ActiveScheduledAppointment = scheduledAppointment;
            PatientFromActiveAppointment = ActiveScheduledAppointment.Patient;
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var app = Application.Current as App;
            allergensAlertTextBox.Visibility = Visibility.Hidden;
            submitButton.IsEnabled = true;
            Medication med = (Medication)medComboBox.SelectedItem;
            medIdTextBox.Text = med.Id.ToString();
            HealthRecord healthRecordByPatientFromActiveAppointment = app.HealthRecordController.FindHealthRecordByPatient(PatientFromActiveAppointment.Id);
            List<Ingredient> ingredientsFromSelectedMedication = med.Ingredients;
            List<Allergens> allergensFromPatientHealthRecord = healthRecordByPatientFromActiveAppointment.Allergens;
            List<Ingredient> allIngredientsFromEveryPatientAllergen = new List<Ingredient>();
            foreach(Allergens a in allergensFromPatientHealthRecord)
            {
                foreach(Ingredient i in a.Ingredients)
                {
                    if (!allIngredientsFromEveryPatientAllergen.Contains(i))
                    {
                        allIngredientsFromEveryPatientAllergen.Add(i);
                    }
                }
            }

            foreach(Ingredient i in ingredientsFromSelectedMedication)
            {
                foreach(Ingredient i2 in allIngredientsFromEveryPatientAllergen)
                {
                    if (i2.Id == i.Id)
                    {
                        allergensAlertTextBox.Visibility = Visibility.Visible;
                        submitButton.IsEnabled = false;
                        return;
                    }
                }
            }
        }

        private void createPrescribedMedicine(object sender, RoutedEventArgs e)
        {
            Medication med = (Medication)medComboBox.SelectedItem;
            //if (medComboBox.SelectedItem == null)
            //{
            //    if (timesPerDay.Text == null || timesPerDay.Text == "" || onHours.Text == null || onHours.Text == "")
            //    {
            //        MessageBox.Show("Morate uneti sve obavezne podatke(medication,tbd,oh,endDate...)");
            //        return;
            //    }
            //    MessageBox.Show("Morate odabrati lek!");
            //    return;
            //}
            
            //if(timesPerDay.Text==null || timesPerDay.Text=="")
            //{
            //    if (onHours.Text == null || onHours.Text == "" || medComboBox.SelectedItem == null)
            //    {
            //        MessageBox.Show("Morate uneti sve obavezne podatke(medication,tbd,oh,endDate...)");
            //        return;
            //    } 
            //    MessageBox.Show("Morate uneti tpd podatak");
            //    return;
            //}
            string tpd = timesPerDay.Text;
            //if (onHours.Text == null || onHours.Text=="")
            //{
            //    if (timesPerDay.Text == null || timesPerDay.Text == "" || medComboBox.SelectedItem == null)
            //    {
            //        MessageBox.Show("Morate uneti sve obavezne podatke(medication,tbd,oh,endDate...)");
            //        return;
            //    }
            //    MessageBox.Show("Morate uneti oh podatak");
            //    return;
            //}
            string oh = onHours.Text;
            //if(((DateTime?)endDate.SelectedDate) == null)
            //{
            //    MessageBox.Show("Morate izabrati end date!");
            //    return;
            //}
            DateTime? ed = (DateTime?)endDate.SelectedDate;
            //if(ed.Year<DateTime.Now.Year)
            //{
            //    MessageBox.Show("Izabrali ste termin u proslosti!");
            //    return;
            //} else if(ed.Year == DateTime.Now.Year && ed.Month<DateTime.Now.Month)
            //{
            //    MessageBox.Show("Izabrali ste termin u proslosti!");
            //    return;
            //} else if(ed.Year == DateTime.Now.Year && ed.Month == DateTime.Now.Month && ed.Day<DateTime.Now.Day)
            //{
            //    MessageBox.Show("Izabrali ste termin u proslosti!");
            //    return;
            //}
            string desc=description.Text;
            //if(description.Text==null)
            //{
            //    desc = "";
            //} else
            //{
            //    desc = description.Text;
            //}
            var app = Application.Current as App;
            if(app.PrescribedMedicineController.ValidateParametersFromForm(med, tpd, oh, ed, desc))
            {
                PrescribedMedicine.Add(new PrescribedMedicine(-1, int.Parse(tpd), int.Parse(oh), (DateTime)ed, desc, med));
                this.Close();
            }

            /////////////////////////////////// fensi submit validacija ali ce biti dupliran i prljav kod
            if (string.IsNullOrEmpty(medComboBox.Text))
            {
                selectedMedicinePreventErrorTextBlock.Visibility = Visibility.Visible;
                medIdTextBox.BorderBrush = Brushes.Red;
                submitButton.IsEnabled = false;
            }
            else
            {
                selectedMedicinePreventErrorTextBlock.Visibility = Visibility.Hidden;
                medIdTextBox.BorderBrush = Brush;
                CheckIfCanEnableSubmitButton();
            }
            if (string.IsNullOrEmpty(timesPerDay.Text))
            {
                timesPerDay.BorderBrush = Brushes.Red;
                submitButton.IsEnabled = false;
            }
            else
            {
                timesPerDay.BorderBrush = Brush;
                CheckIfCanEnableSubmitButton();
            }
            if (string.IsNullOrEmpty(quantity.Text))
            {
                quantity.BorderBrush = Brushes.Red;
                submitButton.IsEnabled = false;
            }
            else
            {
                quantity.BorderBrush = Brush;
                CheckIfCanEnableSubmitButton();
            }
            if (string.IsNullOrEmpty(onHours.Text))
            {
                onHours.BorderBrush = Brushes.Red;
                submitButton.IsEnabled = false;
            }
            else
            {
                onHours.BorderBrush = Brush;
                CheckIfCanEnableSubmitButton();
            }
            if (endDate.SelectedDate < DateTime.Now || endDate.SelectedDate == null)
            {
                endDate.BorderBrush = Brushes.Red;
                endDate.ToolTip = "You must enter valid date!";
                endDatePreventErrorTextBlock.Visibility = Visibility.Visible;
                submitButton.IsEnabled = false;
            }
            else
            {
                endDate.BorderBrush = Brushes.Gray;
                endDate.ToolTip = "This field is required!";
                endDatePreventErrorTextBlock.Visibility = Visibility.Hidden;
                CheckIfCanEnableSubmitButton();
            }
            /////////////////////////////////////////////

        }

        private void Button_Click_Cancel_Adding_Medicine_In_Therapy(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Are you sure you want to undo the changes?", "Changing terapy", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void medComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(medComboBox.Text))
            {
                selectedMedicinePreventErrorTextBlock.Visibility = Visibility.Visible;
                medIdTextBox.BorderBrush = Brushes.Red;
                submitButton.IsEnabled = false;
            }
            else
            {
                selectedMedicinePreventErrorTextBlock.Visibility = Visibility.Hidden;
                medIdTextBox.BorderBrush = Brush;
                CheckIfCanEnableSubmitButton();
            }
        }

        private void timesPerDay_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(timesPerDay.Text))
            {
                timesPerDay.BorderBrush = Brushes.Red;
                submitButton.IsEnabled = false;
            }
            else
            {
                timesPerDay.BorderBrush = Brush;
                CheckIfCanEnableSubmitButton();
            }
        }

        private void quantity_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(quantity.Text))
            {
                quantity.BorderBrush = Brushes.Red;
                submitButton.IsEnabled = false;
            }
            else
            {
                quantity.BorderBrush = Brush;
                CheckIfCanEnableSubmitButton();
            }
        }

        private void onHours_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(onHours.Text))
            {
                onHours.BorderBrush = Brushes.Red;
                submitButton.IsEnabled = false;
            }
            else
            {
                onHours.BorderBrush = Brush;
                CheckIfCanEnableSubmitButton();
            }
        }
        private void endDate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (endDate.SelectedDate < DateTime.Now || endDate.SelectedDate == null)
            {
                endDate.BorderBrush = Brushes.Red;
                endDate.ToolTip = "You must ender valid date!";
                endDatePreventErrorTextBlock.Visibility = Visibility.Visible;
                submitButton.IsEnabled = false;
            }
            else
            {
                endDate.BorderBrush = Brushes.Gray;
                endDate.ToolTip = "This field is required!";
                endDatePreventErrorTextBlock.Visibility = Visibility.Hidden;
                CheckIfCanEnableSubmitButton();
            }
        }
        public void CheckIfCanEnableSubmitButton()
        {
            if (string.IsNullOrEmpty(medComboBox.Text) || endDate.SelectedDate < DateTime.Now || endDate.SelectedDate==null || timesPerDay.BorderBrush == Brushes.Red || quantity.BorderBrush == Brushes.Red || onHours.BorderBrush == Brushes.Red)
            {
                submitButton.IsEnabled = false;
            }
            else
            {
                submitButton.IsEnabled = true;
            }
        }
    }
}
