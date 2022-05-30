using zdravstvena_ustanova.Model;
using System;
using zdravstvena_ustanova.Repository;
using System.Collections.Generic;
using System.Linq;
using zdravstvena_ustanova.Repository.RepositoryInterface;

namespace zdravstvena_ustanova.Service
{
    public class MedicationService
    {
        private readonly IMedicationRepository _medicationRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMedicationTypeRepository _medicationTypeRepository;


        public MedicationService(IMedicationRepository medicationRepository, IIngredientRepository ingredientRepository,
            IMedicationTypeRepository medicationTypeRepository)
        {
            _medicationRepository = medicationRepository;
            _ingredientRepository = ingredientRepository;
            _medicationTypeRepository = medicationTypeRepository;
        }

        public IEnumerable<Medication> GetAll()
        {
            var medications = _medicationRepository.GetAll();
            var ingredients = _ingredientRepository.GetAll();
            var medicationTypes = _medicationTypeRepository.GetAll();
            BindMedicationsWithIngredients(medications, ingredients);
            BindMedicationsWithMedicationTypes(medications, medicationTypes);
            return medications;
        }

        private void BindMedicationsWithMedicationTypes(IEnumerable<Medication> medications, IEnumerable<MedicationType> medicationTypes)
        {
            foreach (Medication medication in medications)
            {
                BindMedicationWithMedicationTypes(medication, medicationTypes);
            }
        }

        private void BindMedicationWithMedicationTypes(Medication medication, IEnumerable<MedicationType> medicationTypes)
        {
            medication.MedicationType = FindMedicationTypeById(medicationTypes, medication.MedicationType.Id);
        }
        private MedicationType FindMedicationTypeById(IEnumerable<MedicationType> medicationTypes, long medicationTypeId)
        {
            return medicationTypes.SingleOrDefault(medicationType => medicationType.Id == medicationTypeId);
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
            var medicationTypes = _medicationTypeRepository.GetAll();
            BindMedicationWithIngredients(medication, ingredients);
            BindMedicationWithMedicationTypes(medication, medicationTypes);
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
            var ingredients = _ingredientRepository.GetAll();
            foreach (var medicationIngredient in medication.Ingredients)
            {
                if (medicationIngredient.Id == -1)
                {
                    foreach (var ingredient in ingredients)
                    {
                        if (ingredient.Name == medicationIngredient.Name)
                        {
                            medicationIngredient.Id = ingredient.Id;
                        }
                    }
                }
                
            }
            return _medicationRepository.Update(medication);
        }
        public bool Delete(long medicationId)
        {
            return _medicationRepository.Delete(medicationId);
        }

    }
}