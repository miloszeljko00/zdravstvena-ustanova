using zdravstvena_ustanova.Model;
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
        public Room FirstRoom { get; set; }
        public Room SecondRoom { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public RenovationType RenovationType { get; set; }

        public RenovationAppointment(Room room, DateTime startDate, DateTime endDate, 
            string description, RenovationType renovationType)
        {
            Room = room;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            RenovationType = renovationType;
        }
        public RenovationAppointment(long id, long roomId, DateTime startDate, DateTime endDate, 
            string description, long renovationTypeId)
        {
            Id = id;
            Room = new Room(roomId);
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            RenovationType = new RenovationType(renovationTypeId);
        }

        public RenovationAppointment(Room room, Room firstRoom, Room secondRoom, DateTime startDate,
            DateTime endDate, string description, RenovationType renovationType)
        {
            Room = room;
            FirstRoom = firstRoom;
            SecondRoom = secondRoom;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            RenovationType = renovationType;
        }

        public RenovationAppointment(long id, long roomId, long firstRoomId, long secondRoomId, DateTime startDate,
            DateTime endDate, string description, long renovationTypeId)
        {
            Id = id;
            Room = new Room(roomId);
            FirstRoom = new Room(firstRoomId);
            SecondRoom = new Room(secondRoomId);
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            RenovationType = new RenovationType(renovationTypeId);
        }
    }
}
