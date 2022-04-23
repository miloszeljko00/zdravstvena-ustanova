using System;

namespace Model
{
   public class PatientVaccination
   {
        public long Id { get; set; }
        public DateTime DateOfVaccination { get; set; }
      
      /*public Vaccine Vaccine { get; set; }

        public PatientVaccination(long id, DateTime dateOfVaccination, Vaccine vaccine)
        {
            Id = id;
            DateOfVaccination = dateOfVaccination;
            Vaccine = vaccine;
        }*/
    }
}