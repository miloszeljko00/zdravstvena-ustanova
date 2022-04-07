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

        public DoctorService(DoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        internal IEnumerable<Doctor> GetAll()
        {
            var doctors = _doctorRepository.GetAll();
            return doctors;
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