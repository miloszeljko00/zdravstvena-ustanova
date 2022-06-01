using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Controller
{
    public class PatientDiseaseController
    {
        private readonly IPatientDiseaseService _patientDiseaseService;

        public PatientDiseaseController(IPatientDiseaseService patientDiseaseService)
        {
            _patientDiseaseService = patientDiseaseService;
        }

        public IEnumerable<PatientDisease> GetAll()
        {
            return _patientDiseaseService.GetAll();
        }

        public PatientDisease GetById(long id)
        {
            return _patientDiseaseService.Get(id);
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
