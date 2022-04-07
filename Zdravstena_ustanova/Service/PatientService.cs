using Model;
using System;
using System.Collections.Generic;
using Repository;
using System.Linq;

namespace Service
{
    public class PatientService
    {
        private readonly PatientRepository _patientRepository;

        public PatientService(PatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        internal IEnumerable<Patient> GetAll()
        {
            return _patientRepository.GetAll();
        }
        internal Patient GetById(long id)
        {
            return _patientRepository.Get(id);
        }

        private Patient FindPatientById(IEnumerable<Patient> patients, long patientId)
        {
            return patients.SingleOrDefault(patient => patient.Id == patientId);
        }

        public Patient Create(Patient patient)
        {
            return _patientRepository.Create(patient);
        }
        public void Update(Patient patient)
        {
            _patientRepository.Update(patient);
        }
        public void Delete(long patientId)
        {
            _patientRepository.Delete(patientId);
        }
    }
}