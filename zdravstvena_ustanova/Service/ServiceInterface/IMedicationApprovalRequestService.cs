using System.Collections.Generic;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;

namespace zdravstvena_ustanova.Service.ServiceInterface;

public interface IMedicationApprovalRequestService : IService<MedicationApprovalRequest>
{
    IEnumerable<MedicationApprovalRequest> GetAllByApprovingDoctorId(long doctorId);
    IEnumerable<MedicationApprovalRequest> GetByRequestStatus(RequestStatus requestStatus);
    bool CheckIfAlreadyWaitingForApproval(Medication medication);
}