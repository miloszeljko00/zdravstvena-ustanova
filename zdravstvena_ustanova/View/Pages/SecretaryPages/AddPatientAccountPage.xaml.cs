using Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace zdravstvena_ustanova.View.Pages.SecretaryPages
{
    /// <summary>
    /// Interaction logic for AddPatientAccountPage.xaml
    /// </summary>
    public partial class AddPatientAccountPage : Page
    {
        public AddPatientAccountPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTB.Text;
            string password = passwordTB.Text;
            string name = NameTB.Text;
            string surname = SurnameTB.Text;
            double id = Convert.ToDouble(jmbgTB.Text);
            string phone = phoneTB.Text;
            string email = emailTB.Text;
            DateTime date1 = (DateTime)dateDP.SelectedDate;
            string street = streetTB.Text;
            string num = numberTB.Text;
            string city = cityTB.Text;
            string country = countryTB.Text;
            Address address = new Address(street, num, city, country);
            //Patient patient = new Patient(name, surname, phone, email, date1, address, -1);
           // var app = Application.Current as App;
            //patient = app.PatientController.Create(patient);
        }
    }
}
