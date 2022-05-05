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
using System.Windows.Shapes;

namespace zdravstvena_ustanova.View
{
    /// <summary>
    /// Interaction logic for DeleteAppointment.xaml
    /// </summary>
    public partial class DeleteAppointment : Window
    {
        private Window parent;
        public ScheduledAppointment ScheduledAppointment { get; set; }
        public DeleteAppointment(Window parent, ScheduledAppointment sa)
        {
            InitializeComponent();
            this.parent = parent;
            ScheduledAppointment = sa;
        }

        private void goToAppointments(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;

            app.ScheduledAppointmentController.Delete(ScheduledAppointment.Id);
            this.Close();
            this.parent.Close();
        }

        private void goToManageAppointment(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
