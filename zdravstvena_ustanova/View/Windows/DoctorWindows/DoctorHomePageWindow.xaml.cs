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
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.View.Pages;
using zdravstvena_ustanova.Model.Enums;
using System.Threading;
using zdravstvena_ustanova.View.Windows.DoctorWindows.View;

namespace zdravstvena_ustanova.View.Windows.DoctorWindows
{
    /// <summary>
    /// Interaction logic for DoctorHomePageWindow.xaml
    /// </summary>
    public partial class DoctorHomePageWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection <AppointmentsWeeklyByHour> appointmentsWeeklyByHours { get; set; }

        public ObservableCollection <MedicationApprovalRequest> MedicationApprovalRequests { get; set; }
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
        private int _numberOfUnSeenRequests;
        public int NumberOfUnSeenRequests
        {
            get
            {
                return _numberOfUnSeenRequests;
            }
            set
            {
                if (value != _numberOfUnSeenRequests)
                {
                    _numberOfUnSeenRequests = value;
                    OnPropertyChanged("NumberOfUnSeenRequests");
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
            NumberOfUnSeenRequests = 0;
            var app = Application.Current as App;
            Username = app.LoggedInUser.Name;
            MedicationApprovalRequests = new ObservableCollection<MedicationApprovalRequest>();
            CheckForNewMedicationApprovalRequest();
            StartCheckingForNewMedicationApprovalRequest(500);
            UpdateCalendar();
        }

        public void UpdateCalendar()
        {
            var app = Application.Current as App;
            DateTime todayDate = DateTime.Now;
            DateTime date;
            date = new DateTime(todayDate.Year, todayDate.Month, todayDate.Day, 8, 0, 0);
            if (todayDate.DayOfWeek == 0)
            {
                date = date.AddDays(6);
            }
            else
            {
                date = date.AddDays(1 - (int)todayDate.DayOfWeek);
            }
                appointmentsWeeklyByHours = new ObservableCollection<AppointmentsWeeklyByHour>();
                dataGridScheduledAppointments.ItemsSource = appointmentsWeeklyByHours;
                appointmentsWeeklyByHours.Add(new AppointmentsWeeklyByHour(date));
                for (int i = 1; i < 14; i++)
                {
                    appointmentsWeeklyByHours.Add(new AppointmentsWeeklyByHour(new DateTime(date.Year, date.Month, date.Day, date.Hour + i, 0, 0)));
                }
                DateTime endDateOfWeek = new DateTime(date.Year, date.Month, date.Day, 21, 0, 0);
                endDateOfWeek = endDateOfWeek.AddDays(6);
                var scheduledAppointments = new List<ScheduledAppointment>();
                var scheduledAppointmentsFromTo = app.ScheduledAppointmentController.GetFromToDates(date, endDateOfWeek);


                foreach (ScheduledAppointment sa in scheduledAppointmentsFromTo)
                {
                    if (sa.Doctor.Id == app.LoggedInUser.Id)
                    {
                        scheduledAppointments.Add(sa);
                    }
                }

                foreach (ScheduledAppointment sa in scheduledAppointments)
                {
                    foreach (AppointmentsWeeklyByHour awbh in appointmentsWeeklyByHours)
                    {
                        if (sa.Start.Hour == awbh.DateOfWeekStart.Hour)
                        {
                            if (sa.Start.Day == awbh.DateOfWeekStart.Day)
                            {
                                awbh.MondayAppointment = sa;
                            }
                            else if (sa.Start.Day == awbh.DateOfWeekStart.Day + 1)
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
        private void dataGridScheduledAppointments_SelectionChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dataGridScheduledAppointments.SelectedCells.Count == 0) return;
            var selectedCellIndex = (int)dataGridScheduledAppointments.SelectedCells[0].Column.DisplayIndex;
            AppointmentsWeeklyByHour awbh = (AppointmentsWeeklyByHour)dataGridScheduledAppointments.SelectedCells[0].Item;
            ScheduledAppointment sa = null;

            switch (selectedCellIndex)
            {
                case 0:
                    return;
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
                ScheduledAppointmentWindow scheduledAppointmentWindow = new ScheduledAppointmentWindow(sa, this);
                scheduledAppointmentWindow.ShowDialog();
            } else
            {
                CreateNewAppointment createNewAppointment = new CreateNewAppointment(this);
            }
            dataGridScheduledAppointments.SelectedCells.Clear();
        }

        private void MenuItem_Click_Logout(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Button_Click_Make_An_Appointment(object sender, RoutedEventArgs e)
        {
            CreateNewAppointment createNewAppointment = new CreateNewAppointment(this, "sramota koje stelovanje");
        }

        private void MenuItem_MouseEnter(object sender, MouseEventArgs e)
        {
            MedicationApprovalRequestsListView.Visibility = Visibility.Visible;

        }

        private void MenuItem_MouseLeave(object sender, MouseEventArgs e)
        {
            MedicationApprovalRequestsListView.Visibility = Visibility.Hidden;
        }

        private void MedicationNameTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var medicationApprovalRequestTextBlock = (TextBlock)e.OriginalSource;
            var medicationApprovalRequest = (MedicationApprovalRequest)medicationApprovalRequestTextBlock.DataContext;
            medicationApprovalRequest.IsSeenByDoctor = true;
            MedicationApprovalRequests.Remove(medicationApprovalRequest);
            MedicationApprovalRequests.Add(medicationApprovalRequest);
            var app = Application.Current as App;
            app.MedicationApprovalRequestController.Update(medicationApprovalRequest);
            MedicationApprovalRequestWindow medicationApprovalRequestWindow = new MedicationApprovalRequestWindow(medicationApprovalRequest, MedicationApprovalRequests);
            medicationApprovalRequestWindow.ShowDialog();
        }
        public async void StartCheckingForNewMedicationApprovalRequest(int numberOfSecondsBetweenTwoChecks)
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(numberOfSecondsBetweenTwoChecks));
            while (await timer.WaitForNextTickAsync())
            {
                CheckForNewMedicationApprovalRequest();
            }
        }
        private void CheckForNewMedicationApprovalRequest()
        {
            var app = Application.Current as App;
            List<MedicationApprovalRequest> medicationApprovalRequests2 = (List<MedicationApprovalRequest>)app.MedicationApprovalRequestController.GetAll();
            MedicationApprovalRequests.Clear();
            NumberOfUnSeenRequests = 0;
            foreach (MedicationApprovalRequest medicationApprovalRequest in medicationApprovalRequests2)
            {
                if (medicationApprovalRequest.RequestStatus == RequestStatus.WAITING_FOR_APPROVAL)
                {
                    MedicationApprovalRequests.Add(medicationApprovalRequest);
                    if (medicationApprovalRequest.IsSeenByDoctor == false)
                    {
                        NumberOfUnSeenRequests++;
                    }
                }
            }
        }

        private void MenuItem_Click_ProfileAndPersonalData(object sender, RoutedEventArgs e)
        {
            
            ProfileAndPersonalDataWindow profileAndPersonalDataWindow = new ProfileAndPersonalDataWindow();
            profileAndPersonalDataWindow.Show();

        }

        private void Button_Click_MedicalSupplyInventory(object sender, RoutedEventArgs e)
        {
            MedicalSupplyInventoryWindow medicalSupplyInventoryWindow = new MedicalSupplyInventoryWindow();
            medicalSupplyInventoryWindow.ShowDialog();
        }
    }
}
