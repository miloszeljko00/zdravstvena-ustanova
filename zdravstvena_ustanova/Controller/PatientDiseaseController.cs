using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
using Model;

namespace zdravstvena_ustanova.Controller
{
    public class PatientDiseaseController
    {
        private readonly PatientDiseaseService _patientDiseaseService;

        public PatientDiseaseController(PatientDiseaseService patientDiseaseService)
        {
            _patientDiseaseService = patientDiseaseService;
        }

        public IEnumerable<PatientDisease> GetAll()
        {
            return _patientDiseaseService.GetAll();
        }

        public PatientDisease GetById(long id)
        {
            return _patientDiseaseService.GetById(id);
        }

        public PatientDisease Create(PatientDisease patientDisease)
        {
            return _patientDiseaseService.Create(patientDisease);
        }
        public bool Update(PatientDisease patientDisease)
        {
            return _patientDiseaseService.Update(patientDisease);
        }
        public bool Delete(long patientDiseaseId)
        {
            return _patientDiseaseService.Delete(patientDiseaseId);
        }
    }
}
