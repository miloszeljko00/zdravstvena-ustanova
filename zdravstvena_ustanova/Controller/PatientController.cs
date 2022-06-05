using System;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;
using System.Collections.Generic;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Controller
{
   public class PatientController
   {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public IEnumerable<Patient> GetAll()
        {
            return _patientService.GetAll();
        }
        public Patient GetById(long id)
        {
            return _patientService.Get(id);
        }

        public Patient Create(Patient patient)
        {
            return _patientService.Create(patient);
        }
        public void Update(Patient patient)
        {
            _patientService.Update(patient);
        }
        public void Delete(long patientId)
        {
            _patientService.Delete(patientId);
        }
    }
}