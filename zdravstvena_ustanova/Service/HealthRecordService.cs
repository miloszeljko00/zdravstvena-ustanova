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
            var anamnesis = _anamnesisRepository.GetAll();
            var labAnalysisRecords = _labAnalysisRecordRepository.GetAll();
            var hospitalizationRecords = _hospitalizationRecordRepository.GetAll();
            var prescribedMedicines = _prescribedMedicineRepository.GetAll();
            var patientDiseases = _patientDiseaseRepository.GetAll();
            var patientVaccinations = _patientVaccinationRepository.GetAll();
            return null;
        }

    }
}
