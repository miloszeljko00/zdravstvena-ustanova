using zdravstvena_ustanova.Model;
using System;
using zdravstvena_ustanova.Repository;
using System.Collections.Generic;
using System.Linq;

namespace zdravstvena_ustanova.Service
{
    public class MedicationService
    {
        private readonly MedicationRepository _medicationRepository;
        private readonly IngredientRepository _ingredientRepository;


        public MedicationService(MedicationRepository medicationRepository, IngredientRepository ingredientRepository)
        {
            _medicationRepository = medicationRepository;
            _ingredientRepository = ingredientRepository;
        }

        public IEnumerable<Medication> GetAll()
        {
            var medications = _medicationRepository.GetAll();
            var ingredients = _ingredientRepository.GetAll();
            BindMedicationsWithIngredients(medications, ingredients);
            return medications;
        }

        private void BindMedicationsWithIngredients(IEnumerable<Medication> medications, IEnumerable<Ingredient> ingredients)
        {
            foreach (Medication m in medications)
            {
                BindMedicationWithIngredients(m, ingredients);
            }
        }

        public Medication GetById(long id)
        {
            var medication = _medicationRepository.Get(id);
            var ingredients = _ingredientRepository.GetAll();
            BindMedicationWithIngredients(medication, ingredients);
            return medication;
        }

        private void BindMedicationWithIngredients(Medication medication, IEnumerable<Ingredient> ingredients)
        {
            List<Ingredient> ingredientsBinded = new List<Ingredient>();
            foreach (Ingredient i1 in medication.Ingredients)
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
            medication.Ingredients = ingredientsBinded;
        }

        public Medication Create(Medication medication)
        {
            return _medicationRepository.Create(medication);
        }
        public bool Update(Medication medication)
        {
            return _medicationRepository.Update(medication);
        }
        public bool Delete(long medicationId)
        {
            return _medicationRepository.Delete(medicationId);
        }

    }
}