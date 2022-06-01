using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Controller
{
    public class RenovationAppointmentController
    {
        private readonly IRenovationAppointmentService _renovationAppointmentService;

        public RenovationAppointmentController(IRenovationAppointmentService renovationAppointmentService)
        {
            _renovationAppointmentService = renovationAppointmentService;
        }

        public IEnumerable<RenovationAppointment> GetAll()
        {
            return _renovationAppointmentService.GetAll();
        }
        public RenovationAppointment GetById(long id)
        {
            return _renovationAppointmentService.Get(id);
        }
        public IEnumerable<RenovationAppointment> GetFromToDates(DateTime start, DateTime end)
        {
            return _renovationAppointmentService.GetFromToDates(start, end);
        }
        public IEnumerable<RenovationAppointment> GetIfContainsDateForRoom(DateTime date, long roomId)
        {
            return _renovationAppointmentService.GetIfContainsDateForRoom(date, roomId);
        }
        public IEnumerable<RenovationAppointment> GetIfContainsDate(DateTime date)
        {
            return _renovationAppointmentService.GetIfContainsDate(date);
        }
        public RenovationAppointment Create(RenovationAppointment renovationAppointment)
        {
            return _renovationAppointmentService.Create(renovationAppointment);
        }
        public bool Update(RenovationAppointment renovationAppointment)
        {
            return _renovationAppointmentService.Update(renovationAppointment);
        }
        public bool Delete(long renovationAppointmentId)
        {
            return _renovationAppointmentService.Delete(renovationAppointmentId);
        }

        public RenovationAppointment ScheduleRenovation(RenovationAppointment renovationAppointment)
        {
            return _renovationAppointmentService.ScheduleRenovation(renovationAppointment);
        }
        public int NumberOfScheduledAppointmentsFromToForRoom(Room room, DateTime from, DateTime to)
        {
            return _renovationAppointmentService.NumberOfScheduledAppointmentsFromToForRoom(room, from, to);
        }
        public bool HasRenovationFromTo(Room room, DateTime from, DateTime to)
        {
            return _renovationAppointmentService.HasRenovationFromTo(room, from, to);
        }

        public IEnumerable<RenovationAppointment> GetRenovationAppointmentsByMergeRoomForMergeRenovation(long roomId)
        {
            return _renovationAppointmentService.GetRenovationAppointmentsByMergeRoomForMergeRenovation(roomId);
        }
    }
}
