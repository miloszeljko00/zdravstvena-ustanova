using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Controller
{
    public class MeetingController
    {
        private readonly IMeetingService _meetingService;

        public MeetingController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        public IEnumerable<Meeting> GetAll()
        {
            return _meetingService.GetAll();
        }
        public Meeting GetById(long id)
        {
            return _meetingService.Get(id);
        }
        public Meeting Create(Meeting meeting)
        {
            return _meetingService.Create(meeting);
        }
        public bool Update(Meeting meeting)
        {
            return _meetingService.Update(meeting);
        }
        public bool Delete(long meetingId)
        {
            return _meetingService.Delete(meetingId);
        }
    }
}
