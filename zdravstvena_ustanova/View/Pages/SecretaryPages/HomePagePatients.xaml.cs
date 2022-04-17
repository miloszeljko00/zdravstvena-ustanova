using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Model.Enums;

namespace zdravstvena_ustanova.View.Pages.SecretaryPages
{
    /// <summary>
    /// Interaction logic for HomePagePatients.xaml
    /// </summary>
    
    public partial class HomePagePatients : Page
    {
        public ObservableCollection<Account> Accounts { get; set; }
        public ICollectionView PatientsAccounts { get; set; }
        public ICollectionView StaffAccounts { get; set; }
        public HomePagePatients()
        {
            InitializeComponent();
            this.DataContext = this;
            var app = Application.Current as App;
            Accounts = new ObservableCollection<Account>(app.AccountController.GetAll());
            PatientsAccounts = new CollectionViewSource { Source = Accounts }.View;
            PatientsAccounts.Filter = acc => ((Account)acc).AccountType == AccountType.PATIENT || ((Account)acc).AccountType == AccountType.GUEST;
            StaffAccounts = new CollectionViewSource { Source = Accounts }.View;
            StaffAccounts.Filter = acc => ((Account)acc).AccountType != AccountType.PATIENT && ((Account)acc).AccountType != AccountType.GUEST;

        }

        public void DeleteMouseDown(object sender, MouseEventArgs e)
        {
            var acc = (Account)dataGridAccountsPatients.SelectedItem;
            if (acc == null)
            {
                MessageBox.Show("Odaberi nalog!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var app = Application.Current as App;

            app.AccountController.Delete(acc.Id);
            Accounts.Remove(acc);

            CollectionViewSource.GetDefaultView(dataGridAccountsPatients.ItemsSource).Refresh();
        }

        public void AddMouseDown(object sender, MouseEventArgs e)
        {
            NavigationService.Navigate(new AddPatientAccountPage());
        }
    }

}
