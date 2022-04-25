using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Repository;

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

        public HealthRecordService(HealthRecordRepository healthRecordRepository, PatientRepository patientRepository,
            AllergensRepository allergensRepository, AnamnesisRepository anamnesisRepository, LabAnalysisRecordRepository labAnalysisRecordRepository,
            HospitalizationRecordRepository hospitalizationRecordRepository, PrescribedMedicineRepository prescribedMedicineRepository,
            PatientDiseaseRepository patientDiseaseRepository, PatientVaccinationRepository patientVaccinationRepository)
        {
            _healthRecordRepository = healthRecordRepository;
            _patientRepository = patientRepository;
            _allergensRepository = allergensRepository;
            _anamnesisRepository = anamnesisRepository;
            _labAnalysisRecordRepository= labAnalysisRecordRepository;
            _hospitalizationRecordRepository = hospitalizationRecordRepository;
            _prescribedMedicineRepository = prescribedMedicineRepository;
            _patientDiseaseRepository = patientDiseaseRepository;
            _patientVaccinationRepository = patientVaccinationRepository;
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
            BindAllergensWithHealthRecords(allergens, healthRecords);
            BindAnamnesissWithHealthRecords(anamnesiss, healthRecords);
            BindLabAnalysisRecordsWithHealthRecords(labAnalysisRecords, healthRecords);
            BindHospitalizationRecordsWithHealthRecords(hospitalizationRecords, healthRecords);
            BindPrescribedMedicinesWithHealthRecords(prescribedMedicines, healthRecords);
            BindPatientDiseasesWithHealthRecords(patientDiseases, healthRecords);
            BindPatientVaccinationsWithHealthRecords(patientVaccinations, healthRecords);
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
            BindAllergensWithHealthRecord(allergens, healthRecord);
            BindAnamnesissWithHealthRecord(anamnesiss, healthRecord);
            BindLabAnalysisRecordsWithHealthRecord(labAnalysisRecords, healthRecord);
            BindHospitalizationRecordsWithHealthRecord(hospitalizationRecords, healthRecord);
            BindPrescribedMedicinesWithHealthRecord(prescribedMedicines, healthRecord);
            BindPatientDiseasesWithHealthRecord(patientDiseases, healthRecord);
            BindPatientVaccinationsWithHealthRecord(patientVaccinations, healthRecord);
            return healthRecord;
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

        private void BindPatientDiseasesWithHealthRecord(IEnumerable<PatientDisease> patientDiseases, HealthRecord healthRecord)
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
            healthRecord.PatientDisease = patientDiseasesBinded;
        }
        private void BindPatientDiseasesWithHealthRecords(IEnumerable<PatientDisease> patientDiseases, IEnumerable<HealthRecord> healthRecords)
        {
            foreach (HealthRecord hr in healthRecords)
            {
                BindPatientDiseasesWithHealthRecord(patientDiseases, hr);
            }
        }

        private void BindLabAnalysisRecordsWithHealthRecord(IEnumerable<LabAnalysisRecord> labAnalysisRecords, HealthRecord healthRecord)
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
            healthRecord.LabAnalysisRecord = labAnalysisRecordsBinded;
        }
        private void BindLabAnalysisRecordsWithHealthRecords(IEnumerable<LabAnalysisRecord> labAnalysisRecords, IEnumerable<HealthRecord> healthRecords)
        {
            foreach (HealthRecord hr in healthRecords)
            {
                BindLabAnalysisRecordsWithHealthRecord(labAnalysisRecords, hr);
            }
        }

        private void BindHospitalizationRecordsWithHealthRecord(IEnumerable<HospitalizationRecord> hospitalizationRecords, HealthRecord healthRecord)
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
            healthRecord.HospitalizationRecord = hospitalizationRecordsBinded;
        }
        private void BindHospitalizationRecordsWithHealthRecords(IEnumerable<HospitalizationRecord> hospitalizationRecords, IEnumerable<HealthRecord> healthRecords)
        {
            foreach (HealthRecord hr in healthRecords)
            {
                BindHospitalizationRecordsWithHealthRecord(hospitalizationRecords, hr);
            }
        }


        private void BindPatientVaccinationsWithHealthRecord(IEnumerable<PatientVaccination> patientVaccinations, HealthRecord healthRecord)
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
            healthRecord.PatientVaccination = patientVaccinationsBinded;
        }
        private void BindPatientVaccinationsWithHealthRecords(IEnumerable<PatientVaccination> patientVaccinations, IEnumerable<HealthRecord> healthRecords)
        {
            foreach (HealthRecord hr in healthRecords)
            {
                BindPatientVaccinationsWithHealthRecord(patientVaccinations, hr);
            }
        }


        private void BindAllergensWithHealthRecord(IEnumerable<Allergens> allergens, HealthRecord healthRecord)
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
            healthRecord.Allergens = allergensBinded;
        }
        private void BindAllergensWithHealthRecords(IEnumerable<Allergens> allergens, IEnumerable<HealthRecord> healthRecords)
        {
            foreach (HealthRecord hr in healthRecords)
            {
                BindAllergensWithHealthRecord(allergens, hr);
            }
        }


        private void BindPrescribedMedicinesWithHealthRecord(IEnumerable<PrescribedMedicine> prescribedMedicines, HealthRecord healthRecord)
        {
            List<PrescribedMedicine> prescribedMedicinesBinded = new List<PrescribedMedicine>();
            foreach(PrescribedMedicine pm1 in healthRecord.PrescribedMedicine)
            {
                foreach(PrescribedMedicine pm2 in prescribedMedicines)
                {
                    if(pm2.Id==pm1.Id)
                    {
                        prescribedMedicinesBinded.Add(pm2);
                        break;
                    }
                }
            }
            healthRecord.PrescribedMedicine = prescribedMedicinesBinded;
        }
        private void BindPrescribedMedicinesWithHealthRecords(IEnumerable<PrescribedMedicine> prescribedMedicines, IEnumerable<HealthRecord> healthRecords)
        {
            foreach(HealthRecord hr in healthRecords)
            {
                BindPrescribedMedicinesWithHealthRecord(prescribedMedicines, hr);
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
    }
}
