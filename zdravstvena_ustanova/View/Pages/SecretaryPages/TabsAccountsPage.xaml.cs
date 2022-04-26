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
using zdravstvena_ustanova.View.Controls.SecretaryControls;

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


        private void AddNewPatientMouseDown(object sender, MouseEventArgs e)
        {
            
            _homePagePatients.SecretaryFrame.Content = new AddPatientAccountPage(_homePagePatients);
        }

        private void AddStaffMouseDown(object sender, MouseEventArgs e)
        {
            _homePagePatients.SecretaryFrame.Content = new AddStaffAccountPage(_homePagePatients);
        }


        private void Edit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var editIcon = (Image)e.OriginalSource;
            var dataContext = editIcon.DataContext;
            Account account = dataContext as Account;
            if (account != null)
            {
                _homePagePatients.SecretaryFrame.Content = new EditPatientAccountPage(account, _homePagePatients);
            }

        }

        private void Record_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var recordIcon = (Image)e.OriginalSource;
            var dataContext = recordIcon.DataContext;
            Account account = dataContext as Account;
            if (account != null)
            {
                _homePagePatients.SecretaryFrame.Content = new SecretaryHealthRecordPage((Patient)account.Person);
            }
        }

        private void DeletePatient_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (dataGridAccountsPatients.SelectedItem == null)
                return;
            MainWindow.Modal.Content = new DeleteAccount(Accounts, dataGridAccountsPatients);
            MainWindow.Modal.IsOpen = true;
        }

        private void DeleteStaffImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (dataGridAccountsPatients.SelectedItem == null)
                return;
            MainWindow.Modal.Content = new DeleteAccount(Accounts, dataGridAccountsStaff);
            MainWindow.Modal.IsOpen = true;
        }
    }
}
