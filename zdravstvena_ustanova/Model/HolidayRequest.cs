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
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public HolidayRequestStatus HolidayRequestStatus { get; set; }
        public bool? IsUrgent { get; set; }
        public Doctor Doctor { get; set; }


        public HolidayRequest(long id, string cause, DateTime? startDate, DateTime? endDate,
            HolidayRequestStatus holidayRequestStatus, bool? isUrgent, Doctor doctor)
        {
            Id = id;
            Cause = cause;
            StartDate = startDate;
            EndDate = endDate;
            HolidayRequestStatus = holidayRequestStatus;
            IsUrgent = isUrgent;
            Doctor = doctor;
        }
        public HolidayRequest(long id, string cause, DateTime? startDate, DateTime? endDate,
            HolidayRequestStatus holidayRequestStatus, bool? isUrgent, long doctorId)
        {
            Id = id;
            Cause = cause;
            StartDate = startDate;
            EndDate = endDate;
            HolidayRequestStatus = holidayRequestStatus;
            IsUrgent = isUrgent;
            Doctor = new Doctor(doctorId);
        }

        public HolidayRequest(string cause, DateTime? startDate, DateTime? endDate,
            HolidayRequestStatus holidayRequestStatus, bool? isUrgent, Doctor doctor)
        {
            Cause = cause;
            StartDate = startDate;
            EndDate = endDate;
            HolidayRequestStatus = holidayRequestStatus;
            IsUrgent = isUrgent;
            Doctor = doctor;
        }
    }
}
