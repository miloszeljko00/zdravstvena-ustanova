using System;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;
using System.Collections.Generic;

namespace zdravstvena_ustanova.Controller
{
   public class SpecialtyController
   {
        private readonly SpecialtyService _specialtyService;

        public SpecialtyController(SpecialtyService specialtyService)
        {
            _specialtyService = specialtyService;
        }

        public IEnumerable<Specialty> GetAll()
        {
            return _specialtyService.GetAll();
        }
        public Specialty GetById(long id)
        {
            return _specialtyService.GetById(id);
        }

        public Specialty Create(Specialty specialty)
        {
            return _specialtyService.Create(specialty);
        }
        public void Update(Specialty specialty)
        {
            _specialtyService.Update(specialty);
        }
        public void Delete(long specialtyId)
        {
            _specialtyService.Delete(specialtyId);
        }
        public List<Doctor> GetDoctorsBySpecialty(Specialty specialty, IEnumerable<Doctor> doctors)
        {
            return _specialtyService.GetDoctorsBySpecialty(specialty, doctors);
        }
    }
}