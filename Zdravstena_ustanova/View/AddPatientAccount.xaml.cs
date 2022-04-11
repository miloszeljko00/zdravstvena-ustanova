using System;
using System.Collections.Generic;
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
using Model;
using Model.Enums;

namespace Zdravstena_ustanova.View
{
    /// <summary>
    /// Interaction logic for AddPatientAccount.xaml
    /// </summary>
    public partial class AddPatientAccount : Window
    {
        public AddPatientAccount()
        {
            InitializeComponent();
            var app = Application.Current as App;
            bloodTypeComboBox.ItemsSource = Enum.GetValues(typeof(BloodType)).Cast<BloodType>();
            emplTypeComboBox.ItemsSource = Enum.GetValues(typeof(EmploymentStatus)).Cast<EmploymentStatus>();
        }

        private void Button_Click_Add_Account(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            string surname = surnameTextBox.Text;
            double id = Convert.ToDouble(idTextBox.Text);
            string phone = phoneTextBox.Text;
            string email = emailTextBox.Text;
            DateTime date1 = (DateTime)date.SelectedDate;
            string street = streetTextBox.Text;
            string num = numberTextBox.Text;
            string city = cityTextBox.Text;
            string country = countryTextBox.Text;
            int insurance = int.Parse(insuranceTextBox.Text);
            BloodType bloodType = (BloodType)bloodTypeComboBox.SelectedIndex;
            EmploymentStatus employment = (EmploymentStatus)emplTypeComboBox.SelectedIndex;
            Address address = new Address(street, num, city, country);
            Patient patient = new Patient(name, surname, phone, email, date1, address, -1, insurance, bloodType, employment);
            var app = Application.Current as App;
            patient = app.PatientController.Create(patient);

            app.Patient = patient;
            this.Close();
        }

        
    }
}
