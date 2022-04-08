using Model;
using System;
using System.Collections.Generic;
using Repository;
using System.Linq;

namespace Service
{
    public class SpecialtyService
    {
        private readonly SpecialtyRepository _specialtyRepository;

        public SpecialtyService(SpecialtyRepository specialtyRepository)
        {
            _specialtyRepository = specialtyRepository;
        }

        public IEnumerable<Specialty> GetAll()
        {
            var specialties = _specialtyRepository.GetAll();
            return specialties;
        }
        public Specialty GetById(long id)
        {
            return _specialtyRepository.Get(id);
        }

        private Specialty FindSpecialtyById(IEnumerable<Specialty> specialties, long specialtyId)
        {
            return specialties.SingleOrDefault(specialty => specialty.Id == specialtyId);
        }

        public Specialty Create(Specialty specialty)
        {
            return _specialtyRepository.Create(specialty);
        }
        public void Update(Specialty specialty)
        {
            _specialtyRepository.Update(specialty);
        }
        public void Delete(long specialtyId)
        {
            _specialtyRepository.Delete(specialtyId);
        }
    }
}