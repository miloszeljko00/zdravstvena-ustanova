using System;

namespace zdravstvena_ustanova.Model
{
   public class PrescribedMedicine
   {
        public long Id { get; set; }
        public int TimesPerDay { get; set; }
        public int OnHours { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
      
        public Medication Medication { get; set; }

        public PrescribedMedicine(long id, int timesPerDay, int onHours, DateTime endDate, string description, Medication medication)
        {
            Id = id;
            TimesPerDay = timesPerDay;
            OnHours = onHours;
            EndDate = endDate;
            Description = description;
            Medication = medication;
        }
        public PrescribedMedicine(long id, int timesPerDay, int onHours, DateTime endDate, string description, long medicationId)
        {
            Id = id;
            TimesPerDay = timesPerDay;
            OnHours = onHours;
            EndDate = endDate;
            Description = description;
            Medication = new Medication(medicationId);
        }

        public PrescribedMedicine(long id)
        {
            Id = id;
        }
    }
}