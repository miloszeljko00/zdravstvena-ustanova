using zdravstvena_ustanova.Model.Enums;
using System;

namespace zdravstvena_ustanova.Model
{
    public class ScheduledAppointment
    {
        public System.DateTime Start { get; set; }
        public System.DateTime End { get; set; }
        public Enums.AppointmentType AppointmentType { get; set; }

        public long Id { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor   { get; set; }
        public Room Room { get; set; }

        public ScheduledAppointment(DateTime start, DateTime end, AppointmentType appointmentType,
            long id, Patient patient, Doctor doctor, Room room)
        {
            Start = start;
            End = end;
            AppointmentType = appointmentType;
            Id = id;
            Patient = patient;
            Doctor = doctor;
            Room = room;
        }

        public ScheduledAppointment(DateTime start, DateTime end, AppointmentType appointmentType,
            long id, long patientId, long doctorId, long roomId)
        {
            Start = start;
            End = end;
            AppointmentType = appointmentType;
            Id = id;
            Patient = new Patient(patientId);
            Doctor = new Doctor(doctorId);
            Room = new Room(roomId);
        }

        public ScheduledAppointment(DateTime start, DateTime end, AppointmentType appointmentType,
            long patientId, long doctorId, long roomId)
        {
            Start = start;
            End = end;
            AppointmentType = appointmentType;
            Patient = new Patient(patientId);
            Doctor = new Doctor(doctorId);
            Room = new Room(roomId);
        }

        public ScheduledAppointment(long id)
        {
            Id=id;
        }

    }
}