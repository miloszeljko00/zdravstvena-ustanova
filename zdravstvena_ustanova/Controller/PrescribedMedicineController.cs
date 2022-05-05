using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Controller
{
    public class PrescribedMedicineController
    {
        private readonly PrescribedMedicineService _prescribedMedicineService;

        public PrescribedMedicineController(PrescribedMedicineService prescribedMedicineService)
        {
            _prescribedMedicineService = prescribedMedicineService;
        }

        public IEnumerable<PrescribedMedicine> GetAll()
        {
            return _prescribedMedicineService.GetAll();
        }

        public PrescribedMedicine GetById(long id)
        {
            return _prescribedMedicineService.GetById(id);
        }

        public PrescribedMedicine Create(PrescribedMedicine prescribedMedicine)
        {
            return _prescribedMedicineService.Create(prescribedMedicine);
        }
        public bool Update(PrescribedMedicine prescribedMedicine)
        {
            return _prescribedMedicineService.Update(prescribedMedicine);
        }
        public bool Delete(long prescribedMedicineId)
        {
            return _prescribedMedicineService.Delete(prescribedMedicineId);
        }
    }
}
