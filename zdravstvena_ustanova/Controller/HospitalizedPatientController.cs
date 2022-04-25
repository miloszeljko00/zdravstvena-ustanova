using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;
using Model;


namespace zdravstvena_ustanova.Controller
{
    public class HospitalizedPatientController
    {
        private readonly HospitalizedPatientService _hospitalizedPatientService;

        public HospitalizedPatientController(HospitalizedPatientService hospitalizedPatientService)
        {
            _hospitalizedPatientService = hospitalizedPatientService;
        }

        public IEnumerable<HospitalizedPatient> GetAll()
        {
            return _hospitalizedPatientService.GetAll();
        }
        public HospitalizedPatient GetById(long id)
        {
            return _hospitalizedPatientService.GetById(id);
        }
        public HospitalizedPatient Create(HospitalizedPatient hospitalizedPatient)
        {
            return _hospitalizedPatientService.Create(hospitalizedPatient);
        }
        public bool Update(HospitalizedPatient hospitalizedPatient)
        {
            return _hospitalizedPatientService.Update(hospitalizedPatient);
        }
        public bool Delete(long id)
        {
            return _hospitalizedPatientService.Delete(id);
        }
    }
}
