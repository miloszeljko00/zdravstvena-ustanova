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
        private readonly AccountRepository _accountRepository;

        public PatientService(PatientRepository patientRepository, AccountRepository accountRepository)
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

        public Patient GetById(long id)
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