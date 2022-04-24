using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using Model;

namespace Controller
{
    public class IngredientController
    {
        private readonly IngredientService _ingredientService;

        public IngredientController(IngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public IEnumerable<Ingredient> GetAll()
        {
            return _ingredientService.GetAll();
        }
        public Ingredient GetById(IEnumerable<Ingredient> ingredients, long id)
        {
            return _ingredientService.FindIngredientById(ingredients, id);
        }
        public Ingredient Create(Ingredient ingredient)
        {
            return _ingredientService.Create(ingredient);
        }
        public bool Update(Ingredient ingredient)
        {
            return _ingredientService.Update(ingredient);
        }
        public bool Delete(long ingredientId)
        {
            return _ingredientService.Delete(ingredientId);
        }
    }
}
