using System;
using Service;
using Model;
using System.Collections.Generic;

namespace Controller
{
   public class DoctorSpecController
   {
        private readonly DoctorSpecService _doctorSpecService;

        public DoctorSpecController(DoctorSpecService doctorSpecService)
        {
            _doctorSpecService = doctorSpecService;
        }

        public IEnumerable<DoctorSpecialist> GetAll()
        {
            return _doctorSpecService.GetAll();
        }
        public DoctorSpecialist GetById(long id)
        {
            return _doctorSpecService.GetById(id);
        }

        public DoctorSpecialist Create(DoctorSpecialist doctor)
        {
            return _doctorSpecService.Create(doctor);
        }
        public void Update(DoctorSpecialist doctor)
        {
            _doctorSpecService.Update(doctor);
        }
        public void Delete(long doctorId)
        {
            _doctorSpecService.Delete(doctorId);
        }
    }
}