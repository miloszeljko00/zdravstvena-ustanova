using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdravstvena_ustanova.Model
{
    public class AntiTrollMechanism
    {
        public long Id { get; set; }
        public Patient Patient { get; set; }
        public int NumberOfDates { get; set; }
        public List<DateTime> DatesOfCanceledAppointments { get; set; }

        public AntiTrollMechanism(long id, Patient patient, int numberOfDates, List<DateTime> datesOfCanceledAppointments)
        {
            Id = id;
            Patient = patient;
            NumberOfDates = numberOfDates;
            DatesOfCanceledAppointments = datesOfCanceledAppointments;
        }
        public AntiTrollMechanism(long id, long patientId, int numberOfDates, List<DateTime> datesOfCanceledAppointments)
        {
            Id = id;
            Patient = new Patient(patientId);
            NumberOfDates = numberOfDates;
            DatesOfCanceledAppointments = datesOfCanceledAppointments;
        }

        public AntiTrollMechanism(long id, Patient patient, int numberOfDates)
        {
            Id = id;
            Patient = patient;
            NumberOfDates = numberOfDates;
            DatesOfCanceledAppointments = new List<DateTime>();
        }
        public AntiTrollMechanism(long id, long patientId, int numberOfDates)
        {
            Id = id;
            Patient = new Patient(patientId);
            NumberOfDates = numberOfDates;
            DatesOfCanceledAppointments = new List<DateTime>();
        }
    }
}
