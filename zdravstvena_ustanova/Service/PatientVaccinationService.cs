using zdravstvena_ustanova.Model;
using System.Collections.Generic;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class PatientVaccinationService : IPatientVaccinationService
    {
        private readonly IPatientVaccinationRepository _patientVaccinationRepository;
        private readonly IVaccineRepository _vaccineRepository;

        public PatientVaccinationService(IPatientVaccinationRepository patientVaccinationRepository, IVaccineRepository vaccineRepository)
        {
            _patientVaccinationRepository = patientVaccinationRepository;
            _vaccineRepository = vaccineRepository;
        }

        public IEnumerable<PatientVaccination> GetAll()
        {
            var patientVaccinations = _patientVaccinationRepository.GetAll();
            var vaccinations = _vaccineRepository.GetAll();
            BindPatientVaccinationsWithVaccines(patientVaccinations, vaccinations);
            return patientVaccinations;
        }

        private void BindPatientVaccinationsWithVaccines(IEnumerable<PatientVaccination> patientVaccinations, IEnumerable<Vaccine> vaccinations)
        {
            foreach (PatientVaccination pv in patientVaccinations)
            {
                BindPatientVaccinationWithVaccines(pv, vaccinations);
            }
        }

        public PatientVaccination Get(long id)
        {
            var vaccinations = _vaccineRepository.GetAll();
            var patientVaccination = _patientVaccinationRepository.Get(id);
            BindPatientVaccinationWithVaccines(patientVaccination, vaccinations);
            return patientVaccination;
        }

        private void BindPatientVaccinationWithVaccines(PatientVaccination patientVaccination, IEnumerable<Vaccine> vaccinations)
        {
            foreach (Vaccine v in vaccinations)
            {
                if (patientVaccination.Vaccine.Id == v.Id)
                {
                    patientVaccination.Vaccine = v;
                    break;
                }
            }
        }

        public PatientVaccination Create(PatientVaccination patientVaccination)
        {
            return _patientVaccinationRepository.Create(patientVaccination);
        }
        public bool Update(PatientVaccination patientVaccination)
        {
            return _patientVaccinationRepository.Update(patientVaccination);
        }
        public bool Delete(long patientVaccinationId)
        {
            return _patientVaccinationRepository.Delete(patientVaccinationId);
        }
    }
}
