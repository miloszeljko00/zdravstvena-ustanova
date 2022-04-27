using Model;
using Model.Enums;
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

namespace zdravstvena_ustanova.View.Pages.SecretaryPages
{
    /// <summary>
    /// Interaction logic for AddStaffAccountPage.xaml
    /// </summary>
    public partial class AddStaffAccountPage : Page
    {
        private HomePagePatients _homePagePatients;

        public ObservableCollection<Specialty> Specs { get; set; }
        public AddStaffAccountPage(HomePagePatients hpp)
        {
            InitializeComponent();
            String[] types = { "Doctor", "Manager", "Secretary" };
            //typeComboBox.ItemsSource = Enum.GetValues(typeof(AccountType)).Cast<AccountType>();
            typeComboBox.ItemsSource = types;
            _homePagePatients = hpp;
            var app = Application.Current as App;
            Specs = new ObservableCollection<Specialty>(app.SpecialtyController.GetAll());
            specCB.DataContext = Specs;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(typeComboBox.SelectedIndex == -1)
            {
                //TODO neka prikladna poruka
                _homePagePatients.SecretaryFrame.Content = new TabsAccountsPage(_homePagePatients);
                return;
            }
            string username = usernameTB.Text;
            string password = passwordTB.Text;
            string name = nameTB.Text;
            string surname = surnameTB.Text;
            long id = int.Parse(jmbgTB.Text);
            string phone = phoneTB.Text;
            string email = emailTB.Text;
            string street = streetTB.Text;
            string num = numTB.Text;
            string city = cityTB.Text;
            string country = countryTB.Text;
            var date1 = (DateTime)dateDP.SelectedDate;
            Address address = new Address(street, num, city, country);
            string licenceNumber = "123";       //TODO NAPRAVITI UNOS SA XAML FORME!!!!
            int experience = 5;                 //TODO NAPRAVITI UNOS SA XAML FORME!!!!
            DateTime emplDate = date1;          //TODO NAPRAVITI UNOS SA XAML FORME!!!!

            long roomId = 1;                   //TODO NAPRAVITI UNOS SA XAML FORME!!!!
            // //TODO NAPRAVITI UNOS SA XAML FORME!!!!
            //long specialityId = 1;
            if(typeComboBox.SelectedIndex == 0)
            {
                long specialityId = (long)specCB.SelectedValue;
                Doctor doctor = new Doctor(licenceNumber, roomId, specialityId, emplDate, experience, name, surname, id, phone, email, date1, address, -1, Shift.FIRST);
                var app1 = Application.Current as App;
                doctor = app1.DoctorController.Create(doctor);
                Account account = new Account(username, password, true, doctor, AccountType.DOCTOR);    
                account = app1.AccountController.Create(account);
                doctor.Account = account;
                app1.DoctorController.Update(doctor);
                _homePagePatients.SecretaryFrame.Content = new TabsAccountsPage(_homePagePatients);
                return;
            }

          if(typeComboBox.SelectedIndex == 1)
            {
                Manager manager = new Manager(name, surname, id, phone, email, date1, address, -1, emplDate, experience, Shift.FIRST);
                var app1 = Application.Current as App;
                manager = app1.ManagerController.Create(manager);
                Account account = new Account(username, password, true, manager, AccountType.MANAGER);    
                account = app1.AccountController.Create(account);
                manager.Account = account;
                app1.ManagerController.Update(manager);
                _homePagePatients.SecretaryFrame.Content = new TabsAccountsPage(_homePagePatients);
                return;
            }

            if(typeComboBox.SelectedIndex == 2)
            {
                Secretary secretary = new Secretary(name, surname, id, phone, email, date1, address, -1, emplDate, experience, Shift.FIRST);
                var app1 = Application.Current as App;
                secretary = app1.SecretaryController.Create(secretary);
                Account account = new Account(username, password, true, secretary, AccountType.SECRETARY);    
                account = app1.AccountController.Create(account);
                secretary.Account = account;
                app1.SecretaryController.Update(secretary);
                _homePagePatients.SecretaryFrame.Content = new TabsAccountsPage(_homePagePatients);
            }
        }
    }
}
