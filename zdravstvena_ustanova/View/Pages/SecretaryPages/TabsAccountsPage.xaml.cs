using Model;
using Model.Enums;
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

namespace zdravstvena_ustanova.View.Pages.SecretaryPages
{
    /// <summary>
    /// Interaction logic for TabsAccountsPage.xaml
    /// </summary>
    public partial class TabsAccountsPage : Page
    {
        public ObservableCollection<Account> Accounts { get; set; }
        public ICollectionView PatientsAccounts { get; set; }
        public ICollectionView StaffAccounts { get; set; }

        private HomePagePatients _homePagePatients;

        private bool da;
        public TabsAccountsPage(HomePagePatients hpp)
        {
            InitializeComponent();
            this.DataContext = this;
            _homePagePatients = hpp;
            var app = Application.Current as App;
            Accounts = new ObservableCollection<Account>(app.AccountController.GetAll());
            PatientsAccounts = new CollectionViewSource { Source = Accounts }.View;
            PatientsAccounts.Filter = acc => ((Account)acc).AccountType == AccountType.PATIENT || ((Account)acc).AccountType == AccountType.GUEST;
            StaffAccounts = new CollectionViewSource { Source = Accounts }.View;
            StaffAccounts.Filter = acc => ((Account)acc).AccountType != AccountType.PATIENT && ((Account)acc).AccountType != AccountType.GUEST;
            da = true;
        }

        private void dataGridAccountsPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (da)
            {
                dataGridAccountsPatients.SelectedIndex = -1;
                da = false;
            }
            if(dataGridAccountsPatients.SelectedIndex == -1)
            {
                return;
            }
            Account account = dataGridAccountsPatients.SelectedValue as Account;
            if (account != null)
            {
                _homePagePatients.SecretaryFrame.Content = new EditPatientAccountPage(account, _homePagePatients);
            }
        }

        private void AddNewPatientMouseDown(object sender, MouseEventArgs e)
        {
            
            _homePagePatients.SecretaryFrame.Content = new AddPatientAccountPage(_homePagePatients);
        }

        private void AddStaffMouseDown(object sender, MouseEventArgs e)
        {
            _homePagePatients.SecretaryFrame.Content = new AddStaffAccountPage(_homePagePatients);
        }
    }
}
