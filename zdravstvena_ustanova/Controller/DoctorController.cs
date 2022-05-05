using System;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;
using System.Collections.Generic;

namespace zdravstvena_ustanova.Controller
{
   public class DoctorController
   {
        private readonly DoctorService _doctorService;

        public DoctorController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _doctorService.GetAll();
        }
        public Doctor GetById(long id)
        {
            return _doctorService.GetById(id);
        }

        public Doctor Create(Doctor doctor)
        {
            return _doctorService.Create(doctor);
        }
        public void Update(Doctor doctor)
        {
            _doctorService.Update(doctor);
        }
        public void Delete(long doctorId)
        {
            _doctorService.Delete(doctorId);
        }
    }
}