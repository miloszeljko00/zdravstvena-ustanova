using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace zdravstvena_ustanova.View.Model
{
    public class AppointmentsWeeklyByHour
    {
        public DateTime DateOfWeekStart { get; set; }
        public ScheduledAppointment MondayAppointment { get; set; }
        public ScheduledAppointment TuesdayAppointment { get; set; }
        public ScheduledAppointment WednesdayAppointment { get; set; }
        public ScheduledAppointment ThursdayAppointment { get; set; }
        public ScheduledAppointment FridayAppointment { get; set; }
        public ScheduledAppointment SaturdayAppointment { get; set; }
        public ScheduledAppointment SundayAppointment { get; set; }

        public AppointmentsWeeklyByHour(DateTime dateOfWeekStart)
        {
            DateOfWeekStart = dateOfWeekStart;
        }
    }
}
