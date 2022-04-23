using System;

namespace Model
{
   public class PrescribedMedicine
   {
        public int TimesPerDay { get; set; }
        public int OnHours { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
      
        public Medication Medication { get; set; }

        public PrescribedMedicine(int timesPerDay, int onHours, DateTime endDate, string description, Medication medication)
        {
            TimesPerDay = timesPerDay;
            OnHours = onHours;
            EndDate = endDate;
            Description = description;
            Medication = medication;
        }
    }
}