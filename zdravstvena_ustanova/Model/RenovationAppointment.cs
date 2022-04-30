using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RenovationAppointment
    {
        public long Id { get; set; }
        public Room Room { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }

        public RenovationAppointment()
        {
        }
        public RenovationAppointment(Room room, DateTime startDate, DateTime endDate, string description)
        {
            Room = room;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
        }
        public RenovationAppointment(long id, Room room, DateTime startDate, DateTime endDate, string description)
        {
            Id = id;
            Room = room;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
        }
        public RenovationAppointment(long id, long roomId, DateTime startDate, DateTime endDate, string description)
        {
            Id = id;
            Room = new Room(roomId);
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
        }
    }
}
