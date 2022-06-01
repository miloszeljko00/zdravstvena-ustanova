using System.Collections.Generic;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Service.ServiceInterface;

public interface IVaccineService : IService<Vaccine>
{
    Vaccine FindVaccineById(IEnumerable<Vaccine> vaccines, long id);
}