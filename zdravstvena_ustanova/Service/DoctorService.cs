using zdravstvena_ustanova.Model;
using System.Collections.Generic;
using System.Linq;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;
using zdravstvena_ustanova.Model.Enums;

namespace zdravstvena_ustanova.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ISpecialtyRepository _specialtyRepository;

        public DoctorService(IDoctorRepository doctorRepository, IRoomRepository roomRepository,
            IAccountRepository accountRepository, ISpecialtyRepository specialtyRepository)
        {
            _doctorRepository = doctorRepository;
            _roomRepository = roomRepository;
            _accountRepository = accountRepository;
            _specialtyRepository = specialtyRepository;

        }

        public IEnumerable<Doctor> GetAll()
        {
            var rooms = _roomRepository.GetAll();
            var accounts = _accountRepository.GetAll();
            var doctors = _doctorRepository.GetAll();
            var specialties = _specialtyRepository.GetAll();
            BindDoctorsWithSpecialties(specialties, doctors);
            BindDoctorsWithRooms(rooms, doctors);
            BindDoctorsWithAccounts(accounts, doctors);
            return doctors;
        }

        public Doctor GetDoctorByShift(int hour)
        {
            var doctors = GetAll();
            Doctor doctor = null;
            foreach (Doctor d in doctors)
            {
                if (hour < 14 && d.Shift == Shift.FIRST)
                {
                    doctor = d;
                    break;
                }
                else if (hour >= 14 && d.Shift == Shift.SECOND)
                {
                    doctor = d;
                    break;
                }
            }
            return doctor;
        }
        public Doctor GetDoctorByNameSurname(string nameSurname)
        {
            var doctors = GetAll();
            Doctor doctor = null;
            foreach (Doctor d in doctors)
            {
                if (nameSurname.Equals(d.Name + " " + d.Surname))
                {
                    doctor = d;
                    break;
                }
            }
            return doctor;
        }
        private void BindDoctorsWithSpecialties(IEnumerable<Specialty> specialties, IEnumerable<Doctor> doctors)
        {
            doctors.ToList().ForEach(doctor =>
            {
                BindDoctorWithSpecialty(specialties, doctor);
            });
        }

        private void BindDoctorWithSpecialty(IEnumerable<Specialty> specialties, Doctor doctor)
        {
            specialties.ToList().ForEach(specialty =>
            {
                if (specialty.Id == doctor.Specialty.Id)
                {
                    doctor.Specialty = specialty;
                }
            });
        }

        private void BindDoctorsWithAccounts(IEnumerable<Account> accounts, IEnumerable<Doctor> doctors)
        {
            doctors.ToList().ForEach(doctor =>
            {
               BindDoctorWithAccount(accounts, doctor);
            });
        }
        private void BindDoctorWithAccount(IEnumerable<Account> accounts, Doctor doctor)
        {
            accounts.ToList().ForEach(account =>
            {
                if (account.Id == doctor.Account.Id)
                {
                    account.Person = doctor;
                    doctor.Account = account;
                }
            });
        }
        private void BindDoctorsWithRooms(IEnumerable<Room> rooms, IEnumerable<Doctor> doctors)
        {
            doctors.ToList().ForEach(doctor =>
            {
                rooms.ToList().ForEach(room =>
                {
                    if(room.Id == doctor.Room.Id)
                    {
                        doctor.Room = room;
                    }
                });
            });
        }
        
        public Doctor Get(long id)
        {
            var rooms = _roomRepository.GetAll();
            var accounts = _accountRepository.GetAll();
            var specialties = _specialtyRepository.GetAll();
            var doctor =  _doctorRepository.Get(id);
            BindDoctorWithSpecialty(specialties, doctor);
            BindDoctorWithRoom(rooms, doctor);
            BindDoctorWithAccount(accounts, doctor);
            return doctor;
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

        public Doctor Create(Doctor doctor)
        {
            return _doctorRepository.Create(doctor);
        }
        public bool Update(Doctor doctor)
        {
            return _doctorRepository.Update(doctor);
        }
        public bool Delete(long doctorId)
        {
           return  _doctorRepository.Delete(doctorId);
        }
    }
}