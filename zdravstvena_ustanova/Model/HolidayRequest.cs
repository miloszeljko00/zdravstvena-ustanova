using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Model.Enums;

namespace zdravstvena_ustanova.Model
{
    public class HolidayRequest
    {
        public long Id { get; set; }
        public string Cause { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public HolidayRequestStatus HolidayRequestStatus { get; set; }

        public HolidayRequest(long id, string cause, DateTime startDate, DateTime endDate, HolidayRequestStatus holidayRequestStatus)
        {
            Id = id;
            Cause = cause;
            StartDate = startDate;
            EndDate = endDate;
            HolidayRequestStatus = holidayRequestStatus;
        }
    }
}
