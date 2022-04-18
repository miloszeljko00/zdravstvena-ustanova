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
using System.Windows.Shapes;
using Model;
using Model.Enums;

namespace zdravstvena_ustanova.View
{
    /// <summary>
    /// Interaction logic for AddDoctorAccount.xaml
    /// </summary>
    public partial class AddDoctorAccount : Window
    {
        private int type;
        public ObservableCollection<Specialty> Specs { get; set; }
        public AddDoctorAccount(int type)
        {
            InitializeComponent();
            this.type = type;
            var app = Application.Current as App;
            Specs = new ObservableCollection<Specialty>(app.SpecialtyController.GetAll());
            specCB.DataContext = Specs;
            
            
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
            int weeklyHours = int.Parse(weeklyHoursTextBox.Text);
            string licenceNumber = licenceTextBox.Text;
            int experience = int.Parse(experienceTextBox.Text);
            DateTime emplDate = (DateTime)dateOfEmployment.SelectedDate;

            long roomId = 1; //TODO NAPRAVITI UNOS SA XAML FORME!!!!
            long specialityId = 1; //TODO NAPRAVITI UNOS SA XAML FORME!!!!
            if(type == 0)
            {
                Doctor doctor = new Doctor(licenceNumber, roomId, specialityId, emplDate, experience, name, surname, id, phone, email, date1, address, -1, Shift.FIRST);
                var app = Application.Current as App;
                doctor = app.DoctorController.Create(doctor);
                app.Doctor = doctor;
                this.Close();
            }
            
        }
    }
}
