using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.View.Model;
using System.Net.Mime;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using LiveCharts.Wpf;

namespace zdravstvena_ustanova.View.Pages.ManagerPages
{
    /// <summary>
    /// Interaction logic for RoomGeneratedReportPage.xaml
    /// </summary>
    public partial class RoomGeneratedReportPage : Page, INotifyPropertyChanged
    {
        public Room Room { get; set; }
        public ChartValues<int> NumberOfAppointments { get; set; }
        public ChartValues<string> Dates { get; set; }
        public DayOfWeek MostFrequentDayOfWeek { get; set; }
        #region
        private int _totalNumberOfAppointments;
        public int TotalNumberOfAppointments
        {
            get
            {
                return _totalNumberOfAppointments;
            }
            set
            {
                if (value != _totalNumberOfAppointments)
                {
                    _totalNumberOfAppointments = value;
                    OnPropertyChanged("TotalNumberOfAppointments");
                }
            }
        }
        private DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }
        private DateTime _endDate;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
                    OnPropertyChanged("EndDate");
                }
            }
        }
        private double _avgHoursDaily;
        public double AvgHoursDaily
        {
            get
            {
                return _avgHoursDaily;
            }
            set
            {
                if (value != _avgHoursDaily)
                {
                    _avgHoursDaily = value;
                    OnPropertyChanged("AvgHoursDaily");
                }
            }
        }
        private string _mostFrequentDay;
        public string MostFrequentDay
        {
            get
            {
                return _mostFrequentDay;
            }
            set
            {
                if (value != _mostFrequentDay)
                {
                    _mostFrequentDay = value;
                    OnPropertyChanged("MostFrequentDay");
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


        public RoomGeneratedReportPage(Room room, DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            DataContext = this;
            Room = room;
            StartDate = startDate;
            EndDate = endDate;
            var app = Application.Current as App;
            DateTime date = new DateTime(startDate.Year, startDate.Month, startDate.Day);
            Dates = new ChartValues<string>();
            NumberOfAppointments = new ChartValues<int>();
            AvgHoursDaily = 0;
            TotalNumberOfAppointments = 0;
            int numberOfDays = 0;
            int maxAppointmentCount = 0;
            while (date.CompareTo(endDate) < 0)
            {
                var appointments = (List<ScheduledAppointment>)app.ScheduledAppointmentController.GetFromToDatesForRoom(date, date.AddDays(1), Room.Id);
                Dates.Add(date.ToString("dd.MM.yyyy"));
                if (maxAppointmentCount < appointments.Count)
                {
                    maxAppointmentCount = appointments.Count;
                    MostFrequentDayOfWeek = date.DayOfWeek;
                }
                NumberOfAppointments.Add(appointments.Count);
                date = date.AddDays(1);
                numberOfDays++;
                TotalNumberOfAppointments+= appointments.Count;
            }

            if (numberOfDays != 0)
            {
                AvgHoursDaily = (double)TotalNumberOfAppointments / numberOfDays;
            }
            switch (MostFrequentDayOfWeek)
            {
                case DayOfWeek.Monday:
                    MostFrequentDay = "Ponedeljak";
                    break;
                case DayOfWeek.Tuesday:
                    MostFrequentDay = "Utorak";
                    break;
                case DayOfWeek.Wednesday:
                    MostFrequentDay = "Sreda";
                    break;
                case DayOfWeek.Thursday:
                    MostFrequentDay = "Četvrtak";
                    break;
                case DayOfWeek.Friday:
                    MostFrequentDay = "Petak";
                    break;
                case DayOfWeek.Saturday:
                    MostFrequentDay = "Subota";
                    break;
                case DayOfWeek.Sunday:
                    MostFrequentDay = "Nedelja";
                    break;

            }
        }

        private void PrintButton_OnClick(object sender, RoutedEventArgs e)
        {

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)AppointmentsChart.ActualWidth, (int)AppointmentsChart.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(AppointmentsChart);



            RoomsReportPDF roomsReportPdf = new RoomsReportPDF(Room, NumberOfAppointments, Dates,
                TotalNumberOfAppointments, StartDate, EndDate, AvgHoursDaily, MostFrequentDay, rtb);

            try
            {
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(roomsReportPdf.Report, "Izveštaj o zauzeću prostorije");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                roomsReportPdf.Close();
            }
        }
    }
}
