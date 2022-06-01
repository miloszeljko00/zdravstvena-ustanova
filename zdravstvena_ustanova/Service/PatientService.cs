using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using zdravstvena_ustanova.Repository;
using System.Linq;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IAccountRepository _accountRepository;

        public PatientService(IPatientRepository patientRepository, IAccountRepository accountRepository)
        {
            _patientRepository = patientRepository;
            _accountRepository = accountRepository;
        }


        public IEnumerable<Patient> GetAll()
        {
            var patients = _patientRepository.GetAll();
            var accounts = _accountRepository.GetAll();
            BindPatientsWithAccounts(patients, accounts);
            return patients;
        }

        private void BindPatientsWithAccounts(IEnumerable<Patient> patients, IEnumerable<Account> accounts)
        {
            foreach (Patient p in patients)
            {
                BindPatientWithAccount(p, accounts);
            }
        }

        public Patient Get(long id)
        {
            var patient = _patientRepository.Get(id);
            var accounts = _accountRepository.GetAll();
            BindPatientWithAccount(patient, accounts);
            return patient;
        }

        private void BindPatientWithAccount(Patient patient, IEnumerable<Account> accounts)
        {
            foreach(Account acc in accounts)
            {
                if (acc.Person == null) continue;
                if(acc.Person.Id == patient.Id)
                {
                    patient.Account = acc;
                    acc.Person = patient;
                }
            }
        }

        private Patient FindPatientById(IEnumerable<Patient> patients, long patientId)
        {
            return patients.SingleOrDefault(patient => patient.Id == patientId);
        }

        public Patient Create(Patient patient)
        {
            return _patientRepository.Create(patient);
        }
        public bool Update(Patient patient)
        {
            return _patientRepository.Update(patient);
        }
        public bool Delete(long patientId)
        {
            return _patientRepository.Delete(patientId);
        }
    }
}