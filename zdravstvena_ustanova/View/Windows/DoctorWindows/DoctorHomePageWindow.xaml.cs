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
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;
using zdravstvena_ustanova.View.Model;
using Model;

namespace zdravstvena_ustanova.View.Windows.DoctorWindows
{
    /// <summary>
    /// Interaction logic for DoctorHomePageWindow.xaml
    /// </summary>
    public partial class DoctorHomePageWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection <AppointmentsWeeklyByHour> appointmentsWeeklyByHours { get; set; }
        #region NotifyProperties
        private string _name;
        public string Username
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Username");
                }
            }
        }
        #endregion

        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        public DoctorHomePageWindow()
        {
            InitializeComponent();
            DataContext = this;
            var app = Application.Current as App;
            Username = app.LoggedInUser.Name;

            //AppointmentsWeeklyByHour proba1 = new AppointmentsWeeklyByHour();
            //List<ScheduledAppointment> proba2 = (List<ScheduledAppointment>)app.ScheduledAppointmentController.GetAll();
            //proba1.DateOfWeekStart = proba2.First().Start;
            //proba1.MondayAppointment = proba2.First();
            //proba2.Remove(proba2.First());
            //proba1.TuesdayAppointment = proba2.First();
            //proba2.Remove(proba2.First());
            //proba1.WednesdayAppointment = proba2.First();
            //proba2.Remove(proba2.First());
            //proba1.ThursdayAppointment = proba2.First();
            //proba2.Remove(proba2.First());
            //appointmentsWeeklyByHours = new ObservableCollection<AppointmentsWeeklyByHour>();
            //appointmentsWeeklyByHours.Add(proba1);

            DateTime todayDate = DateTime.Now;
            DateTime date;
            date = new DateTime(todayDate.Year, todayDate.Month, todayDate.Day, 8, 0, 0);
            if (todayDate.DayOfWeek == 0)
            {
                date = date.AddDays(6);
            }
            else
            {
                date = date.AddDays(1 -(int)todayDate.DayOfWeek);
            }
            appointmentsWeeklyByHours = new ObservableCollection<AppointmentsWeeklyByHour>();
            dataGridScheduledAppointments.ItemsSource = appointmentsWeeklyByHours;
            appointmentsWeeklyByHours.Add(new AppointmentsWeeklyByHour(date));
            for(int i = 1;i<14;i++)
            {
                appointmentsWeeklyByHours.Add(new AppointmentsWeeklyByHour(new DateTime(date.Year,date.Month,date.Day,date.Hour+i,0,0)));
            }
            DateTime endDateOfWeek = new DateTime(date.Year, date.Month, date.Day, 21, 0, 0);
            endDateOfWeek = endDateOfWeek.AddDays(6);
            var scheduledAppointments = app.ScheduledAppointmentController.GetFromToDates(date, endDateOfWeek);

            foreach(ScheduledAppointment sa in scheduledAppointments)
            {
                foreach(AppointmentsWeeklyByHour awbh in appointmentsWeeklyByHours)
                {
                    if(sa.Start.Hour==awbh.DateOfWeekStart.Hour)
                    {
                        if(sa.Start.Day==awbh.DateOfWeekStart.Day)
                        {
                            awbh.MondayAppointment = sa;
                        } else if (sa.Start.Day==awbh.DateOfWeekStart.Day+1)
                        {
                            awbh.TuesdayAppointment = sa;
                        }
                        else if (sa.Start.Day == awbh.DateOfWeekStart.Day + 2)
                        {
                            awbh.WednesdayAppointment = sa;
                        }
                        else if (sa.Start.Day == awbh.DateOfWeekStart.Day + 3)
                        {
                            awbh.ThursdayAppointment = sa;
                        }
                        else if (sa.Start.Day == awbh.DateOfWeekStart.Day + 4)
                        {
                            awbh.FridayAppointment = sa;
                        }
                        else if (sa.Start.Day == awbh.DateOfWeekStart.Day + 5)
                        {
                            awbh.SaturdayAppointment = sa;
                        }
                        else if (sa.Start.Day == awbh.DateOfWeekStart.Day + 6)
                        {
                            awbh.SundayAppointment = sa;
                        }
                    }
                }
            }
            CollectionViewSource.GetDefaultView(dataGridScheduledAppointments.ItemsSource).Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dataGridScheduledAppointments_SelectionChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var selectedCellIndex = (int)dataGridScheduledAppointments.SelectedCells[0].Column.DisplayIndex;
            AppointmentsWeeklyByHour awbh = (AppointmentsWeeklyByHour)dataGridScheduledAppointments.SelectedCells[0].Item;
            ScheduledAppointment sa = null;

            switch (selectedCellIndex)
            {
                case 1:
                    sa = awbh.MondayAppointment;
                    break;
                case 2:
                    sa = awbh.TuesdayAppointment;
                    break;
                case 3:
                    sa = awbh.WednesdayAppointment;
                    break;
                case 4:
                    sa = awbh.ThursdayAppointment;
                    break;
                case 5:
                    sa = awbh.FridayAppointment;
                    break;
                case 6:
                    sa = awbh.SaturdayAppointment;
                    break;
                case 7:
                    sa = awbh.SundayAppointment;
                    break;
            }

            if (sa != null)
            {
                ScheduledAppointmentWindow scheduledAppointmentWindow = new ScheduledAppointmentWindow(sa);
                scheduledAppointmentWindow.Show();
            } else
            {
                CreateNewAppointment createNewAppointment = new CreateNewAppointment();
                createNewAppointment.Show();
            }
        }
    }
}
