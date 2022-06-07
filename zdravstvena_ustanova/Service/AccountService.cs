using zdravstvena_ustanova.Model;
using System.Collections.Generic;
using System.Linq;
using zdravstvena_ustanova.Model.Enums;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ISecretaryRepository _secretaryRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IRoomRepository _roomRepository;

        public AccountService(IAccountRepository accountsRepository, IPatientRepository patientRepository, IDoctorRepository doctorRepository,
            ISecretaryRepository secretaryRepository, IManagerRepository managerRepository, IRoomRepository roomRepository)
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

        public Account Get(long id)
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

            return Get(accountId).Person;
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

        private void BindPersonWithAccount(IEnumerable<Patient> patients, IEnumerable<Doctor> doctors, IEnumerable<Manager> managers, IEnumerable<Secretary> secretaries, Account account)
        {
            if (account.Person == null) return;
            switch(account.AccountType)
            {
                case AccountType.GUEST:
                case AccountType.PATIENT:
                    BindPatientWithAccount(patients, account.Person.Id, account);
                    break;
                case AccountType.DOCTOR:
                    BindDoctorWithAccount(doctors, account.Person.Id, account);
                    break;
                case AccountType.MANAGER:
                    BindManagerWithAccount(managers, account.Person.Id, account);
                    break;
                case AccountType.SECRETARY:
                    BindSecretaryWithAccount(secretaries, account.Person.Id, account);
                    break;
                default:
                    break;
            }
        }
        private void BindPatientWithAccount(IEnumerable<Patient> patients, long patientId, Account account)
        {
            Patient patient = patients.SingleOrDefault(patient => patient.Id == patientId);
            patient.Account = account;
            account.Person = patient;
        }

        private void BindDoctorWithAccount(IEnumerable<Doctor> doctors, long doctorId, Account account)
        {
            Doctor doctor = doctors.SingleOrDefault(doctor => doctor.Id == doctorId);
            doctor.Account = account;
            account.Person = doctor;
            var rooms = _roomRepository.GetAll();
            BindDoctorWithRoom(rooms, doctor);
        }

        private void BindManagerWithAccount(IEnumerable<Manager> managers, long managerId, Account account)
        {
            Manager manager = managers.SingleOrDefault(manager => manager.Id == managerId);
            manager.Account = account;
            account.Person = manager;
        }
        private void BindSecretaryWithAccount(IEnumerable<Secretary> secrearies, long secretaryId, Account account)
        {
            Secretary secretary = secrearies.SingleOrDefault(secretary => secretary.Id == secretaryId);
            secretary.Account = account;
            account.Person = secretary;
        }

        public bool IsUniqueUsername(string username)
        {
            var accounts = _accountRepository.GetAll();
            bool ret = true;
            foreach(Account a in accounts)
            {
                if(a.Username.Equals(username))
                {
                    ret = false;
                    break;
                }
            }
            return ret;
        }

        public Account FindAccountByDoctorId(long doctorId)
        {
            var accounts = GetAll();
            Account ret = null;
            foreach(Account a in accounts)
            {
                if (a.AccountType == AccountType.DOCTOR && a.Person.Id == doctorId)
                {
                    ret = a;
                    break;
                }

            }
            return ret;
        }

    }
}