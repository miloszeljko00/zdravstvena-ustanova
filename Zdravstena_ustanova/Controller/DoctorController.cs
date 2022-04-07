using System;
using Service;
using Model;
using System.Collections.Generic;

namespace Controller
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