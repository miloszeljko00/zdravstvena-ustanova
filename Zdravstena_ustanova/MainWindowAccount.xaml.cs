using System;
using System.Collections.Generic;
using System.Windows;
using Model;
using Service;

namespace Zdravstena_ustanova
{
    public partial class MainWindowAccount : Window
    {
        AccountService accService;
        public MainWindowAccount()
        {
            InitializeComponent();
            accService = new AccountService();
        }

        private void Button_Click_Add_Account(object sender, RoutedEventArgs e)
        {
            string name1 = name.Text;
            string surname1 = surname.Text;
            double id1 = Convert.ToDouble(id.Text);
            string phone1 = phone.Text;
            string mail1 = mail.Text;
            DateTime date1 = (DateTime)date.SelectedDate;
            DateTime date2 = (DateTime)employmentDate.SelectedDate;
            int hours = Convert.ToInt32(hour.Text);
            int exp = Convert.ToInt32(experience.Text);
            string street1 = street.Text;
            string num1 = num.Text;
            string city1 = city.Text;
            string country1 = country.Text;
            string password1 = password.Text;
            string username1 = username.Text;
            Address address = new Address(street1, num1, city1, country1);
            Secretary s = new Secretary(name1, surname1, id1, phone1, mail1, date1, address, null, date2, hours, exp);
            accService.CreateAccount(new Account(username1, password1, true, s));
            List<Account> list1 = accService.Accounts;
            list.Items.Clear();
            foreach (Account acc in list1) 
            {
                list.Items.Add(acc.Username);
            }


        }

        private void Button_Click_Delete_Account(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Update_Account(object sender, RoutedEventArgs e)
        {

        }

        private void spec_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
