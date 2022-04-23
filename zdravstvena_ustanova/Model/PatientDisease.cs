using System;

namespace Model
{
   public class PatientDisease
   {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
      
        public Disease Disease { get; set; }

        public PatientDisease(long id, DateTime startDate, DateTime endDate, Disease disease)
        {
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
            Disease = disease;
        }
    }
}