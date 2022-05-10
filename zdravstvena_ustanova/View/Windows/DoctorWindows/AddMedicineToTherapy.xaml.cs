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
            medIdTextBox.Text = med.Id.ToString();
        }

        private void createPrescribedMedicine(object sender, RoutedEventArgs e)
        {
            if(medComboBox.SelectedItem == null)
            {
                if (timesPerDay.Text == null || timesPerDay.Text == "" || onHours.Text == null || onHours.Text == "")
                {
                    MessageBox.Show("Morate uneti sve obavezne podatke(medication,tbd,oh,endDate...)");
                    return;
                }
                MessageBox.Show("Morate odabrati lek!");
                return;
            }
            Medication med = (Medication)medComboBox.SelectedItem;
            if(timesPerDay.Text==null || timesPerDay.Text=="")
            {
                if (onHours.Text == null || onHours.Text == "" || medComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Morate uneti sve obavezne podatke(medication,tbd,oh,endDate...)");
                    return;
                } 
                MessageBox.Show("Morate uneti tpd podatak");
                return;
            }
            int tpd = int.Parse(timesPerDay.Text);
            if (onHours.Text == null || onHours.Text=="")
            {
                if (timesPerDay.Text == null || timesPerDay.Text == "" || medComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Morate uneti sve obavezne podatke(medication,tbd,oh,endDate...)");
                    return;
                }
                MessageBox.Show("Morate uneti oh podatak");
                return;
            }
            int oh = int.Parse(onHours.Text);
            if(((DateTime?)endDate.SelectedDate) == null)
            {
                MessageBox.Show("Morate izabrati end date!");
                return;
            }
            DateTime ed = (DateTime)endDate.SelectedDate;
            if(ed.Year<DateTime.Now.Year)
            {
                MessageBox.Show("Izabrali ste termin u proslosti!");
                return;
            } else if(ed.Year == DateTime.Now.Year && ed.Month<DateTime.Now.Month)
            {
                MessageBox.Show("Izabrali ste termin u proslosti!");
                return;
            } else if(ed.Year == DateTime.Now.Year && ed.Month == DateTime.Now.Month && ed.Day<DateTime.Now.Day)
            {
                MessageBox.Show("Izabrali ste termin u proslosti!");
                return;
            }
            string desc;
            if(description.Text==null)
            {
                desc = "";
            } else
            {
                desc = description.Text;
            }
            PrescribedMedicine.Add(new PrescribedMedicine(-1, tpd, oh, ed, desc, med));
            this.Close();


        }

        private void Button_Click_Cancel_Adding_Medicine_In_Therapy(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Da li ste sigurni da zelite da ponistite izmene?", "Izmena terapije", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
