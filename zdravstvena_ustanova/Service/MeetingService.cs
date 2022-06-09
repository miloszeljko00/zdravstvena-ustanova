using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class MeetingService : IMeetingService
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IRoomRepository _roomRepository;


        public MeetingService(IMeetingRepository meetingRepository, IAccountRepository accountRepository, IRoomRepository roomRepository)
        {
            _meetingRepository = meetingRepository;
            _accountRepository = accountRepository;
            _roomRepository = roomRepository;
        }

        public IEnumerable<Meeting> GetAll()
        {
            var meetings = _meetingRepository.GetAll();
            var participants = _accountRepository.GetAll();
            var rooms = _roomRepository.GetAll();
            BindMeetingsWithRooms(meetings, rooms);
            BindMeetingsWithParticipants(meetings, participants);
            return meetings;
        }

        private void BindMeetingsWithRooms(IEnumerable<Meeting> meetings, IEnumerable<Room> rooms)
        {
            foreach (Meeting m in meetings)
            {
                BindMeetingWithRoom(m, rooms);
            }
        }
        private void BindMeetingWithRoom(Meeting meeting, IEnumerable<Room> rooms)
        {
            foreach(Room r in rooms)
            {
                if(r.Id == meeting.Room.Id)
                {
                    meeting.Room = r;
                    return;
                }
            }
        }


        private void BindMeetingsWithParticipants(IEnumerable<Meeting> meetings, IEnumerable<Account> participants)
        {
            foreach (Meeting m in meetings)
            {
                BindMeetingWithParticipants(m, participants);
            }
        }

        public Meeting Get(long id)
        {
            var meeting = _meetingRepository.Get(id);
            var participants = _accountRepository.GetAll();
            var rooms = _roomRepository.GetAll();
            BindMeetingWithRoom(meeting, rooms);
            BindMeetingWithParticipants(meeting, participants);
            return meeting;
        }

        private void BindMeetingWithParticipants(Meeting meeting, IEnumerable<Account> accounts)
        {
            List<Account> _participants = new List<Account>(meeting.Participants);
            meeting.Participants.Clear();
            foreach (Account p1 in _participants)
            {
                foreach (Account p2 in accounts)
                {
                    if (p1.Id == p2.Id)
                    {
                        meeting.Participants.Add(p2);
                        break;
                    }
                }
            }       
        }

        public Meeting Create(Meeting meeting)
        {
            return _meetingRepository.Create(meeting);
        }
        public bool Update(Meeting meeting)
        {
            return _meetingRepository.Update(meeting);
        }
        public bool Delete(long meetingId)
        {
            return _meetingRepository.Delete(meetingId);
        }
    }
}
