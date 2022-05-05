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

namespace zdravstvena_ustanova.View.Pages.ManagerPages
{
    public partial class RenovationReportsPage : Page
    {
        public ObservableCollection<RenovationAppointment> RenovationAppointments { get; set; }

        public RenovationReportsPage()
        {
            InitializeComponent();
            DataContext = this;

            var app = Application.Current as App;

            RenovationAppointments = new ObservableCollection<RenovationAppointment>();
            var renovationAppointments = app.RenovationAppointmentController.GetAll();

            foreach(var renovationAppointment in renovationAppointments)
            {
                RenovationAppointments.Add(renovationAppointment);
            }
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTextBox.Text == "")
            {
                // Create an ImageBrush.
                ImageBrush textImageBrush = new();
                textImageBrush.ImageSource =
                    new BitmapImage(
                        new Uri(App.ProjectPath + "/Resources/img/search-name.png")
                    );
                textImageBrush.AlignmentX = AlignmentX.Left;
                textImageBrush.Stretch = Stretch.None;

                // Use the brush to paint the button's background.
                SearchTextBox.Background = textImageBrush;
            }
            else
            {

                SearchTextBox.Background = null;
            }
        }

        private void AddIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void EditIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void DeleteIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
