using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Service.ServiceInterface;

public interface IHealthRecordService : IService<HealthRecord>
{
    HealthRecord FindHealthRecordByPatient(long patientId);
}