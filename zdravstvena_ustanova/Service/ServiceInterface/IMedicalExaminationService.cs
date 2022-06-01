using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Service.ServiceInterface;

public interface IMedicalExaminationService : IService<MedicalExamination>
{
    MedicalExamination FindByScheduledAppointmentId(long id);
}