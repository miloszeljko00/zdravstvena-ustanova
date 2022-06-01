using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using zdravstvena_ustanova.Repository;
using System.Linq;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly ISpecialtyRepository _specialtyRepository;

        public SpecialtyService(ISpecialtyRepository specialtyRepository)
        {
            _specialtyRepository = specialtyRepository;
        }

        public IEnumerable<Specialty> GetAll()
        {
            var specialties = _specialtyRepository.GetAll();
            return specialties;
        }
        public Specialty Get(long id)
        {
            return _specialtyRepository.Get(id);
        }

        private Specialty FindSpecialtyById(IEnumerable<Specialty> specialties, long specialtyId)
        {
            return specialties.SingleOrDefault(specialty => specialty.Id == specialtyId);
        }

        public List<Doctor> GetDoctorsBySpecialty(Specialty specialty, IEnumerable<Doctor> doctors)
        {
            var doctorsBySpecialty = new List<Doctor>();
            foreach (var doctor in doctors)
            {
                if (doctor.Specialty.Id == specialty.Id)
                {
                    doctorsBySpecialty.Add(doctor);
                }
            }
            return doctorsBySpecialty;
        }

        public Specialty Create(Specialty specialty)
        {
            return _specialtyRepository.Create(specialty);
        }
        public bool Update(Specialty specialty)
        {
            return _specialtyRepository.Update(specialty);
        }
        public bool Delete(long specialtyId)
        {
            return _specialtyRepository.Delete(specialtyId);
        }
    }
}