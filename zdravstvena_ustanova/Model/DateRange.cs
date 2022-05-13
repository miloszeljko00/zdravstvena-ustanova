using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdravstvena_ustanova.Model
{
    public class DateRange
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public DateRange(DateTime start, DateTime end)
        {
            ValidateIfStartIsEarlier(start, end);
            Start = start;
            End = end;
        }

        private static void ValidateIfStartIsEarlier(DateTime start, DateTime end)
        {
            if (end.CompareTo(start) < 0)
            {
                throw new ArgumentException("Start date must be earlier then end date!");
            }
        }
    }
}
