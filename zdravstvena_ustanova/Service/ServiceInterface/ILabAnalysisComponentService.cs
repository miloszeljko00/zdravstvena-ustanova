using System.Collections.Generic;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Service.ServiceInterface;

public interface ILabAnalysisComponentService : IService<LabAnalysisComponent>
{
    LabAnalysisComponent FindLabAnalysisComponentById(IEnumerable<LabAnalysisComponent> labAnalysisComponents, long id);
}