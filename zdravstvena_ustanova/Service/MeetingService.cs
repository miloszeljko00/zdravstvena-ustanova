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


        public MeetingService(IMeetingRepository meetingRepository, IAccountRepository accountRepository)
        {
            _meetingRepository = meetingRepository;
            _accountRepository = accountRepository;
        }

        public IEnumerable<Meeting> GetAll()
        {
            var meetings = _meetingRepository.GetAll();
            var participants = _accountRepository.GetAll();
            BindMeetingsWithParticipants(meetings, participants);
            return meetings;
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
