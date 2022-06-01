using System;
using System.Collections.Generic;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Service.ServiceInterface;

public interface IScheduledAppointmentService : IService<ScheduledAppointment>
{
    IEnumerable<ScheduledAppointment> GetAllUnbound();
    IEnumerable<ScheduledAppointment> GetFromToDates(DateTime start, DateTime end);
    IEnumerable<ScheduledAppointment> GetFromToDatesForRoom(DateTime start, DateTime end, long roomId);
    IEnumerable<ScheduledAppointment> GetScheduledAppointmentsForPatient(long patientId);
    ScheduledAppointment GetScheduledAppointmentsForDate(DateTime date, long patientId);
}