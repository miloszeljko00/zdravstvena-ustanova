using System.Windows;
using System.Windows.Navigation;
using zdravstvena_ustanova.View.Pages;

namespace zdravstvena_ustanova.View
{
    public partial class PatientMainWindow : Window
    {
        public PatientMainWindow()
        {
            InitializeComponent();
        }

        private void goToAppointments(object sender, RoutedEventArgs e)
        {
            this.content.Content = new Appointments();
        }

        private void goToLogin(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void goToHelp(object sender, RoutedEventArgs e)
        {
            this.content.Content=new Help();
        }

        private void goToMedicalReports(object sender, RoutedEventArgs e)
        {

        }
    }
}
