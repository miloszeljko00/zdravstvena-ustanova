using System;
using Service;
using Model;
using System.Collections.Generic;

namespace Controller
{
   public class ScheduledAppointmentController
   {
        private readonly ScheduledAppointmentService _scheduledAppointmentService;

        public ScheduledAppointmentController(ScheduledAppointmentService scheduledAppointmentService)
        {
            _scheduledAppointmentService = scheduledAppointmentService;
        }

        public IEnumerable<ScheduledAppointment> GetAll()
        {
            return _scheduledAppointmentService.GetAll();
        }
        public IEnumerable<ScheduledAppointment> GetFromToDates(DateTime start, DateTime end)
        {
            return _scheduledAppointmentService.GetFromToDates(start, end);
        }
        public IEnumerable<ScheduledAppointment> GetFromToDatesForRoom(DateTime start, DateTime end, long roomId)
        {
            return _scheduledAppointmentService.GetFromToDatesForRoom(start, end, roomId);
        }
        public ScheduledAppointment GetById(long Id)
        {
            return _scheduledAppointmentService.GetById(Id);
        }

        public ScheduledAppointment Create(ScheduledAppointment scheduledAppointment)
        {
            return _scheduledAppointmentService.Create(scheduledAppointment);
        }
        public void Update(ScheduledAppointment scheduledAppointment)
        {
            _scheduledAppointmentService.Update(scheduledAppointment);
        }
        public void Delete(long scheduledAppointmentId)
        {
            _scheduledAppointmentService.Delete(scheduledAppointmentId);
        }
    }
}