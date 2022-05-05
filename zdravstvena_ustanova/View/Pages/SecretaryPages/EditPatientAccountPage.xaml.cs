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
    /// Interaction logic for EditPatientAccountPage.xaml
    /// </summary>
    public partial class EditPatientAccountPage : Page
    {
        private Account account;
        private HomePagePatients _homePagePatients;
        public EditPatientAccountPage(Account account, HomePagePatients hpp)
        {
            InitializeComponent();
            this.account = account;
            _homePagePatients = hpp;
            usernameTB.Text = account.Username;
            passwordTB.Text = account.Password;
            nameTB.Text = account.Person.Name;
            surnameTB.Text = account.Person.Surname;
            if (account.AccountType == AccountType.GUEST)
                dateDP.SelectedDate = null;
            else
                dateDP.SelectedDate = account.Person.DateOfBirth;
            emailTB.Text = account.Person.Email;
            phoneTB.Text = account.Person.PhoneNumber;
            jmbgTB.Text = Convert.ToString(account.Person.Id);
            streetTB.Text = account.Person.Address.Street;
            numTB.Text = account.Person.Address.Number;
            cityTB.Text = account.Person.Address.City;
            countryTB.Text = account.Person.Address.Country;
            isDisabledCheckBox.IsChecked = !account.IsEnabled;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            if(account.Username != usernameTB.Text || account.Password != passwordTB.Text || isDisabledCheckBox.IsChecked == account.IsEnabled)
            {
                account.Username = usernameTB.Text;
                account.Password = passwordTB.Text;
                account.IsEnabled = !(bool)isDisabledCheckBox.IsChecked; 
                app.AccountController.Update(account);
            }
            account.Person.Name = nameTB.Text;
            account.Person.Surname = surnameTB.Text;
            account.Person.DateOfBirth = (DateTime)dateDP.SelectedDate;
            account.Person.Email =emailTB.Text;
            account.Person.PhoneNumber = phoneTB.Text;
            //jmbgTB.Text = Convert.ToString(account.Person.Id);
            account.Person.Address.Street = streetTB.Text;
            account.Person.Address.Number = numTB.Text;
            account.Person.Address.City = cityTB.Text;
            account.Person.Address.Country = countryTB.Text;
            Patient patient = account.Person as Patient;
            app.PatientController.Update(patient);
            _homePagePatients.SecretaryFrame.Content = new TabsAccountsPage(_homePagePatients);
        }
    }
}
