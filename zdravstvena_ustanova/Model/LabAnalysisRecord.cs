using System;
using System.Collections.Generic;
using zdravstvena_ustanova.Model;

namespace Model
{
   public class LabAnalysisRecord
   {
      public long Id { get; set; }
      public DateTime Date { get; set; }
      
      public List<LabAnalysisComponent> LabAnalysisComponent { get; set; }

        public LabAnalysisRecord(long id, DateTime date)
        {
            Id = id;
            Date = date;
            LabAnalysisComponent = new List<LabAnalysisComponent>();
        }

        public LabAnalysisRecord(long id)
        {
            Id = id;
            LabAnalysisComponent = new List<LabAnalysisComponent>();
        }
    }
}