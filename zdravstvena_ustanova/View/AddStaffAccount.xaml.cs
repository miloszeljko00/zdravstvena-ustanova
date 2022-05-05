using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;

namespace zdravstvena_ustanova.View
{
    /// <summary>
    /// Interaction logic for AddStaffAccount.xaml
    /// </summary>
    public partial class AddStaffAccount : Window
    {
        private int type;
        public AddStaffAccount(int type)
        {
            InitializeComponent();
            this.type = type;
        }

        private void Button_Click_Add_Account(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            string surname = surnameTextBox.Text;
            long id = long.Parse(idTextBox.Text);
            string phone = phoneTextBox.Text;
            string email = emailTextBox.Text;
            DateTime date1 = (DateTime)date.SelectedDate;
            string street = streetTextBox.Text;
            string num = numberTextBox.Text;
            string city = cityTextBox.Text;
            string country = countryTextBox.Text;
            Address address = new Address(street, num, city, country);
            int experience = int.Parse(experienceTextBox.Text);
            DateTime emplDate = (DateTime)dateOfEmployment.SelectedDate;
            if(type == 0)
            {
                Manager manager = new Manager(name, surname, id, phone, email, date1, address, -1, emplDate, experience, Shift.FIRST);
                var app = Application.Current as App;
                manager = app.ManagerController.Create(manager);
                app.Manager = manager;
                this.Close();
            }
            else
            {
                Secretary secretary = new Secretary(name, surname, id, phone, email, date1, address, -1, emplDate, experience, Shift.FIRST);
                var app = Application.Current as App;
                secretary = app.SecretaryController.Create(secretary);
                app.Secretary = secretary;
                this.Close();
            }
        }
    }
}
