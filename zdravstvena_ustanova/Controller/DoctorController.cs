using System;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;
using System.Collections.Generic;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Controller
{
   public class DoctorController
   {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _doctorService.GetAll();
        }

        public Doctor GetDoctorByShift(int hour)
        {
            return _doctorService.GetDoctorByShift(hour);
        }
        public Doctor GetDoctorByNameSurname(string nameSurname)
        {
            return _doctorService.GetDoctorByNameSurname(nameSurname);
        }
        public Doctor GetById(long id)
        {
            return _doctorService.Get(id);
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