using zdravstvena_ustanova.Model;
using System;
using zdravstvena_ustanova.Repository;
using System.Collections.Generic;
using System.Linq;

namespace zdravstvena_ustanova.Service
{
    public class AllergensService
    {
        private readonly AllergensRepository _allergensRepository;
        private readonly IngredientRepository _ingredientRepository;


        public AllergensService(AllergensRepository allergensRepository, IngredientRepository ingredientRepository)
        {
            _allergensRepository = allergensRepository;
            _ingredientRepository = ingredientRepository;
        }

        public IEnumerable<Allergens> GetAll()
        {
            var allergens = _allergensRepository.GetAll();
            var ingredients = _ingredientRepository.GetAll();
            BindAllergensWithIngredients(allergens, ingredients);
            return allergens;
        }

        private void BindAllergensWithIngredients(IEnumerable<Allergens> allergens, IEnumerable<Ingredient> ingredients)
        {
            foreach (Allergens a in allergens)
            {
                BindAllergenWithIngredients(a, ingredients);
            }
        }

        public Allergens GetById(long id)
        {
            var allergen = _allergensRepository.Get(id);
            var ingredients = _ingredientRepository.GetAll();
            BindAllergenWithIngredients(allergen, ingredients);
            return allergen;
        }

        private void BindAllergenWithIngredients(Allergens allergen, IEnumerable<Ingredient> ingredients)
        {
            List<Ingredient> ingredientsBinded = new List<Ingredient>();
            foreach (Ingredient i1 in allergen.Ingredients)
            {
                foreach (Ingredient i2 in ingredients)
                {
                    if (i2.Id == i1.Id)
                    {
                        ingredientsBinded.Add(i2);
                        break;
                    }
                }
            }
            allergen.Ingredients = ingredientsBinded;
        }

        public Allergens Create(Allergens allergen)
        {
            return _allergensRepository.Create(allergen);
        }
        public bool Update(Allergens allergen)
        {
            return _allergensRepository.Update(allergen);
        }
        public bool Delete(long allergenId)
        {
            return _allergensRepository.Delete(allergenId);
        }

    }
}