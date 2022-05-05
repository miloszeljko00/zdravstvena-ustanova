using zdravstvena_ustanova.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using zdravstvena_ustanova.View.Controls.SecretaryControls;

namespace zdravstvena_ustanova.View.Pages.SecretaryPages
{
    /// <summary>
    /// Interaction logic for UnscheduledAppointmentsPage.xaml
    /// </summary>
    public partial class UnscheduledAppointmentsPage : Page
    {
        public ObservableCollection<ScheduledAppointment> ScheduledAppointments { get; set; }

        private HomePagePatients _homePagePatients;

        public UnscheduledAppointmentsPage(HomePagePatients hpp)
        {
            InitializeComponent();
            this.DataContext = this;
            var app = Application.Current as App;
            _homePagePatients = hpp;
            ScheduledAppointments = new ObservableCollection<ScheduledAppointment>(app.UnScheduledAppointmentController.GetAll());

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var editIcon = (Image)e.OriginalSource;
            var dataContext = editIcon.DataContext;
            ScheduledAppointment sa = dataContext as ScheduledAppointment;
            if (sa.Start < DateTime.Today)
                return;

            if (sa != null)
            {
                _homePagePatients.SecretaryFrame.Content = new EditAppointmentPage(sa, _homePagePatients, true);
            }
        }

        private void Delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (dataGridScheduledAppointments.SelectedItem == null)
                return;
            MainWindow.Modal.Content = new CancelAppointmentControl(ScheduledAppointments, dataGridScheduledAppointments, true);
            MainWindow.Modal.IsOpen = true;
        }

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _homePagePatients.SecretaryFrame.Content = new SecretaryAppointmentPage(_homePagePatients);
        }
    }
}
