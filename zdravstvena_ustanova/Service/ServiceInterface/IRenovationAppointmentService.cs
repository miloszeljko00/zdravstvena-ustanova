using System;
using System.Collections.Generic;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Service.ServiceInterface;

public interface IRenovationAppointmentService : IService<RenovationAppointment>
{
    IEnumerable<RenovationAppointment> GetFromToDates(DateTime start, DateTime end);
    IEnumerable<RenovationAppointment> GetIfContainsDateForRoom(DateTime date, long roomId);
    IEnumerable<RenovationAppointment> GetIfContainsDate(DateTime date);
    RenovationAppointment ScheduleRenovation(RenovationAppointment renovationAppointment);
    int NumberOfScheduledAppointmentsFromToForRoom(Room room, DateTime from, DateTime to);
    bool HasRenovationFromTo(Room room, DateTime from, DateTime to);
    IEnumerable<RenovationAppointment> GetRenovationAppointmentsByMergeRoomForMergeRenovation(long roomId);
}