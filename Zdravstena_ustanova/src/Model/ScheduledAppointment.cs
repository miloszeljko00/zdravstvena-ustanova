using Model.Enums;
using System;

namespace Model
{
    /// U kodu za zakazivanje operacija treba da se omoguci da samo lekar specijalista zakazuje operacije
    [Serializable]
    public class ScheduledAppointment
    {
        public System.DateTime Start { get; set; }
        public System.DateTime End { get; set; }
        public Enums.AppointmentType AppointmentType { get; set; }

        public int AppointmentId { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor   { get; set; }
        public Room Room { get; set; }

        public ScheduledAppointment(DateTime start, DateTime end, AppointmentType appointmentType, int appointmentId, Patient patient, Doctor doctor, Room room)
        {
            Start = start;
            End = end;
            AppointmentType = appointmentType;
            AppointmentId = appointmentId;
            Patient = patient;
            Doctor = doctor;
            Room = room;
        }
    }
}