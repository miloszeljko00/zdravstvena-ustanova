using zdravstvena_ustanova.Model;
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
using System.Windows.Shapes;

namespace zdravstvena_ustanova.View.Windows.DoctorWindows
{
    public partial class UpdateMedicineInTherapy : Window
    {
        public ObservableCollection<Medication> Medications { get; set; }
        public PrescribedMedicine PrescribedMedicineSelected { get; set; }
        public ObservableCollection<PrescribedMedicine> PrescribedMedicine { get; set; }
        public SolidColorBrush Brush { get; set; }
        public UpdateMedicineInTherapy(ObservableCollection<PrescribedMedicine> pms, PrescribedMedicine pm)
        {
            InitializeComponent();
            DataContext = this;
            var app = Application.Current as App;
            Medications = new ObservableCollection<Medication>(app.MedicationController.GetAll());
            PrescribedMedicine = pms;
            PrescribedMedicineSelected = pm;
            medComboBox.ItemsSource = Medications;
            medComboBox.Text = pm.Medication.Name;
            medIdTextBox.Text = pm.Medication.Id.ToString();
            timesPerDay.Text = pm.TimesPerDay.ToString();
            quantity.Text = timesPerDay.Text;
            onHours.Text = pm.OnHours.ToString();
            endDate.Text = pm.EndDate.Date.ToString();
            description.Text = pm.Description;
            
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Medication med = (Medication)medComboBox.SelectedItem;
            medIdTextBox.Text = med.Id.ToString();
        }
        private void updatePrescribedMedicine(object sender, RoutedEventArgs e)
        {
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
                endDate.ToolTip = "You must select valid date!";
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
            string desc = description.Text;
            //if(description.Text==null)
            //{
            //    desc = "";
            //} else
            //{
            //    desc = description.Text;
            //}
            var app = Application.Current as App;
            if (app.PrescribedMedicineController.ValidateParametersFromForm(med, tpd, oh, ed, desc))
            {
                foreach (PrescribedMedicine pmPomocni in PrescribedMedicine)
                {
                    if (pmPomocni.Id == PrescribedMedicineSelected.Id)
                    {
                        PrescribedMedicine.Remove(pmPomocni);
                        break;
                    }
                }
                PrescribedMedicine.Add(new PrescribedMedicine(PrescribedMedicineSelected.Id, int.Parse(tpd), int.Parse(oh), (DateTime)ed, desc, med));
                this.Close();
            }
        }
        private void Button_Click_Cancel_Updating_Medicine_In_Therapy(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Are you sure you wanna udno changes?", "Changing terapy", MessageBoxButton.YesNo, MessageBoxImage.Question);
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
                endDate.ToolTip = "You must select valid date!";
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
            if (string.IsNullOrEmpty(medComboBox.Text) || endDate.SelectedDate < DateTime.Now || endDate.SelectedDate == null || timesPerDay.BorderBrush == Brushes.Red || quantity.BorderBrush == Brushes.Red || onHours.BorderBrush == Brushes.Red)
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
