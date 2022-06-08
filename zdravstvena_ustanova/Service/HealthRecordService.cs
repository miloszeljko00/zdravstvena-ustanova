using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Repository;
using System.Collections.Generic;
using System.Linq;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class HealthRecordService : IHealthRecordService
    {
        private readonly IHealthRecordRepository _healthRecordRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IAllergensRepository _allergensRepository;
        private readonly IAnamnesisRepository _anamnesisRepository;
        private readonly ILabAnalysisRecordRepository _labAnalysisRecordRepository;
        private readonly IHospitalizationRecordRepository _hospitalizationRecordRepository;
        private readonly IPrescribedMedicineRepository _prescribedMedicineRepository;
        private readonly IPatientDiseaseRepository _patientDiseaseRepository;
        private readonly IPatientVaccinationRepository _patientVaccinationRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IDiseaseRepository _diseaseRepository;
        private readonly IVaccineRepository _vaccineRepository;
        private readonly ILabAnalysisComponentRepository _labAnalysisComponentRepository;
        private readonly IMedicationRepository _medicationRepository;
        private readonly IMedicationTypeRepository _medicationTypeRepository;
        public HealthRecordService(IHealthRecordRepository healthRecordRepository, IPatientRepository patientRepository,
            IAllergensRepository allergensRepository, IAnamnesisRepository anamnesisRepository,
            ILabAnalysisRecordRepository labAnalysisRecordRepository, IHospitalizationRecordRepository hospitalizationRecordRepository,
            IPrescribedMedicineRepository prescribedMedicineRepository, IPatientDiseaseRepository patientDiseaseRepository,
            IPatientVaccinationRepository patientVaccinationRepository, IRoomRepository roomRepository, IAccountRepository accountRepository,
            IIngredientRepository ingredientRepository, IDiseaseRepository diseaseRepository, IVaccineRepository vaccineRepository,
            ILabAnalysisComponentRepository labAnalysisComponent, IMedicationRepository medicationRepository, IMedicationTypeRepository medicationTypeRepository)
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

        public HealthRecordService(PatientRepository patientRepository, AnamnesisRepository anamnesisRepository, HealthRecordRepository healthRecordRepository, IngredientRepository ingredientRepository, AllergensRepository allergensRepository)
        {
            _anamnesisRepository = anamnesisRepository;
            _patientRepository = patientRepository;
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
            var ingredients = _ingredientRepository.GetAll();
            BindPatientsWithHealthRecords(patients, healthRecords);
            BindAllergensWithHealthRecords(allergens, healthRecords, ingredients);
            BindAnamnesissWithHealthRecords(anamnesiss, healthRecords);
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

        public IEnumerable<Anamnesis> GetAnamnesisForPatient(long patientId)
        {
            var healthRecords = GetAll();
            List<Anamnesis> anamnesis = new List<Anamnesis>();
            foreach (HealthRecord hr in healthRecords)
                if (hr.Patient.Id == patientId)
                {
                    anamnesis = hr.Anamnesis;
                    break;
                }
            return anamnesis;
        }


        public HealthRecord Get(long id)
        {
            var healthRecord = _healthRecordRepository.Get(id);
            var allergens = _allergensRepository.GetAll();
            var anamnesiss = _anamnesisRepository.GetAll();
            var labAnalysisRecords = _labAnalysisRecordRepository.GetAll();
            var hospitalizationRecords = _hospitalizationRecordRepository.GetAll();
            var prescribedMedicines = _prescribedMedicineRepository.GetAll();
            var patientDiseases = _patientDiseaseRepository.GetAll();
            var patientVaccinations = _patientVaccinationRepository.GetAll();
            var rooms = _roomRepository.GetAll();
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

        private void BindPatientsWithHealthRecords(IEnumerable<Patient> patients, IEnumerable<HealthRecord> healthRecords)
        {
            foreach (HealthRecord hr in healthRecords)
            {
                BindPatientsWithHealthRecord(patients, hr);
            }
        }
        private void BindPatientsWithHealthRecord(IEnumerable<Patient> patients, HealthRecord healthRecord)
        {
            foreach (Patient p in patients)
            {
                if (p.Id == healthRecord.Patient.Id)
                {
                    healthRecord.Patient = p;
                    break;
                }
            }
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
                        var allergen = BindAllergensWithIngredients(a2, ingredients);
                        allergensBinded.Add(allergen);
                        break;
                    }
                }
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
                        var LabAnalysisRecord = BindLabAnlysisRecordWithComponents(lar2, labAnalysisComponents);
                        labAnalysisRecordsBinded.Add(LabAnalysisRecord);
                        break;
                    }
                }
            }
            healthRecord.LabAnalysisRecord = labAnalysisRecordsBinded;
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
                        var hr3 = BindHospitalizationRecordWithRoom(hr2, rooms);
                        hospitalizationRecordsBinded.Add(hr3);
                        break;
                    }
                }
            }
            healthRecord.HospitalizationRecord = hospitalizationRecordsBinded;
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
                        var pm3 = BindPrescribedMedicineWithMedication(pm2, ingredients, medications);
                        prescribedMedicinesBinded.Add(pm3);
                        break;
                    }
                }
            }

            healthRecord.PrescribedMedicine = new List<PrescribedMedicine>(prescribedMedicinesBinded);
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
                        var patientDisease = BindPatientDiseaseWithDisease(pd2, diseases);
                        patientDiseasesBinded.Add(patientDisease);
                        break;
                    }
                }
            }

            healthRecord.PatientDisease = new List<PatientDisease>(patientDiseasesBinded);
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
                        var patientVaccination = BindPatientVacinationWIthVaccine(pv2, vaccines);
                        patientVaccinationsBinded.Add(patientVaccination);
                        break;
                    }
                }
            }
            healthRecord.PatientVaccination = new List<PatientVaccination>(patientVaccinationsBinded);
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
                if (healthRecord.Patient.Id == patientId)
                {
                    BindAllergensWithHealthRecord(allergens, healthRecord, ingredients);
                    return healthRecord;
                }
            }
            return null;
        }

        private Allergens BindAllergensWithIngredients(Allergens allergen, IEnumerable<Ingredient> ingredients)
        {
            List<Ingredient> ingredientsBinded = new List<Ingredient>();
            foreach (Ingredient i1 in allergen.Ingredients)
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
            allergen.Ingredients = new List<Ingredient>(ingredientsBinded);
            return allergen;
        }

        private LabAnalysisRecord BindLabAnlysisRecordWithComponents(LabAnalysisRecord lar, IEnumerable<LabAnalysisComponent> labAnalysisComponents)
        {
            List<LabAnalysisComponent> labAnalysisComponentsBinded = new List<LabAnalysisComponent>();
            foreach (LabAnalysisComponent lac1 in lar.LabAnalysisComponent)
            {
                foreach (LabAnalysisComponent lac2 in labAnalysisComponents)
                {
                    if (lac1.Id == lac2.Id)
                    {
                        labAnalysisComponentsBinded.Add(lac2);
                    }
                }
            }
            lar.LabAnalysisComponent = new List<LabAnalysisComponent>(labAnalysisComponentsBinded);
            return lar;
        }

        private HospitalizationRecord BindHospitalizationRecordWithRoom(HospitalizationRecord hr, IEnumerable<Room> rooms)
        {
            foreach (Room r in rooms)
            {
                if (hr.Room.Id == r.Id)
                {
                    hr.Room = r;
                    break;
                }
            }
            return hr;
        }
        private Medication BindMedicationWithIngredients(Medication medication, IEnumerable<Ingredient> ingredients)
        {
            List<Ingredient> ingredientsBinded = new List<Ingredient>();
            foreach (Ingredient i1 in medication.Ingredients)
            {
                foreach (Ingredient i2 in ingredients)
                {
                    if (i1.Id == i2.Id)
                    {
                        ingredientsBinded.Add(i2);
                    }
                }
            }
            medication.Ingredients = new List<Ingredient>(ingredientsBinded);
            return medication;
        }

        private PrescribedMedicine BindPrescribedMedicineWithMedication(PrescribedMedicine prescribedMedicine, IEnumerable<Ingredient> ingredients, IEnumerable<Medication> medications)
        {
            foreach (Medication m in medications)
            {
                if (prescribedMedicine.Medication.Id == m.Id)
                {
                    var medication = BindMedicationWithIngredients(m, ingredients);
                    prescribedMedicine.Medication = medication;
                    break;
                }
            }
            return prescribedMedicine;
        }

        private PatientDisease BindPatientDiseaseWithDisease(PatientDisease pd, IEnumerable<Disease> diseases)
        {
            foreach (Disease d1 in diseases)
            {
                if (pd.Disease.Id == d1.Id)
                {
                    pd.Disease = d1;
                    break;
                }
            }
            return pd;
        }

        private PatientVaccination BindPatientVacinationWIthVaccine(PatientVaccination pv, IEnumerable<Vaccine> vaccines)
        {
            foreach (Vaccine v1 in vaccines)
            {
                if (pv.Vaccine.Id == v1.Id)
                {
                    pv.Vaccine = v1;
                    break;
                }
            }
            return pv;
        }
    }
}
