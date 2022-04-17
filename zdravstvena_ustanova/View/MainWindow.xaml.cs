using System.Collections.Generic;
using System.Windows;
using Model;

namespace zdravstvena_ustanova.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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

        private void Button_Click_Prostorije(object sender, RoutedEventArgs e)
        {
            RoomsTestPage rtp = new RoomsTestPage();
            rtp.Show();
        }

        private void Button_Click_Doktor(object sender, RoutedEventArgs e)
        {
            ScheduledAppointmentDoctorTest sadt = new ScheduledAppointmentDoctorTest();
            sadt.Show();
        }

        private void Button_Click_Pacijent(object sender, RoutedEventArgs e)
        {
            ScheduledAppointmentPatient sap = new ScheduledAppointmentPatient();
            sap.Show();
        }
    }
}
