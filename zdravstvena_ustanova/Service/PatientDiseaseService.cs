using zdravstvena_ustanova.Model;
using System.Collections.Generic;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class PatientDiseaseService : IPatientDiseaseService
    {
        private readonly IPatientDiseaseRepository _patientDiseaseRepository;
        private readonly IDiseaseRepository _diseaseRepository;

        public PatientDiseaseService(IPatientDiseaseRepository patientDiseaseRepository, IDiseaseRepository diseaseRepository)
        {
            _patientDiseaseRepository = patientDiseaseRepository;
            _diseaseRepository = diseaseRepository;
        }

        public IEnumerable<PatientDisease> GetAll()
        {
            var patientDiseases = _patientDiseaseRepository.GetAll();
            var diseases = _diseaseRepository.GetAll();
            BindPatientDiseasesWithDiseases(patientDiseases, diseases);
            return patientDiseases;
        }

        private void BindPatientDiseasesWithDiseases(IEnumerable<PatientDisease> patientDiseases, IEnumerable<Disease> diseases)
        {

            foreach (PatientDisease pd in patientDiseases)
            {
                BindPatientDiseaseWithDiseases(pd, diseases);
            }
        }

        public PatientDisease Get(long id)
        {
            var diseases = _diseaseRepository.GetAll();
            var patientDisease = _patientDiseaseRepository.Get(id);
            BindPatientDiseaseWithDiseases(patientDisease, diseases);
            return patientDisease;
        }

        private void BindPatientDiseaseWithDiseases(PatientDisease patientDisease, IEnumerable<Disease> diseases)
        {
            foreach (Disease d in diseases)
            {
                if (patientDisease.Disease.Id == d.Id)
                {
                    patientDisease.Disease = d;
                    break;
                }
            }
        }

        public PatientDisease Create(PatientDisease patientDisease)
        {
            return _patientDiseaseRepository.Create(patientDisease);
        }
        public bool Update(PatientDisease patientDisease)
        {
            return _patientDiseaseRepository.Update(patientDisease);
        }
        public bool Delete(long patientDiseaseId)
        {
            return _patientDiseaseRepository.Delete(patientDiseaseId);
        }
    }
}
