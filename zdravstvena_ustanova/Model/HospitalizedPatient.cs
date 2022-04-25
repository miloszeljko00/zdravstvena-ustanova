using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdravstvena_ustanova.Model
{
    public class HospitalizedPatient
    {
        public long Id { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string Cause { get; set; }

        public Patient Patient { get; set; }
        public Room Room { get; set; }

        public HospitalizedPatient(long id, DateTime admissionDate, string cause, Patient patient, Room room)
        {
            Id = id;
            AdmissionDate = admissionDate;
            Cause = cause;
            Patient = patient;
            Room = room;
        }
        public HospitalizedPatient(long id, DateTime admissionDate, string cause, long patientId, long roomId)
        {
            Id = id;
            AdmissionDate = admissionDate;
            Cause = cause;
            Patient = new Patient(patientId);
            Room = new Room(roomId);
        }

    }
}
