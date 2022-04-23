using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdravstvena_ustanova.Model
{
    public class RenovationAppointment
    {
        public long Id { get; set; }
        public Room Room { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public RenovationAppointment(long id, Room room, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Room = room;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
