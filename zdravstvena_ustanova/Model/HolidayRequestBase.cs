using System;

namespace zdravstvena_ustanova.Model
{
    public class HolidayRequestBase
    {
        public long Id { get; set; }
        public string Cause { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}