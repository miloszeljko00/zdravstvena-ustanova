using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using zdravstvena_ustanova.Model.Enums;
using zdravstvena_ustanova.View.Controls.SecretaryControls;

namespace zdravstvena_ustanova.View.Pages.SecretaryPages
{
    /// <summary>
    /// Interaction logic for NewMeetingPage.xaml
    /// </summary>
    public partial class NewMeetingPage : Page
    {
        private HomePagePatients _homePagePatients;

        public ObservableCollection<Room> Rooms { get; set; }

        public ObservableCollection<Account> Accounts { get; set; }

        public ObservableCollection<Account> Participants { get; set; }

        public ICollectionView AccountView { get; set; }

        public ICollectionView RoomView { get; set; }
        public NewMeetingPage(HomePagePatients hpp)
        {
            InitializeComponent();
            DataContext = this;
            _homePagePatients = hpp;
            var app = Application.Current as App;
            Rooms = new ObservableCollection<Room>(app.RoomController.GetAll());
            
            RoomView = new CollectionViewSource { Source = Rooms }.View;
            RoomView.Filter = r =>
            {
                Room room = r as Room;
                if (room.RoomType != RoomType.MEETING_ROOM)
                    return false;
                return true;
            };
            Accounts = new ObservableCollection<Account>(app.AccountController.GetAll());
            Participants = new ObservableCollection<Account>();
            AccountView = new CollectionViewSource { Source = Accounts }.View;
            AccountView.Filter = a =>
            {
                Account account = a as Account;
                if (account.AccountType == AccountType.GUEST || account.AccountType == AccountType.PATIENT)
                    return false;
                return true;
            };
            RoomCB.ItemsSource = RoomView;
            participantsCB.ItemsSource = AccountView;
            string[] times = {"08:00", "09:00", "10:00", "11:00", "12:00",
                                "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00" };
            TimeCB.ItemsSource = times;
            ParticipantsList.ItemsSource = Participants;

        }

        private void Add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(participantsCB.SelectedItem != null)
            {
                foreach(Account account in Participants)
                {
                    if (account.Id == ((Account)participantsCB.SelectedItem).Id)
                        return;
                }
                Participants.Add((Account)participantsCB.SelectedItem);
            }

        }

        private void Delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(ParticipantsList.SelectedItem != null)
            {
                Participants.Remove((Account)ParticipantsList.SelectedItem);
            }
        }

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _homePagePatients.SecretaryFrame.Content = new SchedulePage(_homePagePatients);
        }

        private void Save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var app = Application.Current as App;
            var r = RoomCB.SelectedItem as Room;
            if (r == null || TimeCB.SelectedItem == null)
            {
                return;
            }
            
            Meeting meeting = new Meeting(-1);
            DateTime date = (DateTime)DateDP.SelectedDate;
            string[] time = TimeCB.SelectedItem.ToString().Split(":");
            int hour = int.Parse(time[0]);
            int minute = int.Parse(time[1]);

            meeting.Time = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            meeting.Room = r;
            meeting.Topic = TopicTB.Text;
            foreach (Account a in Participants)
            {
                meeting.Participants.Add(a);
            }
            List<Account> busyDoctors = (List<Account>)app.ScheduledAppointmentController.GetBusyDoctors(meeting);
            if(busyDoctors.Count == 0)
            {
                Meeting m = app.MeetingController.Create(meeting);
                _homePagePatients.SecretaryFrame.Content = new SchedulePage(_homePagePatients);
                return;
            }
            TimeCB.SelectedIndex = -1;
            MainWindow.Modal.Content = new CreateMeetingControl(meeting, busyDoctors, _homePagePatients);
            MainWindow.Modal.IsOpen = true;

        }
    }
}
