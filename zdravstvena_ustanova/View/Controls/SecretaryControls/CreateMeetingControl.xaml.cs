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
using zdravstvena_ustanova.View.Pages.SecretaryPages;

namespace zdravstvena_ustanova.View.Controls.SecretaryControls
{
    /// <summary>
    /// Interaction logic for CreateMeetingControl.xaml
    /// </summary>
    public partial class CreateMeetingControl : UserControl
    {
        private Meeting meeting;
        public ObservableCollection<Account> BusyParticipants { get; set; }

        private HomePagePatients _homePagePatients;
        public CreateMeetingControl(Meeting meeting, IEnumerable<Account> busyParticipants, HomePagePatients hpp)
        {
            InitializeComponent();
            DataContext = this;
            this.meeting = meeting;
            BusyParticipants = new ObservableCollection<Account>(busyParticipants);
            participantsList.ItemsSource = busyParticipants;
            _homePagePatients = hpp;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null; 
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            foreach(Account a in BusyParticipants)
            {
                if (meeting.Participants.Contains(a))
                    meeting.Participants.Remove(a);
            }
            Meeting m = app.MeetingController.Create(meeting);
            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
            _homePagePatients.SecretaryFrame.Content = new SchedulePage(_homePagePatients);
        }
    }
}
