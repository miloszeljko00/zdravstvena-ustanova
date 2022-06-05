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
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.View.Pages.SecretaryPages
{
    /// <summary>
    /// Interaction logic for SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : Page
    {
        private HomePagePatients _homePagePatients;

        public ObservableCollection<Meeting> Meetings { get; set; }
        public SchedulePage(HomePagePatients hpp)
        {
            InitializeComponent();
            DataContext = this;
            _homePagePatients = hpp;
            var app = Application.Current as App;
            Meetings = new ObservableCollection<Meeting>(app.MeetingController.GetAll());
        }

        private void Holiday_Click(object sender, RoutedEventArgs e)
        {
            _homePagePatients.SecretaryFrame.Content = new HolidayRequestPage(_homePagePatients);
        }

        private void NewMeeting_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _homePagePatients.SecretaryFrame.Content = new NewMeetingPage(_homePagePatients);
        }
    }
}
