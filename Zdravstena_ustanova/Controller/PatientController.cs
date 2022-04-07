using System;
using Service;
using Model;
using System.Collections.Generic;

namespace Controller
{
   public class PatientController
   {
        private readonly PatientService _patientService;

        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }

        public IEnumerable<Patient> GetAll()
        {
            return _patientService.GetAll();
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