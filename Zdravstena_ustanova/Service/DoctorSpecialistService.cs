using Model;
using System;
using System.Collections.Generic;
using Repository;
using System.Linq;

namespace Service
{
    public class DoctorSpecialistService
    {
        private readonly DoctorSpecialistRepository _doctorSpecialistRepository;
        private readonly SpecialtyService _specialtyService;

        public DoctorSpecialistService(DoctorSpecialistRepository doctorSpecialistRepository, SpecialtyService specialtyService)
        {
            _doctorSpecialistRepository = doctorSpecialistRepository;
            _specialtyService = specialtyService;
        }

        public IEnumerable<DoctorSpecialist> GetAll()
        {
            var doctors = _doctorSpecialistRepository.GetAll();
            var specialties = _specialtyService.GetAll();
            BindSpecialtyWithDoctorSpecialists(specialties, doctors);
            return doctors;
        }
        public DoctorSpecialist GetById(long id)
        {
            return _doctorSpecialistRepository.Get(id);
        }

        private void BindSpecialtyWithDoctorSpecialist(IEnumerable<Specialty> specialties, DoctorSpecialist doctorSpecialist)
        {
            doctorSpecialist.Specialty = FindSpecialtyById(specialties, doctorSpecialist.SpecialtyId);
        }

        private void BindSpecialtyWithDoctorSpecialists(IEnumerable<Specialty> specialties, IEnumerable<DoctorSpecialist> doctorSpecialists)
        {
            doctorSpecialists.ToList().ForEach(doctorSpecialist =>
            {
                doctorSpecialist.Specialty = FindSpecialtyById(specialties, doctorSpecialist.SpecialtyId);
            });
        }
        private Specialty FindSpecialtyById(IEnumerable<Specialty> specialties, long specialtyId)
        {
            return specialties.SingleOrDefault(specialty => specialty.Id == specialtyId);
        }
        private DoctorSpecialist FindDoctorSpecialistById(IEnumerable<DoctorSpecialist> doctors, long doctorId)
        {
            return doctors.SingleOrDefault(doctor => doctor.Id == doctorId);
        }

        public DoctorSpecialist Create(DoctorSpecialist doctor)
        {
            return _doctorSpecialistRepository.Create(doctor);
        }
        public void Update(DoctorSpecialist doctor)
        {
            _doctorSpecialistRepository.Update(doctor);
        }
        public void Delete(long doctorId)
        {
            _doctorSpecialistRepository.Delete(doctorId);
        }
    }
}