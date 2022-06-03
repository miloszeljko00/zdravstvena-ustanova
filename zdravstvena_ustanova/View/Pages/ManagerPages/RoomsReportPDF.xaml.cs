using System;
using System.Collections.Generic;
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
using LiveCharts;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.View.Pages.ManagerPages
{
    /// <summary>
    /// Interaction logic for RoomsReportPDF.xaml
    /// </summary>
    public partial class RoomsReportPDF : Window, INotifyPropertyChanged
    {
        public Room Room { get; set; }
        public ChartValues<int> NumberOfAppointments { get; set; }
        public ChartValues<string> Dates { get; set; }
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
        private RenderTargetBitmap _rtb;
        public RenderTargetBitmap Rtb
        {
            get
            {
                return _rtb;
            }
            set
            {
                if (value != _rtb)
                {
                    _rtb = value;
                    OnPropertyChanged("Rtb");
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

        public RoomsReportPDF(Room room, ChartValues<int> numberOfAppointments, ChartValues<string> dates,
            int totalNumberOfAppointments, DateTime startDate, DateTime endDate, double avgHoursDaily, string mostFrequentDay,
            RenderTargetBitmap rtb)
        {
            InitializeComponent();
            DataContext = this;
            Room = room;
            NumberOfAppointments = numberOfAppointments;
            Dates = dates;
            TotalNumberOfAppointments = totalNumberOfAppointments;
            StartDate = startDate;
            EndDate = endDate;
            AvgHoursDaily = avgHoursDaily;
            MostFrequentDay = mostFrequentDay;
            AppointmentsChart.Source = rtb;
        }
    }
}
