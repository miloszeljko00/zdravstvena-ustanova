using System;
using Service;
using Model;
using System.Collections.Generic;
using System.Linq;

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

        public List<string> GetAppropriateTimes(DateTime dateTime, Doctor doctor, Patient patient, Room room)
        {
            string[] times = {"08:00", "09:00", "10:00", "11:00", "12:00",
                                "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00" };
            

            var _scheduledAppointments = GetAll();
            foreach(ScheduledAppointment sa in _scheduledAppointments)
            {
                if(sa.Start.Date == dateTime)
                {
                    if(sa.Doctor.Id == doctor.Id)
                    {
                        int time = sa.Start.Hour;
                        time -= 8;
                        times[time] = "-1";
                        continue;
                    }
                    if(sa.Room.Id == room.Id)
                    {
                        int time = sa.Start.Hour;
                        time -= 8;
                        times[time] = "-1";
                        continue;
                    }
                    if(sa.Patient.Id == patient.Id)
                    {
                        int time = sa.Start.Hour;
                        time -= 8;
                        times[time] = "-1";
                    }
                }
            }
            List<string> t = new List<string>();
            for (int i=0; i<14; i++)
            {
                if (String.Compare(times[i], "-1") != 0)
                    t.Add(times[i]);
            }
            return t;
        }
    }
}