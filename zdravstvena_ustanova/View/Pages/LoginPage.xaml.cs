using Model;
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
            //string username = usernameTextBox.Text;
            //string password = passwordTextBox.Text;
            string username = "velja";
            string password = "velja";


            if (username == null || password == null) return;

            var app = Application.Current as App;
            var user = app.AccountController.Login(username, password);

            if (user == null) return;

            app.LoggedInUser = user;

            if (app.LoggedInUser is Manager) NavigationService.Navigate(new ManagerMainPage());
            if (app.LoggedInUser is Secretary) 
            {
                HomePagePatients hpp = new HomePagePatients(Mw);
                Mw.Height = 768;
                Mw.Width = 1024;
                Mw.Main.Content = hpp;
               

            }  
            if (app.LoggedInUser is Doctor)
            {
                var doctorHomePage = new DoctorHomePageWindow();
                Mw.Close();
                doctorHomePage.Show();
            }
            if (app.LoggedInUser is Patient) 
            {
                var pmw = new PatientMainWindow();
                Mw.Close();
                pmw.Show();
            }

        }
    }
}
