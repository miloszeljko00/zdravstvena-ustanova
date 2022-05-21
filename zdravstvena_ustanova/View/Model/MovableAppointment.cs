using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.View.Model
{
    public class MovableAppointment
    {
        public ScheduledAppointment ScheduledAppointment { get; set; }

        public DateTime NewTime { get; set; }

        public MovableAppointment(ScheduledAppointment scheduledAppointment, DateTime newTime)
        {
            ScheduledAppointment = scheduledAppointment;
            NewTime = newTime;
        }

        public MovableAppointment(ScheduledAppointment scheduledAppointment)
        {
            ScheduledAppointment = scheduledAppointment;
        }
    }
}
