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
        public bool? IsUrgent { get; set; }
        public Doctor Doctor { get; set; }
        public string ReasonForDenial { get; set; }


        public HolidayRequest(long id, string cause, DateTime startDate, DateTime endDate,
            HolidayRequestStatus holidayRequestStatus, bool? isUrgent, Doctor doctor, string reasonForDenial)
        {
            Id = id;
            Cause = cause;
            StartDate = startDate;
            EndDate = endDate;
            HolidayRequestStatus = holidayRequestStatus;
            IsUrgent = isUrgent;
            Doctor = doctor;
            ReasonForDenial = reasonForDenial;
        }
        public HolidayRequest(long id, string cause, DateTime startDate, DateTime endDate,
            HolidayRequestStatus holidayRequestStatus, bool? isUrgent, long doctorId, string reasonForDenial)
        {
            Id = id;
            Cause = cause;
            StartDate = startDate;
            EndDate = endDate;
            HolidayRequestStatus = holidayRequestStatus;
            IsUrgent = isUrgent;
            Doctor = new Doctor(doctorId);
            ReasonForDenial = reasonForDenial;
        }

        public HolidayRequest(string cause, DateTime startDate, DateTime endDate,
            HolidayRequestStatus holidayRequestStatus, bool? isUrgent, Doctor doctor, string reasonForDenial)
        {
            Cause = cause;
            StartDate = startDate;
            EndDate = endDate;
            HolidayRequestStatus = holidayRequestStatus;
            IsUrgent = isUrgent;
            Doctor = doctor;
            ReasonForDenial = reasonForDenial;
        }
    }
}
