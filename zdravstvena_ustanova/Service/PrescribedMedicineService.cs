using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Repository;

namespace zdravstvena_ustanova.Service
{
    public class PrescribedMedicineService
    {
        private readonly PrescribedMedicineRepository _prescribedMedicineRepository;
        private readonly MedicationRepository _medicationRepository;

        public PrescribedMedicineService(PrescribedMedicineRepository prescribedMedicineRepository, MedicationRepository medicationRepository)
        {
            _prescribedMedicineRepository = prescribedMedicineRepository;
            _medicationRepository = medicationRepository;
        }

        public IEnumerable<PrescribedMedicine> GetAll()
        {
            var prescribedMedicines = _prescribedMedicineRepository.GetAll();
            var medications = _medicationRepository.GetAll();
            BindPrescribedMedicinesWithMedications(prescribedMedicines, medications);
            return prescribedMedicines;
        }

        private void BindPrescribedMedicinesWithMedications(IEnumerable<PrescribedMedicine> prescribedMedicines, IEnumerable<Medication> medications)
        {
            foreach(PrescribedMedicine pm in prescribedMedicines)
            {
                BindPrescribedMedicineWithMedication(pm, medications);
            }
        }

        public PrescribedMedicine GetById(long id)
        {
            var medications = _medicationRepository.GetAll();
            var prescribedMedicine = _prescribedMedicineRepository.Get(id);
            BindPrescribedMedicineWithMedication(prescribedMedicine, medications);
            return prescribedMedicine;
        }

        private void BindPrescribedMedicineWithMedication(PrescribedMedicine prescribedMedicine, IEnumerable<Medication> medications)
        {
            foreach(Medication m in medications)
            {
                if(prescribedMedicine.Medication.Id==m.Id)
                {
                    prescribedMedicine.Medication = m;
                    break;
                }
            }
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
