using Model;
using System;
using System.Collections.Generic;
using Repository;
using System.Linq;

namespace Service
{
    public class DoctorSpecService
    {
        private readonly DoctorSpecRepository _doctorSpecRepository;
        private readonly SpecialtyRepository _specialtyRepository;

        public DoctorSpecService(DoctorSpecRepository doctorSpecRepository)
        {
            _doctorSpecRepository = doctorSpecRepository;
        }

        public IEnumerable<DoctorSpecialist> GetAll()
        {
            var doctors = _doctorSpecRepository.GetAll();
            var specialties = _specialtyRepository.GetAll();
            return doctors;
        }
        public DoctorSpecialist GetById(long id)
        {
            return _doctorSpecRepository.Get(id);
        }

        private void BindSpecialtyWithDoctorSpec(IEnumerable<Specialty> specialties, DoctorSpecialist doctorSpecialist)
        {
            doctorSpecialist.Specialty = FindSpecialtyById(specialties, doctorSpecialist.SpecialtyId);
        }

        private void BindSpecialtyWithDoctorSpecs(IEnumerable<Specialty> specialties, IEnumerable<DoctorSpecialist> doctorSpecialists)
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
        private DoctorSpecialist FindDoctorSpecById(IEnumerable<DoctorSpecialist> doctors, long doctorId)
        {
            return doctors.SingleOrDefault(doctor => doctor.Id == doctorId);
        }

        public DoctorSpecialist Create(DoctorSpecialist doctor)
        {
            return _doctorSpecRepository.Create(doctor);
        }
        public void Update(DoctorSpecialist doctor)
        {
            _doctorSpecRepository.Update(doctor);
        }
        public void Delete(long doctorId)
        {
            _doctorSpecRepository.Delete(doctorId);
        }
    }
}