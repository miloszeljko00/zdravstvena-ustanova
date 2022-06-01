using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using zdravstvena_ustanova.Model.Enums;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class AntiTrollMechanismService : IAntiTrollMechanismService
    {
        private readonly IAntiTrollMechanismRepository _antiTrollMechanismRepository;
        private readonly IPatientRepository _patientRepository;

        public AntiTrollMechanismService(IAntiTrollMechanismRepository antiTrollMechanismRepository, IPatientRepository patientRepository)
        {
            _antiTrollMechanismRepository = antiTrollMechanismRepository;
            _patientRepository = patientRepository;
    }

        public AntiTrollMechanism Create(AntiTrollMechanism antiTrollMechanism)
        {
            return _antiTrollMechanismRepository.Create(antiTrollMechanism);
            
        }

        public bool Update(AntiTrollMechanism antiTrollMechanism) 
        { 
               return _antiTrollMechanismRepository.Update(antiTrollMechanism);
        }

        public AntiTrollMechanism Get(long id)
        {
            var patients = _patientRepository.GetAll();
            var antiTrollMechanism = _antiTrollMechanismRepository.Get(id);
           
            BindPatientWithMechanism(patients, antiTrollMechanism);
            return antiTrollMechanism;
        }

        public IEnumerable<AntiTrollMechanism> GetAll()
        {
            var patients = _patientRepository.GetAll();
            var antiTrollMechanisms = _antiTrollMechanismRepository.GetAll();
            foreach (AntiTrollMechanism atm in antiTrollMechanisms)
            {
                BindPatientWithMechanism(patients, atm);
            }
            return antiTrollMechanisms;
        }
        private void BindPatientWithMechanism(IEnumerable<Patient> patients, AntiTrollMechanism antiTrollMechanism)
        {
            antiTrollMechanism.Patient = FindPatientById(patients, antiTrollMechanism.Patient.Id);
        }
        private Patient FindPatientById(IEnumerable<Patient> patients, long patientId)
        {
            return patients.SingleOrDefault(patient => patient.Id == patientId);
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}