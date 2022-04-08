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
            acc.IsEnabled =(bool)enableRadioButton.IsChecked; 

            app.AccountController.Update(acc);

            CollectionViewSource.GetDefaultView(dataGridAccounts.ItemsSource).Refresh();
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            /*switch (typeComboBox.SelectedIndex)
            {
                case 0:

            }*/
        }

        
    }
}
