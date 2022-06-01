using System.Collections.Generic;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Service.ServiceInterface;

public interface IDiseaseService : IService<Disease>
{
    Disease FindDiseaseById(IEnumerable<Disease> diseases, long id);
}