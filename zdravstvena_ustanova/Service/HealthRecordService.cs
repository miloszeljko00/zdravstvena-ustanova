using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdravstvena_ustanova.Service
{
    public class HealthRecordService
    {
        private readonly HealthRecordRepository _healthRecordRepository;
        private readonly PatientRepository _patientRepository;
        private readonly AllergensRepository _allergensRepository;
        private readonly AnamnesisRepository _anamnesisRepository;
        private readonly LabAnalysisRecordRepository _labAnalysisRecordRepository;
        private readonly HospitalizationRecordRepository _hospitalizationRecordRepository;
        private readonly PrescribedMedicineRepository _prescribedMedicineRepository;
        private readonly PatientDiseaseRepository _patientDiseaseRepository;
        private readonly PatientVaccinationRepository _patientVaccinationRepository;
        private readonly RoomRepository _roomRepository;
        private readonly AccountRepository _accountRepository;
        private readonly IngredientRepository _ingredientRepository;
        private readonly DiseaseRepository _diseaseRepository;
        private readonly VaccineRepository _vaccineRepository;
        private readonly LabAnalysisComponentRepository _labAnalysisComponentRepository;
        private readonly MedicationRepository _medicationRepository;
        private readonly MedicationTypeRepository _medicationTypeRepository;
        public HealthRecordService(HealthRecordRepository healthRecordRepository, PatientRepository patientRepository, AllergensRepository allergensRepository,
            AnamnesisRepository anamnesisRepository, LabAnalysisRecordRepository labAnalysisRecordRepository, HospitalizationRecordRepository hospitalizationRecordRepository,
            PrescribedMedicineRepository prescribedMedicineRepository, PatientDiseaseRepository patientDiseaseRepository,
            PatientVaccinationRepository patientVaccinationRepository, RoomRepository roomRepository, AccountRepository accountRepository,
            IngredientRepository ingredientRepository, DiseaseRepository diseaseRepository, VaccineRepository vaccineRepository,
            LabAnalysisComponentRepository labAnalysisComponent, MedicationRepository medicationRepository, MedicationTypeRepository medicationTypeRepository)
        {
            _healthRecordRepository = healthRecordRepository;
            _patientRepository = patientRepository;
            _allergensRepository = allergensRepository;
            _anamnesisRepository = anamnesisRepository;
            _labAnalysisRecordRepository = labAnalysisRecordRepository;
            _hospitalizationRecordRepository = hospitalizationRecordRepository;
            _prescribedMedicineRepository = prescribedMedicineRepository;
            _patientDiseaseRepository = patientDiseaseRepository;
            _patientVaccinationRepository = patientVaccinationRepository;
            _roomRepository = roomRepository;
            _accountRepository = accountRepository;
            _ingredientRepository = ingredientRepository;
            _diseaseRepository = diseaseRepository;
            _vaccineRepository = vaccineRepository;
            _labAnalysisComponentRepository = labAnalysisComponent;
            _medicationRepository = medicationRepository;
            _medicationTypeRepository = medicationTypeRepository;
        }

        public HealthRecordService(HealthRecordRepository healthRecordRepository, IngredientRepository ingredientRepository, AllergensRepository allergensRepository)
        {
            _healthRecordRepository = healthRecordRepository;
            _ingredientRepository = ingredientRepository;
            _allergensRepository = allergensRepository;
        }

        public IEnumerable<HealthRecord> GetAll()
        {
            var healthRecords = _healthRecordRepository.GetAll();
            var patients = _patientRepository.GetAll();
            var allergens = _allergensRepository.GetAll();
            var anamnesiss = _anamnesisRepository.GetAll();
            var labAnalysisRecords = _labAnalysisRecordRepository.GetAll();
            var hospitalizationRecords = _hospitalizationRecordRepository.GetAll();
            var prescribedMedicines = _prescribedMedicineRepository.GetAll();
            var patientDiseases = _patientDiseaseRepository.GetAll();
            var patientVaccinations = _patientVaccinationRepository.GetAll();
            var rooms = _roomRepository.GetAll();
            var accounts = _accountRepository.GetAll();
            var ingredients = _ingredientRepository.GetAll();
            var labAnalysisComponents = _labAnalysisComponentRepository.GetAll();
            var medications = _medicationRepository.GetAll(); 
            var medicationTypes = _medicationTypeRepository.GetAll();
            var diseases = _diseaseRepository.GetAll();
            var vaccines = _vaccineRepository.GetAll();
            BindMedicationsWithMedicationTypes(medications, medicationTypes);
            BindAllergensWithHealthRecords(allergens, healthRecords, ingredients);
            BindAnamnesissWithHealthRecords(anamnesiss, healthRecords);
            BindLabAnalysisRecordsWithHealthRecords(labAnalysisRecords, healthRecords, labAnalysisComponents);
            BindHospitalizationRecordsWithHealthRecords(hospitalizationRecords, healthRecords, rooms);
            BindPrescribedMedicinesWithHealthRecords(prescribedMedicines, healthRecords, medications, ingredients);
            BindPatientDiseasesWithHealthRecords(patientDiseases, healthRecords, diseases);
            BindPatientVaccinationsWithHealthRecords(patientVaccinations, healthRecords, vaccines);
            return healthRecords;
        }

        public IEnumerable<HealthRecord> GetAllOnlyWithAllergens()
        {
            var healthRecords = _healthRecordRepository.GetAll();
            var allergens = _allergensRepository.GetAll();
            var ingredients = _ingredientRepository.GetAll();
            BindAllergensWithHealthRecords(allergens, healthRecords, ingredients);
            return healthRecords;
        }


        public HealthRecord GetById(long id)
        {
            var healthRecord = _healthRecordRepository.Get(id);
            var patients = _patientRepository.GetAll();
            var allergens = _allergensRepository.GetAll();
            var anamnesiss = _anamnesisRepository.GetAll();
            var labAnalysisRecords = _labAnalysisRecordRepository.GetAll();
            var hospitalizationRecords = _hospitalizationRecordRepository.GetAll();
            var prescribedMedicines = _prescribedMedicineRepository.GetAll();
            var patientDiseases = _patientDiseaseRepository.GetAll();
            var patientVaccinations = _patientVaccinationRepository.GetAll();
            var rooms = _roomRepository.GetAll();
            var accounts = _accountRepository.GetAll();
            var ingredients = _ingredientRepository.GetAll();
            var labAnalysisComponents = _labAnalysisComponentRepository.GetAll();
            var medications = _medicationRepository.GetAll();
            var medicationTypes = _medicationTypeRepository.GetAll();
            var diseases = _diseaseRepository.GetAll();
            var vaccines = _vaccineRepository.GetAll();
            BindMedicationsWithMedicationTypes(medications, medicationTypes);
            BindAllergensWithHealthRecord(allergens, healthRecord, ingredients);
            BindAnamnesissWithHealthRecord(anamnesiss, healthRecord);
            BindLabAnalysisRecordsWithHealthRecord(labAnalysisRecords, healthRecord, labAnalysisComponents);
            BindHospitalizationRecordsWithHealthRecord(hospitalizationRecords, healthRecord, rooms);
            BindPrescribedMedicinesWithHealthRecord(prescribedMedicines, healthRecord, medications, ingredients);
            BindPatientDiseasesWithHealthRecord(patientDiseases, healthRecord, diseases);
            BindPatientVaccinationsWithHealthRecord(patientVaccinations, healthRecord, vaccines);
            return healthRecord;
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
        private void BindAllergensWithHealthRecord(IEnumerable<Allergens> allergens, HealthRecord healthRecord, IEnumerable<Ingredient> ingredients)
        {
            List<Allergens> allergensBinded = new List<Allergens>();
            foreach (Allergens a1 in healthRecord.Allergens)
            {
                foreach (Allergens a2 in allergens)
                {
                    if (a2.Id == a1.Id)
                    {
                        allergensBinded.Add(a2);
                        break;
                    }
                }
            }
            List<Ingredient> ingredientsBinded = new List<Ingredient>();
            foreach (Allergens a3 in allergensBinded)
            {
                foreach (Ingredient i1 in a3.Ingredients)
                {
                    foreach (Ingredient i2 in ingredients)
                    {
                        if (i1.Id == i2.Id)
                        {
                            ingredientsBinded.Add(i2);
                            break;
                        }
                    }
                }
                a3.Ingredients =new List<Ingredient>(ingredientsBinded);
                ingredientsBinded.Clear();
            }
            healthRecord.Allergens = allergensBinded;
        }
        private void BindAllergensWithHealthRecords(IEnumerable<Allergens> allergens, IEnumerable<HealthRecord> healthRecords, IEnumerable<Ingredient> ingredients)
        {
            foreach (HealthRecord hr in healthRecords)
            {
                BindAllergensWithHealthRecord(allergens, hr, ingredients);
            }
        }

        private void BindAnamnesissWithHealthRecord(IEnumerable<Anamnesis> anamnesiss, HealthRecord healthRecord)
        {
            List<Anamnesis> anamnesissBinded = new List<Anamnesis>();
            foreach (Anamnesis a1 in healthRecord.Anamnesis)
            {
                foreach (Anamnesis a2 in anamnesiss)
                {
                    if (a2.Id == a1.Id)
                    {
                        anamnesissBinded.Add(a2);
                        break;
                    }
                }
            }
            healthRecord.Anamnesis = anamnesissBinded;
        }
        private void BindAnamnesissWithHealthRecords(IEnumerable<Anamnesis> anamnesiss, IEnumerable<HealthRecord> healthRecords)
        {
            foreach (HealthRecord hr in healthRecords)
            {
                BindAnamnesissWithHealthRecord(anamnesiss, hr);
            }
        }
        private void BindLabAnalysisRecordsWithHealthRecord(IEnumerable<LabAnalysisRecord> labAnalysisRecords, HealthRecord healthRecord, IEnumerable<LabAnalysisComponent> labAnalysisComponents)
        {
            List<LabAnalysisRecord> labAnalysisRecordsBinded = new List<LabAnalysisRecord>();
            foreach (LabAnalysisRecord lar1 in healthRecord.LabAnalysisRecord)
            {
                foreach (LabAnalysisRecord lar2 in labAnalysisRecords)
                {
                    if (lar2.Id == lar1.Id)
                    {
                        labAnalysisRecordsBinded.Add(lar2);
                        break;
                    }
                }
            }
            List<LabAnalysisComponent> labAnalysisComponentsBinded = new List<LabAnalysisComponent>();
            foreach(LabAnalysisRecord lar3 in labAnalysisRecordsBinded)
            {
                foreach(LabAnalysisComponent lac1 in lar3.LabAnalysisComponent)
                {
                    foreach(LabAnalysisComponent lac2 in labAnalysisComponents)
                    {
                        if(lac1.Id==lac2.Id)
                        {
                            labAnalysisComponentsBinded.Add(lac2);
                        }
                    }
                }
                lar3.LabAnalysisComponent=new List<LabAnalysisComponent>(labAnalysisComponentsBinded);
                labAnalysisComponentsBinded.Clear();
            }
            healthRecord.LabAnalysisRecord = labAnalysisRecordsBinded;
        }
        private void BindLabAnalysisRecordsWithHealthRecords(IEnumerable<LabAnalysisRecord> labAnalysisRecords, IEnumerable<HealthRecord> healthRecords, IEnumerable<LabAnalysisComponent> labAnalysisComponents)
        {
            foreach (HealthRecord hr in healthRecords)
            {
                BindLabAnalysisRecordsWithHealthRecord(labAnalysisRecords, hr, labAnalysisComponents);
            }
        }

        private void BindHospitalizationRecordsWithHealthRecord(IEnumerable<HospitalizationRecord> hospitalizationRecords, HealthRecord healthRecord, IEnumerable<Room> rooms)
        {
            List<HospitalizationRecord> hospitalizationRecordsBinded = new List<HospitalizationRecord>();
            foreach (HospitalizationRecord hr1 in healthRecord.HospitalizationRecord)
            {
                foreach (HospitalizationRecord hr2 in hospitalizationRecords)
                {
                    if (hr2.Id == hr1.Id)
                    {
                        hospitalizationRecordsBinded.Add(hr2);
                        break;
                    }
                }
            }
            foreach(HospitalizationRecord hr3 in hospitalizationRecordsBinded)
            {
                foreach(Room r in rooms)
                {
                    if(hr3.Room.Id==r.Id)
                    {
                        hr3.Room = r;
                        break;
                    }
                }
            }

            healthRecord.HospitalizationRecord = hospitalizationRecordsBinded;
        }
        private void BindHospitalizationRecordsWithHealthRecords(IEnumerable<HospitalizationRecord> hospitalizationRecords, IEnumerable<HealthRecord> healthRecords, IEnumerable<Room> rooms)
        {
            foreach (HealthRecord hr in healthRecords)
            {
                BindHospitalizationRecordsWithHealthRecord(hospitalizationRecords, hr, rooms);
            }
        }
        private void BindPrescribedMedicinesWithHealthRecord(IEnumerable<PrescribedMedicine> prescribedMedicines, HealthRecord healthRecord,
            IEnumerable<Medication> medications, IEnumerable<Ingredient> ingredients)
        {
            List<PrescribedMedicine> prescribedMedicinesBinded = new List<PrescribedMedicine>();
            foreach (PrescribedMedicine pm1 in healthRecord.PrescribedMedicine)
            {
                foreach (PrescribedMedicine pm2 in prescribedMedicines)
                {
                    if (pm2.Id == pm1.Id)
                    {
                        prescribedMedicinesBinded.Add(pm2);
                        break;
                    }
                }
            }
            foreach(PrescribedMedicine pm3 in prescribedMedicinesBinded)
            {
                foreach(Medication m2 in medications)
                {
                    if(pm3.Medication.Id==m2.Id)
                    {
                        pm3.Medication = m2;
                        break;
                    }
                }
            }
            List<Ingredient> ingredientsBinded = new List<Ingredient>();
            foreach(PrescribedMedicine pm4 in prescribedMedicinesBinded)
            {
                foreach(Ingredient i1 in pm4.Medication.Ingredients)
                {
                    foreach(Ingredient i2 in ingredients)
                    {
                        if(i1.Id==i2.Id)
                        {
                            ingredientsBinded.Add(i2);
                        }
                    }
                }
                pm4.Medication.Ingredients =new List<Ingredient>(ingredientsBinded);
                ingredientsBinded.Clear();
            }

            healthRecord.PrescribedMedicine = prescribedMedicinesBinded;
        }
        private void BindPrescribedMedicinesWithHealthRecords(IEnumerable<PrescribedMedicine> prescribedMedicines, IEnumerable<HealthRecord> healthRecords,
            IEnumerable<Medication> medications, IEnumerable<Ingredient> ingredients)
        {
            foreach (HealthRecord hr in healthRecords)
            {
                BindPrescribedMedicinesWithHealthRecord(prescribedMedicines, hr, medications, ingredients);
            }
        }
        private void BindPatientDiseasesWithHealthRecord(IEnumerable<PatientDisease> patientDiseases, HealthRecord healthRecord, IEnumerable<Disease> diseases)
        {
            List<PatientDisease> patientDiseasesBinded = new List<PatientDisease>();
            foreach (PatientDisease pd1 in healthRecord.PatientDisease)
            {
                foreach (PatientDisease pd2 in patientDiseases)
                {
                    if (pd2.Id == pd1.Id)
                    {
                        patientDiseasesBinded.Add(pd2);
                        break;
                    }
                }
            }
            foreach(PatientDisease pd3 in patientDiseasesBinded)
            {
                foreach(Disease d1 in diseases)
                {
                    if(pd3.Disease.Id==d1.Id)
                    {
                        pd3.Disease = d1;
                        break;
                    }
                }
            }

            healthRecord.PatientDisease = patientDiseasesBinded;
        }
        private void BindPatientDiseasesWithHealthRecords(IEnumerable<PatientDisease> patientDiseases, IEnumerable<HealthRecord> healthRecords, IEnumerable<Disease> diseases)
        {
            foreach (HealthRecord hr in healthRecords)
            {
                BindPatientDiseasesWithHealthRecord(patientDiseases, hr, diseases);
            }
        }

        private void BindPatientVaccinationsWithHealthRecord(IEnumerable<PatientVaccination> patientVaccinations, HealthRecord healthRecord, IEnumerable<Vaccine> vaccines)
        {
            List<PatientVaccination> patientVaccinationsBinded = new List<PatientVaccination>();
            foreach (PatientVaccination pv1 in healthRecord.PatientVaccination)
            {
                foreach (PatientVaccination pv2 in patientVaccinations)
                {
                    if (pv2.Id == pv1.Id)
                    {
                        patientVaccinationsBinded.Add(pv2);
                        break;
                    }
                }
            }
            foreach(PatientVaccination pv3 in patientVaccinationsBinded)
            {
                foreach(Vaccine v1 in vaccines)
                {
                    if(pv3.Vaccine.Id==v1.Id)
                    {
                        pv3.Vaccine = v1;
                        break;
                    }
                }
            }
            healthRecord.PatientVaccination = patientVaccinationsBinded;
        }
        private void BindPatientVaccinationsWithHealthRecords(IEnumerable<PatientVaccination> patientVaccinations, IEnumerable<HealthRecord> healthRecords, IEnumerable<Vaccine> vaccines)
        {
            foreach (HealthRecord hr in healthRecords)
            {
                BindPatientVaccinationsWithHealthRecord(patientVaccinations, hr, vaccines);
            }
        }

        public HealthRecord Create(HealthRecord healthRecord)
        {
            return _healthRecordRepository.Create(healthRecord);
        }
        public bool Update(HealthRecord healthRecord)
        {
            return _healthRecordRepository.Update(healthRecord);
        }
        public bool Delete(long id)
        {
            return _healthRecordRepository.Delete(id);
        }

        public HealthRecord FindHealthRecordByPatient(long patientId)
        {
            var healthRecords = GetAllOnlyWithAllergens();
            var allergens = _allergensRepository.GetAll();
            var ingredients = _ingredientRepository.GetAll();
            foreach (HealthRecord healthRecord in healthRecords)
            {
                if(healthRecord.Patient.Id == patientId)
                {
                    BindAllergensWithHealthRecord(allergens, healthRecord, ingredients);
                    return healthRecord;
                }
            }
            return null;
        }

        //private void BindHealthRecordWithAllergens(HealthRecord healthRecord)
        //{
        //    var allergens = _allergensRepository.GetAll();
        //    List<Allergens> bindedAllergens = new List<Allergens>();
        //    foreach(Allergens a1 in allergens)
        //    {
        //        foreach(Allergens a2 in healthRecord.Allergens)
        //        {
        //            if (a1.Id == a2.Id)
        //            {
        //                bindedAllergens.Add(a1);
        //            }
        //        }
        //    }
        //    healthRecord.Allergens.Clear();
        //    foreach(Allergens a3 in bindedAllergens)
        //    {
        //        healthRecord.Allergens.Add(a3);
        //    }
        //}

        //private void BindHealthRecordWithPatient(HealthRecord healthRecord, long patientId)
        //{
        //    var patients = _patientRepository.GetAll();
        //    foreach (Patient p in patients)
        //    {
        //        if (p.Id == patientId)
        //        {
        //            healthRecord.Patient = p;
        //        }
        //    }
        //}
    }
}
