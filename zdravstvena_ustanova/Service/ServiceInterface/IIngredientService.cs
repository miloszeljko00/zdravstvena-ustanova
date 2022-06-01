using System.Collections.Generic;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Service.ServiceInterface;

public interface IIngredientService : IService<Ingredient>
{
    Ingredient FindIngredientById(IEnumerable<Ingredient> ingredients, long id);
    bool CheckIfItsAlreadyContained(IEnumerable<Ingredient> ingredients, Ingredient ingredient);
    void CreateIfNotSavedWithSameName(List<Ingredient> ingredients);
}