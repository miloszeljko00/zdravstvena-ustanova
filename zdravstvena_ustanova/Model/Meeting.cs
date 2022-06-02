using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdravstvena_ustanova.Model
{
    public class Meeting
    {
        public long Id { get; set; }
        public DateTime Time { get; set; }
        public Room Room { get; set; }

        public string Topic { get; set; }
        public List<Account> Participants { get; set; }

        public Meeting(long id, DateTime time, Room room, string topic, List<Account> participants)
        {
            Id = id;
            Time = time;
            Room = room;
            Topic = topic;
            Participants = participants;
        }
        public Meeting(long id, DateTime time, long roomId, string topic, List<Account> participants)
        {
            Id = id;
            Time = time;
            Room = new Room(roomId);
            Topic = topic;
            Participants = participants;
        }


        public Meeting(long id, DateTime time, Room room, string topic)
        {
            Id = id;
            Time = time;
            Room = room;
            Topic = topic;
            Participants = new List<Account>();
        }

        public Meeting(long id, DateTime time, long roomId, string topic)
        {
            Id = id;
            Time = time;
            Room = new Room(roomId);
            Topic = topic;
            Participants = new List<Account>();
        }
    }
}
