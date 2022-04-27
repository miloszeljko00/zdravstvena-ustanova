using Model;
using System;
using System.Collections.Generic;
using Repository;
using System.Linq;

namespace Service
{
    public class IngredientService
    {
        private readonly IngredientRepository _ingredientRepository;

        public IngredientService(IngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public IEnumerable<Ingredient> GetAll()
        {
            return _ingredientRepository.GetAll();
        }

        public Ingredient FindIngredientById(IEnumerable<Ingredient> ingredients, long id)
        {
            return ingredients.SingleOrDefault(ingredient => ingredient.Id == id);
        }

        public Ingredient Create(Ingredient ingredient)
        {
            return _ingredientRepository.Create(ingredient);
        }
        public bool Update(Ingredient ingredient)
        {
            return _ingredientRepository.Update(ingredient);
        }
        public bool Delete(long ingredientId)
        {
            return _ingredientRepository.Delete(ingredientId);
        }
    }
}