using System.Collections.Generic;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Service.ServiceInterface;

public interface IMedicationTypeService : IService<MedicationType>
{
    MedicationType FindIngredientById(IEnumerable<MedicationType> medicationTypes, long id);
}