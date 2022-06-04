using zdravstvena_ustanova.Model;
using System.Collections.Generic;
using System.Linq;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class PrescribedMedicineService : IPrescribedMedicineService
    {
        private readonly IPrescribedMedicineRepository _prescribedMedicineRepository;
        private readonly IMedicationRepository _medicationRepository;
        private readonly IMedicationTypeRepository _medicationTypeRepository;
        private readonly IIngredientRepository _ingredientRepository;

        public PrescribedMedicineService(IPrescribedMedicineRepository prescribedMedicineRepository,
            IMedicationRepository medicationRepository, IMedicationTypeRepository medicationTypeRepository,
            IIngredientRepository ingredientRepository)
        {
            _prescribedMedicineRepository = prescribedMedicineRepository;
            _medicationRepository = medicationRepository;
            _medicationTypeRepository = medicationTypeRepository;
            _ingredientRepository = ingredientRepository;
        }

        public IEnumerable<PrescribedMedicine> GetAll()
        {
            var prescribedMedicines = _prescribedMedicineRepository.GetAll();
            var medications = _medicationRepository.GetAll();
            var medicationTypes = _medicationTypeRepository.GetAll();
            var ingredients = _ingredientRepository.GetAll();
            BindMedicationsWithMedicationTypes(medications, medicationTypes);
            BindPrescribedMedicinesWithMedications(prescribedMedicines, medications, ingredients);
            return prescribedMedicines;
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

        private void BindPrescribedMedicinesWithMedications(IEnumerable<PrescribedMedicine> prescribedMedicines, IEnumerable<Medication> medications, IEnumerable<Ingredient> ingredients)
        {
            foreach(PrescribedMedicine pm in prescribedMedicines)
            {
                BindPrescribedMedicineWithMedication(pm, medications, ingredients);
            }
        }

        public PrescribedMedicine Get(long id)
        {
            var medications = _medicationRepository.GetAll();
            var medicationTypes = _medicationTypeRepository.GetAll();
            var prescribedMedicine = _prescribedMedicineRepository.Get(id);
            var ingredients = _ingredientRepository.GetAll(); 
            BindMedicationsWithMedicationTypes(medications, medicationTypes);
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
