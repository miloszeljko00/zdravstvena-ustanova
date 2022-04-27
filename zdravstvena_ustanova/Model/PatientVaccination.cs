using System;
using zdravstvena_ustanova.Model;

namespace Model
{
   public class PatientVaccination
   {
        public long Id { get; set; }
        public DateTime DateOfVaccination { get; set; }
      
        public Vaccine Vaccine { get; set; }

        public PatientVaccination(long id, DateTime dateOfVaccination, Vaccine vaccine)
        {
            Id = id;
            DateOfVaccination = dateOfVaccination;
            Vaccine = vaccine;
        }
        public PatientVaccination(long id, DateTime dateOfVaccination, long vaccineId)
        {
            Id = id;
            DateOfVaccination = dateOfVaccination;
            Vaccine = new Vaccine(vaccineId);
        }

        public PatientVaccination(long id)
        {
            Id = id;
        }
    }
}