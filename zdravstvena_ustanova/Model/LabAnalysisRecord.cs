using System;
using System.Collections.Generic;

namespace Model
{
   public class LabAnalysisRecord
   {
      public long Id { get; set; }
      public DateTime Date { get; set; }
      
      //public List<LabAnalysisComponent> LabAnalysisComponent { get; set; }

        /*public LabAnalysisRecord(long id, DateTime date)
        {
            Id = id;
            Date = date;
            LabAnalysisComponent = new List<LabAnalysisComponent>();
        }*/
    }
}