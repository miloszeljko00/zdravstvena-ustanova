using System.Collections.Generic;
using System.Windows;
using Model;

namespace Zdravstena_ustanova
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
            MainWindowAccount mwa = new MainWindowAccount();
            mwa.Show();
        }

        private void Button_Click_Prostorije(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Doktor(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Pacijent(object sender, RoutedEventArgs e)
        {

        }
    }
}
