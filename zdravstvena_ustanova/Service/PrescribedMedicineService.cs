using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdravstvena_ustanova.Service
{
    public class PrescribedMedicineService
    {
        private readonly PrescribedMedicineRepository _prescribedMedicineRepository;
        private readonly MedicationRepository _medicationRepository;
        private readonly IngredientRepository _ingredientRepository;

        public PrescribedMedicineService(PrescribedMedicineRepository prescribedMedicineRepository, MedicationRepository medicationRepository, IngredientRepository ingredientRepository)
        {
            _prescribedMedicineRepository = prescribedMedicineRepository;
            _medicationRepository = medicationRepository;
            _ingredientRepository = ingredientRepository;
        }

        public IEnumerable<PrescribedMedicine> GetAll()
        {
            var prescribedMedicines = _prescribedMedicineRepository.GetAll();
            var medications = _medicationRepository.GetAll();
            var ingredients = _ingredientRepository.GetAll();
            BindPrescribedMedicinesWithMedications(prescribedMedicines, medications, ingredients);
            return prescribedMedicines;
        }

        private void BindPrescribedMedicinesWithMedications(IEnumerable<PrescribedMedicine> prescribedMedicines, IEnumerable<Medication> medications, IEnumerable<Ingredient> ingredients)
        {
            foreach(PrescribedMedicine pm in prescribedMedicines)
            {
                BindPrescribedMedicineWithMedication(pm, medications, ingredients);
            }
        }

        public PrescribedMedicine GetById(long id)
        {
            var medications = _medicationRepository.GetAll();
            var prescribedMedicine = _prescribedMedicineRepository.Get(id);
            var ingredients = _ingredientRepository.GetAll();
            BindPrescribedMedicineWithMedication(prescribedMedicine, medications, ingredients);
            return prescribedMedicine;
        }

        private void BindPrescribedMedicineWithMedication(PrescribedMedicine prescribedMedicine, IEnumerable<Medication> medications, IEnumerable<Ingredient> ingredients)
        {
            List<Ingredient> ingredientsBinded = new List<Ingredient>();
            foreach(Medication m in medications)
            {
                if(prescribedMedicine.Medication.Id==m.Id)
                {
                    prescribedMedicine.Medication = m;
                    break;
                }
            }
            foreach(Ingredient i1 in prescribedMedicine.Medication.Ingredients)
            {
                foreach(Ingredient i2 in ingredients)
                {
                    if(i1.Id==i2.Id)
                    {
                        ingredientsBinded.Add(i2);
                        break;
                    }
                }
            }
            prescribedMedicine.Medication.Ingredients = ingredientsBinded;
        }

        public PrescribedMedicine Create(PrescribedMedicine prescribedMedicine)
        {
            return _prescribedMedicineRepository.Create(prescribedMedicine);
        }
        public bool Update(PrescribedMedicine prescribedMedicine)
        {
            return _prescribedMedicineRepository.Update(prescribedMedicine);
        }
        public bool Delete(long prescribedMedicineId)
        {
            return _prescribedMedicineRepository.Delete(prescribedMedicineId);
        }
    }
}
