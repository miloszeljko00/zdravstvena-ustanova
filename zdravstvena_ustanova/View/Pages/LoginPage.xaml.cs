using zdravstvena_ustanova.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using zdravstvena_ustanova.View.Pages.ManagerPages;
using zdravstvena_ustanova.View.Pages.SecretaryPages;
using zdravstvena_ustanova.View.Windows.DoctorWindows;
using zdravstvena_ustanova.View.Windows.DoctorWindows.View;

namespace zdravstvena_ustanova.View.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public MainWindow Mw { get; set; }
        public LoginPage(MainWindow mw)
        {
            InitializeComponent();
            Mw = mw;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Password;
            //username = "velja";
            //password = "velja";

            //username = "upravnik";
            //password = "upravnik";

            if (username == null || password == null) return;

            var app = Application.Current as App;
            var user = app.AccountController.Login(username, password);

            if (user == null) return;

            app.LoggedInUser = user;

            if (app.LoggedInUser is Manager) NavigationService.Navigate(new ManagerMainPage(this));
            if (app.LoggedInUser is Secretary) 
            {
                HomePagePatients hpp = new HomePagePatients(Mw);
                Mw.Height = 768;
                Mw.Width = 1024;
                Mw.Main.Content = hpp;
               

            }  
            if (app.LoggedInUser is Doctor)
            {
                var doctorLoginWindow = new LoginView();
                Mw.Close();
                doctorLoginWindow.Show();
            }
            if (app.LoggedInUser is Patient) 
            {
                List<AntiTrollMechanism> atms = new List<AntiTrollMechanism>(app.AntiTrollMechanismController.GetAll());
                foreach (AntiTrollMechanism atm in atms)
                {
                    if (app.LoggedInUser.Id == atm.Patient.Id && atm.NumberOfDates == 5)
                    {
                        if ((atm.DatesOfCanceledAppointments[4] - atm.DatesOfCanceledAppointments[0]).TotalDays <= 30)
                        {
                            MessageBox.Show("Onemogucen vam je pristup ovoj aplikaciji usled potencijalne zloupotrebe!", "Greska", MessageBoxButton.OK);
                            return;
                        }
                    }

                }
                    var pmw = new PatientMainWindow();
                    Mw.Close();
                    pmw.Show();
            }

        }

        private void goToRegistration(object sender, RoutedEventArgs e)
        {
            Registration r = new Registration();
            r.ShowDialog();
        }
    }
}
