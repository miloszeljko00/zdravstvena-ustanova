using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.View.ManagerMVVM.Model
{
    public class MovementGraphModel : BindableBase
    {
        public ChartValues<int> NumberOfAppointments { get; set; }
        public ChartValues<string> Dates { get; set; }

        private int _totalNumberOfAppointments;
        public int TotalNumberOfAppointments
        {
            get => _totalNumberOfAppointments;
            set => SetProperty(ref _totalNumberOfAppointments, value);
        }
        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }
        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        public MovementGraphModel(DateTime start, DateTime end)
        {
            StartDate = start;
            EndDate = end;
            GetData();
        }

        private void GetData()
        {
            TotalNumberOfAppointments = 0;
            Dates = new ChartValues<string>();
            NumberOfAppointments = new ChartValues<int>();
            var app = Application.Current as App;

            var date = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day);
            while (date.CompareTo(EndDate) < 0)
            {
                var appointments = (List<ScheduledAppointment>)app.ScheduledAppointmentController.GetFromToDates(date, date.AddDays(1));
                Dates.Add(date.ToString("dd.MM.yyyy"));
                NumberOfAppointments.Add(appointments.Count);

                date = date.AddDays(1);
            }
        }
    }
}
