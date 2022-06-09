using System.Collections.Generic;
using System.Windows;
using ModalControl;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.View.Pages;
using zdravstvena_ustanova.View.Pages.ManagerPages;

namespace zdravstvena_ustanova.View
{
    public partial class MainWindow : Window
    {
        public static Modal Modal { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            CenterWindowOnScreen();
            Modal = modal;
            Main.Content = new LoginPage(this);
        }
        private void CenterWindowOnScreen()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void Button_Click_Nalozi(object sender, RoutedEventArgs e)
        {
            /*Repository.AccountsRepository accountsRepository = new Repository.AccountsRepository();
            Doctor doctor = new Doctor("Pera", "Peric", 2, "12345", "heloo@gmail.com", System.DateTime.Now, new Model.Address("ulica", "broj", "grad", "drzava"), null, System.DateTime.Now, 40, 10, "123245");
            Account account = new Account("peraperic", "peroni", true, doctor);
            List<Account> accounts = new List<Account>();
            accounts.Add(account);
            accountsRepository.Save(accounts);

            List<Model.Account> nalozi =  accountsRepository.Read();

            foreach (Model.Account nalog in nalozi)
            {
                list.Items.Add(nalog.Username);
            }*/
            //AccountTest accountTest = new AccountTest();
            //accountTest.Show();
            SecretaryAppointments sa = new SecretaryAppointments();
            sa.Show();
           //MainWindowAccount mwa = new MainWindowAccount();
           // mwa.Show();
        }
    }
}
