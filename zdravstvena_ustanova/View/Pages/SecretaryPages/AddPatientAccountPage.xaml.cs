using zdravstvena_ustanova.Model;
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
using zdravstvena_ustanova.Model.Enums;

namespace zdravstvena_ustanova.View.Pages.SecretaryPages
{
    /// <summary>
    /// Interaction logic for AddPatientAccountPage.xaml
    /// </summary>
    public partial class AddPatientAccountPage : Page
    {

        private HomePagePatients _homePagePatients;
        public AddPatientAccountPage(HomePagePatients hpp)
        {
            InitializeComponent();
            _homePagePatients = hpp;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTB.Text;
            string password = passwordTB.Text;
            string name = nameTB.Text;
            string surname = surnameTB.Text;
            double id = Convert.ToDouble(jmbgTB.Text);
            string phone = phoneTB.Text;
            string email = emailTB.Text;
            string street = streetTB.Text;
            string num = numTB.Text;
            string city = cityTB.Text;
            string country = countryTB.Text;
            var date1 = (DateTime)dateDP.SelectedDate;
            Address address = new Address(street, num, city, country);
            Patient patient = new Patient(name, surname, phone, email, date1, address, -1);
            var app = Application.Current as App;
            patient = app.PatientController.Create(patient);
            Account account2 = new Account(username, password, true, patient, AccountType.PATIENT);
            account2 = app.AccountController.Create(account2);
            patient.Account = account2;
            app.PatientController.Update(patient);
            _homePagePatients.SecretaryFrame.Content = new TabsAccountsPage(_homePagePatients);


        }

    }
}
