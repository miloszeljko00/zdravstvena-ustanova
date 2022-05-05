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
using zdravstvena_ustanova.Model.Enums;

namespace zdravstvena_ustanova.View.Pages.SecretaryPages
{
    /// <summary>
    /// Interaction logic for SecretaryHealthRecordPage.xaml
    /// </summary>
    public partial class SecretaryHealthRecordPage : Page
    {
        private Patient patient;

        private HealthRecord healthRecord;

        public ObservableCollection<Allergens> patientAllergens { get; set; }

        public ObservableCollection<Allergens> allergens { get; set; }

        public HomePagePatients _homePagePatients;
        private bool newRecord;
        public SecretaryHealthRecordPage(Patient patient, HomePagePatients hpp)
        {
            InitializeComponent();
            DataContext = this;
            _homePagePatients = hpp;
            var app = Application.Current as App;
            this.patient = patient;
            newRecord = false;
            patientTB.Text = patient.Name + " " + patient.Surname;
            healthRecord = app.HealthRecordController.FindHealthRecordByPatient(patient.Id);
            
            allergens = new ObservableCollection<Allergens>(app.AllergensController.GetAll());
            emplStatusCB.ItemsSource = Enum.GetValues(typeof(EmploymentStatus)).Cast<EmploymentStatus>();
            bloodTypeCB.ItemsSource = Enum.GetValues(typeof(BloodType)).Cast<BloodType>();
            if(healthRecord == null)
            {
                patientAllergens = new ObservableCollection<Allergens>();
                healthRecord = new HealthRecord(-1, -1, 0, 0, patient.Id);
                newRecord = true;
                insuranceTB.Text = "";
            }
            else
            {
                 patientAllergens = new ObservableCollection<Allergens>(healthRecord.Allergens);
                 insuranceTB.Text = healthRecord.InsuranceNumber.ToString();
                 bloodTypeCB.SelectedValue = healthRecord.BloodType;
                 emplStatusCB.SelectedValue = healthRecord.EmploymentStatus;

            }
      
            allergensList.ItemsSource = patientAllergens;
            allergensCB.ItemsSource = allergens;
            
        }

        private void Add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(allergensCB.SelectedItem != null)
            {
                foreach(Allergens allergen in patientAllergens)
                {
                    if (allergen.Id == ((Allergens)allergensCB.SelectedItem).Id)
                        return;
                }
                patientAllergens.Add((Allergens)allergensCB.SelectedItem);
            }
        }
        private void Delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(allergensList.SelectedItem != null)
            {
                patientAllergens.Remove((Allergens)allergensList.SelectedItem);
            }
        }

        private void Save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var app = Application.Current as App;
            if(newRecord)
            {
                healthRecord.InsuranceNumber = Int32.Parse(insuranceTB.Text); //try parse
                healthRecord.BloodType = (BloodType)bloodTypeCB.SelectedItem;
                healthRecord.EmploymentStatus = (EmploymentStatus)emplStatusCB.SelectedItem;
                foreach(Allergens a in patientAllergens)
                {
                    healthRecord.Allergens.Add(a);
                }
                
                healthRecord = app.HealthRecordController.Create(healthRecord);
                _homePagePatients.SecretaryFrame.Content = new TabsAccountsPage(_homePagePatients);
                return;
            }
            healthRecord.InsuranceNumber = Int32.Parse(insuranceTB.Text); //try parse
            healthRecord.BloodType = (BloodType)bloodTypeCB.SelectedItem;
            healthRecord.EmploymentStatus = (EmploymentStatus)emplStatusCB.SelectedItem;

            healthRecord.Allergens = new List<Allergens>();
            foreach(Allergens a in patientAllergens)
            {
                healthRecord.Allergens.Add(a);
            }
            
            app.HealthRecordController.Update(healthRecord);
            _homePagePatients.SecretaryFrame.Content = new TabsAccountsPage(_homePagePatients);


        }

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _homePagePatients.SecretaryFrame.Content = new TabsAccountsPage(_homePagePatients);
        }
    }
}
