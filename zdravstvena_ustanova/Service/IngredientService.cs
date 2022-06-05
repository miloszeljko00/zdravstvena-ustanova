using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class IngredientService :IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
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

        public bool CheckIfItsAlreadyContained(IEnumerable<Ingredient> ingredients, Ingredient ingredient)
        {
            bool isContained = false;
            foreach(var i in ingredients)
            {
                if(i.Name == ingredient.Name)
                {
                    isContained = true;
                    break;
                }
            }
            return isContained;
        }

        public void CreateIfNotSavedWithSameName(List<Ingredient> ingredients)
        {
            var savedIngredients = GetAll();

            foreach(var ingredient in ingredients)
            {
                if (!CheckIfItsAlreadyContained(savedIngredients, ingredient))
                {
                    Create(ingredient);
                }
            }
        }

        public Ingredient Get(long id)
        {
            return _ingredientRepository.Get(id);
        }
    }
}