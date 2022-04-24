using Model;
using System;
using Repository;
using System.Collections.Generic;
using System.Linq;
using zdravstvena_ustanova.Repository;

namespace Service
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
            return allergens;
        }
        public Allergens GetById(long id)
        {
            var allergen = _allergensRepository.Get(id);
            return allergen;
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