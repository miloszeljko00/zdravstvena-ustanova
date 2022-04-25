using System;
using System.Collections.Generic;
using zdravstvena_ustanova.Model;

namespace Model
{
   public class MedicalExamination
   {
      public long Id { get; set; }
      
      public ScheduledAppointment ScheduledAppointment { get; set; }
      public Anamnesis Anamnesis { get; set; }
      public SpecialistRequest SpecialistRequest { get; set; }
      public LabAnalysisRequest LabAnalysisRequest { get; set; }
      public HospitalizationRequest HospitalizationRequest { get; set; }
      public List<PrescribedMedicine> PrescribedMedicine { get; set; }

        public MedicalExamination(long id, ScheduledAppointment scheduledAppointment, Anamnesis anamnesis, SpecialistRequest specialistRequest, LabAnalysisRequest labAnalysisRequest, HospitalizationRequest hospitalizationRequest)
        {
            Id = id;
            ScheduledAppointment = scheduledAppointment;
            Anamnesis = anamnesis;
            SpecialistRequest = specialistRequest;
            LabAnalysisRequest = labAnalysisRequest;
            HospitalizationRequest = hospitalizationRequest;
            PrescribedMedicine = new List<PrescribedMedicine>();
        }
    }
}