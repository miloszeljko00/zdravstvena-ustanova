using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using zdravstvena_ustanova.Model.Enums;
using zdravstvena_ustanova.Repository.RepositoryInterface;

namespace zdravstvena_ustanova.Service
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly PatientRepository _patientRepository;
        private readonly DoctorRepository _doctorRepository;
        private readonly SecretaryRepository _secretaryRepository;
        private readonly ManagerRepository _managerRepository;
        private readonly RoomRepository _roomRepository;

        public AccountService(IAccountRepository accountsRepository, PatientRepository patientRepository, DoctorRepository doctorRepository,
            SecretaryRepository secretaryRepository, ManagerRepository managerRepository, RoomRepository roomRepository)
        {
            _accountRepository = accountsRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _secretaryRepository = secretaryRepository;
            _managerRepository = managerRepository;
            _roomRepository = roomRepository;
    }

        public Account Create(Account account)
        {
            return _accountRepository.Create(account);
            
        }

        public bool Delete(long accountId)
        {
            return _accountRepository.Delete(accountId);
        }

        public bool Update(Account account) 
        { 
               return _accountRepository.Update(account);
        }

        public Account GetById(long id)
        {
            var patients = _patientRepository.GetAll();
            var doctors = _doctorRepository.GetAll();
            var managers = _managerRepository.GetAll();
            var secretaries = _secretaryRepository.GetAll();
            var account = _accountRepository.Get(id);
            
            BindPersonWithAccount(patients, doctors, managers, secretaries, account);
            return account;
        }

        public IEnumerable<Account> GetAll()
        {
            var patients = _patientRepository.GetAll();
            var doctors = _doctorRepository.GetAll();
            var managers = _managerRepository.GetAll();
            var secretaries = _secretaryRepository.GetAll();
            var accounts = _accountRepository.GetAll();
            foreach (Account acc in accounts)
            {
                BindPersonWithAccount(patients, doctors, managers, secretaries, acc);
            }
            return accounts;
        }

        private bool CanLogin(string username, string password)
        {
            var account = _accountRepository.GetByUsername(username);

            if (account == null) return false;
            if(account.Password != password) return false;
            if (!account.IsEnabled) return false;

            return true;
        }

        public Person Login(string username, string password)
        {
            if (!CanLogin(username, password)) return null;

            var accountId =  _accountRepository.GetByUsername(username).Id;

            return GetById(accountId).Person;
        }
        private void BindDoctorWithRoom(IEnumerable<Room> rooms, Doctor doctor)
        {
            rooms.ToList().ForEach(room =>
            {
                if (room.Id == doctor.Room.Id)
                {
                    doctor.Room = room;
                    return;
                }
            });
        }

        public void BindPersonWithAccount(IEnumerable<Patient> patients, IEnumerable<Doctor> doctors, IEnumerable<Manager> managers, IEnumerable<Secretary> secretaries, Account account)
        {
            if (account.Person == null) return;
            switch(account.AccountType)
            {
                case AccountType.GUEST:
                case AccountType.PATIENT:
                    Patient patient = FindPatientById(patients, account.Person.Id);
                    patient.Account = account;
                    account.Person = patient;
                    break;
                case AccountType.DOCTOR:
                    Doctor doctor = FindDoctorById(doctors, account.Person.Id);
                    doctor.Account = account;
                    account.Person = doctor;
                    var rooms = _roomRepository.GetAll();
                    BindDoctorWithRoom(rooms, doctor);
                    break;
                case AccountType.MANAGER:
                    Manager manager = FindManagerById(managers, account.Person.Id);
                    manager.Account = account;
                    account.Person = manager;
                    break;
                case AccountType.SECRETARY:
                    Secretary secretary = FindSecretaryById(secretaries, account.Person.Id);
                    secretary.Account = account;
                    account.Person = secretary;
                    break;
                default:
                    break;
            }
        }
        private Patient FindPatientById(IEnumerable<Patient> patients, long patientId)
        {
            return patients.SingleOrDefault(patient => patient.Id == patientId);
        }

        private Doctor FindDoctorById(IEnumerable<Doctor> doctors, long doctorId)
        {
            return doctors.SingleOrDefault(doctor => doctor.Id == doctorId);
        }

        private Manager FindManagerById(IEnumerable<Manager> managers, long managerId)
        {
            return managers.SingleOrDefault(manager => manager.Id == managerId);
        }
        private Secretary FindSecretaryById(IEnumerable<Secretary> secrearies, long secretaryId)
        {
            return secrearies.SingleOrDefault(secretary => secretary.Id == secretaryId);
        }


    }
}