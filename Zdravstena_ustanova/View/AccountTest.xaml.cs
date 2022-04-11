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
using Model;
using Model.Enums;


namespace Zdravstena_ustanova.View
{
    /// <summary>
    /// Interaction logic for AccountTest.xaml
    /// </summary>
    public partial class AccountTest : Window
    {
        public ObservableCollection<Account> Accounts { get; set; }
        public AccountTest()
        {
            InitializeComponent();
            this.DataContext = this;
            var app = Application.Current as App;

            typeComboBox.ItemsSource = Enum.GetValues(typeof(AccountType)).Cast<AccountType>();
            try
            { 
                Accounts = new ObservableCollection<Account>(app.AccountController.GetAll());
            }catch(System.Exception ex)
            {
                Accounts = new ObservableCollection<Account>();
            }
            
        }

        private void dataGridAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var acc = (Account)dataGridAccounts.SelectedItem;

            if (acc == null)
            {
                return;
            }

            typeComboBox.SelectedValue = acc.AccountType;
            usernameTextBox.Text = acc.Username;
            passwordTextBox.Text = acc.Password;
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            var acc = (Account)dataGridAccounts.SelectedItem;
            if (acc == null)
            {
                MessageBox.Show("Odaberi nalog!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var app = Application.Current as App;

            app.AccountController.Delete(acc.Id);
            Accounts.Remove(acc);

            CollectionViewSource.GetDefaultView(dataGridAccounts.ItemsSource).Refresh();
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            var acc = (Account)dataGridAccounts.SelectedItem;
            if (acc == null)
            {
                MessageBox.Show("Odaberi nalog!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (usernameTextBox.Text == "" || passwordTextBox.Text == "" || typeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Popuni sva polja prvo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var app = Application.Current as App;

            acc.Username = usernameTextBox.Text;
            acc.Password = passwordTextBox.Text;
            acc.AccountType = (AccountType)typeComboBox.SelectedIndex;
            acc.IsEnabled = !(bool)isDisabledCheckBox.IsChecked; 

            app.AccountController.Update(acc);

            CollectionViewSource.GetDefaultView(dataGridAccounts.ItemsSource).Refresh();
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;
            bool isEnabled = !(bool)isDisabledCheckBox.IsChecked;

            
            switch (typeComboBox.SelectedIndex)
            {
                case 0:
                    Account account1 = new Account(username, password, isEnabled, (AccountType)typeComboBox.SelectedIndex);
                    app.AccountController.Create(account1);
                    Accounts.Add(account1);
                    CollectionViewSource.GetDefaultView(dataGridAccounts.ItemsSource).Refresh();
                    break;
                case 1:
                    AddPatientAccount apa = new AddPatientAccount();
                    apa.ShowDialog();
                    Patient patient = app.Patient;
                    Account account2 = new Account(username, password, isEnabled, patient, (AccountType)typeComboBox.SelectedIndex);
                    account2 = app.AccountController.Create(account2);
                    patient.Account = account2;
                    app.PatientController.Update(patient);
                    Accounts.Add(account2);
                    CollectionViewSource.GetDefaultView(dataGridAccounts.ItemsSource).Refresh();
                    break;
                case 2:
                    AddDoctorAccount ada = new AddDoctorAccount(0);
                    ada.ShowDialog();
                    Doctor doctor = app.Doctor;
                    Account account3 = new Account(username, password, isEnabled, doctor, (AccountType)typeComboBox.SelectedIndex);
                    account3 = app.AccountController.Create(account3);
                    doctor.Account = account3;
                    app.DoctorController.Update(doctor);
                    Accounts.Add(account3);
                    CollectionViewSource.GetDefaultView(dataGridAccounts.ItemsSource).Refresh();
                    break;
                case 3:
                    AddStaffAccount asa = new AddStaffAccount(0);
                    asa.ShowDialog();
                    Manager manager = app.Manager;
                    Account account4 = new Account(username, password, isEnabled, manager, (AccountType)typeComboBox.SelectedIndex);
                    account4 = app.AccountController.Create(account4);
                    manager.Account = account4;
                    app.ManagerController.Update(manager);
                    Accounts.Add(account4);
                    CollectionViewSource.GetDefaultView(dataGridAccounts.ItemsSource).Refresh();
                    break;
                case 4:
                    AddStaffAccount asa1 = new AddStaffAccount(1);
                    asa1.ShowDialog();
                    Secretary secretary = app.Secretary;
                    Account account5 = new Account(username, password, isEnabled, secretary, (AccountType)typeComboBox.SelectedIndex);
                    account5 = app.AccountController.Create(account5);
                    secretary.Account = account5;
                    app.SecretaryController.Update(secretary);
                    Accounts.Add(account5);
                    CollectionViewSource.GetDefaultView(dataGridAccounts.ItemsSource).Refresh();
                    break;
            }
            
        }

        
    }
}
