using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Model.Enums;

namespace zdravstvena_ustanova.Model
{
    public class MedicationApprovalRequest
    {
        public long Id { get; set; }
        public Medication Medication { get; set; }
        public Doctor ApprovingDoctor { get; set; }
        public string RequestMessage { get; set; }
        public string ResponseMessage { get; set; }
        public RequestStatus RequestStatus { get; set; }

        public MedicationApprovalRequest(long id, Medication medication, Doctor approvingDoctor,
            string requestMessage, string responseMessage, RequestStatus requestStatus)
        {
            Id = id;
            Medication = medication;
            ApprovingDoctor = approvingDoctor;
            RequestMessage = requestMessage;
            ResponseMessage = responseMessage;
            RequestStatus = requestStatus;
        }
        public MedicationApprovalRequest(long id, long medicationId, long approvingDoctorId,
            string requestMessage, string responseMessage, RequestStatus requestStatus)
        {
            Id = id;
            Medication = new Medication(medicationId);
            ApprovingDoctor = new Doctor(approvingDoctorId);
            RequestMessage = requestMessage;
            ResponseMessage = responseMessage;
            RequestStatus = requestStatus;
        }

        public MedicationApprovalRequest(long id)
        {
            Id = id;
        }
    }
}