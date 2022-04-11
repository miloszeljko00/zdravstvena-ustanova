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
        private readonly RoomService _roomService;

        public DoctorService(DoctorRepository doctorRepository, RoomService roomService)
        {
            _doctorRepository = doctorRepository;
            _roomService = roomService;
        }

        public IEnumerable<Doctor> GetAll()
        {
            var rooms = _roomService.GetAll();
            var doctors = _doctorRepository.GetAll();
            BindDoctorsWithRooms(rooms, doctors);
            return doctors;
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
            Doctor doctor =  _doctorRepository.Get(id);
            IEnumerable<Room> rooms = _roomService.GetAll();
            BindDoctorWithRoom(rooms, doctor);
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