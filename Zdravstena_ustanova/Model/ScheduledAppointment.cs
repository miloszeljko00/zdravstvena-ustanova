using Model.Enums;
using System;

namespace Model
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
        public long PatientId { get; set; }
        public long DoctorId { get; set; }
        public long RoomId { get; set; }

        public ScheduledAppointment(DateTime start, DateTime end, AppointmentType appointmentType, long id, Patient patient, Doctor doctor, Room room, long patientId, long doctorId, long roomId)
        {
            Start = start;
            End = end;
            AppointmentType = appointmentType;
            Id = id;
            Patient = patient;
            Doctor = doctor;
            Room = room;
            PatientId = patientId;
            DoctorId = doctorId;
            RoomId = roomId;
        }

        public ScheduledAppointment(DateTime start, DateTime end, AppointmentType appointmentType, long id, long patientId, long doctorId, long roomId)
        {
            Start = start;
            End = end;
            AppointmentType = appointmentType;
            Id = id;
            PatientId = patientId;
            DoctorId = doctorId;
            RoomId = roomId;
        }
        public ScheduledAppointment(DateTime start, DateTime end, AppointmentType appointmentType, long patientId, long doctorId, long roomId)
        {
            Start = start;
            End = end;
            AppointmentType = appointmentType;
            PatientId = patientId;
            DoctorId = doctorId;
            RoomId = roomId;
        }

    }
}