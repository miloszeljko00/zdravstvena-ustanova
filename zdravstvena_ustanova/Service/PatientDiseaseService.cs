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
    public class PatientDiseaseService
    {
        private readonly PatientDiseaseRepository _patientDiseaseRepository;
        private readonly DiseaseRepository _diseaseRepository;

        public PatientDiseaseService(PatientDiseaseRepository patientDiseaseRepository, DiseaseRepository diseaseRepository)
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

        public PatientDisease GetById(long id)
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
