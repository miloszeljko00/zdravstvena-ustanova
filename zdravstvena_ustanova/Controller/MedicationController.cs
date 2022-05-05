using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Controller
{
    public class MedicationController
    {
        private readonly MedicationService _medicationService;

        public MedicationController(MedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        public IEnumerable<Medication> GetAll()
        {
            return _medicationService.GetAll();
        }
        public Medication GetById(long id)
        {
            return _medicationService.GetById(id);
        }
        public Medication Create(Medication medication)
        {
            return _medicationService.Create(medication);
        }
        public bool Update(Medication medication)
        {
            return _medicationService.Update(medication);
        }
        public bool Delete(long medicationId)
        {
            return _medicationService.Delete(medicationId);
        }
    }
}
