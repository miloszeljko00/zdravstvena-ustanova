using Model;
using System;
using System.Collections.Generic;
using Repository;
using System.Linq;

namespace Service
{
    public class DoctorService
    {
        private readonly DoctorRepository _doctorRepository;
        private readonly RoomRepository _roomRepository;
        private readonly AccountRepository _accountRepository;

        public DoctorService(DoctorRepository doctorRepository, RoomRepository roomRepository, AccountRepository accountRepository)
        {
            _doctorRepository = doctorRepository;
            _roomRepository = roomRepository;
            _accountRepository = accountRepository;
        }

        public IEnumerable<Doctor> GetAll()
        {
            var rooms = _roomRepository.GetAll();
            var accounts = _accountRepository.GetAll();
            var doctors = _doctorRepository.GetAll();
            BindDoctorsWithRooms(rooms, doctors);
            BindDoctorsWithAccounts(accounts, doctors);
            return doctors;
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
            var doctor =  _doctorRepository.Get(id);
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