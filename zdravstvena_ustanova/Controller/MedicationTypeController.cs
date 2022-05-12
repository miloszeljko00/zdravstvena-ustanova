using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Service;

namespace zdravstvena_ustanova.Controller
{
    public class MedicationTypeController
    {
        private readonly MedicationTypeService _medicationTypeService;

        public MedicationTypeController(MedicationTypeService medicationTypeService)
        {
            _medicationTypeService = medicationTypeService;
        }

        public IEnumerable<MedicationType> GetAll()
        {
            return _medicationTypeService.GetAll();
        }
        public MedicationType GetById(IEnumerable<MedicationType> medicationTypes, long id)
        {
            return _medicationTypeService.FindIngredientById(medicationTypes, id);
        }
        public MedicationType Create(MedicationType medicationType)
        {
            return _medicationTypeService.Create(medicationType);
        }
        public bool Update(MedicationType medicationType)
        {
            return _medicationTypeService.Update(medicationType);
        }
        public bool Delete(long medicationId)
        {
            return _medicationTypeService.Delete(medicationId);
        }
    }
}
