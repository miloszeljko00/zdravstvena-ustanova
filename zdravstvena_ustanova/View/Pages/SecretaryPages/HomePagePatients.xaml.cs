using zdravstvena_ustanova.Model;
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
using zdravstvena_ustanova.Model.Enums;

namespace zdravstvena_ustanova.View.Pages.SecretaryPages
{
    /// <summary>
    /// Interaction logic for HomePagePatients.xaml
    /// </summary>
    
    public partial class HomePagePatients : Page
    {
        private MainWindow mainWindow;
        public HomePagePatients(MainWindow mainWindow)
        {
            InitializeComponent();
            //this.DataContext = this;
            SecretaryFrame.Content = new TabsAccountsPage(this);
            this.mainWindow = mainWindow;
        }

       /* public void DeleteMouseDown(object sender, MouseEventArgs e)
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
*/
        public void AccountsMouseDown(object sender, MouseEventArgs e)
        {
            //NavigationService.Navigate(new AddPatientAccountPage());
            SecretaryFrame.Content = new TabsAccountsPage(this);
        }
        public void AppointmentMouseDown(object sender, MouseEventArgs e)
        {
                SecretaryFrame.Content = new SecretaryAppointmentPage(this);
        }

        private void Profile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SecretaryFrame.Content = new ProfilePage(mainWindow);
        }
    }

}
