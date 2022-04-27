using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Repository;

namespace zdravstvena_ustanova.Service
{
    public class PatientVaccinationService
    {
        private readonly PatientVaccinationRepository _patientVaccinationRepository;
        private readonly VaccineRepository _vaccineRepository;

        public PatientVaccinationService(PatientVaccinationRepository patientVaccinationRepository, VaccineRepository vaccineRepository)
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

        public PatientVaccination GetById(long id)
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
