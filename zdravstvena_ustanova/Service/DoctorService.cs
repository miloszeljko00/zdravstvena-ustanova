using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using zdravstvena_ustanova.Repository;
using System.Linq;

namespace zdravstvena_ustanova.Service
{
    public class DoctorService
    {
        private readonly DoctorRepository _doctorRepository;
        private readonly RoomRepository _roomRepository;
        private readonly AccountRepository _accountRepository;
        private readonly SpecialtyRepository _specialtyRepository;

        public DoctorService(DoctorRepository doctorRepository, RoomRepository roomRepository, AccountRepository accountRepository, SpecialtyRepository specialtyRepository)
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
        
        public Doctor GetById(long id)
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

        private Doctor FindDoctorById(IEnumerable<Doctor> doctors, long doctorId)
        {
            return doctors.SingleOrDefault(doctor => doctor.Id == doctorId);
        }

        public Doctor Create(Doctor doctor)
        {
            return _doctorRepository.Create(doctor);
        }
        public void Update(Doctor doctor)
        {
            _doctorRepository.Update(doctor);
        }
        public void Delete(long doctorId)
        {
            _doctorRepository.Delete(doctorId);
        }
    }
}