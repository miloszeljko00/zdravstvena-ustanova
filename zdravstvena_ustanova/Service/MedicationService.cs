using Model;
using System;
using Repository;
using System.Collections.Generic;
using System.Linq;
using zdravstvena_ustanova.Repository;

namespace Service
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
            return medications;
        }
        public Medication GetById(long id)
        {
            var medication = _medicationRepository.Get(id);
            return medication;
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