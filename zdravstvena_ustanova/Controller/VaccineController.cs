using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Controller
{
    public class VaccineController
    {
        private readonly VaccineService _vaccineService;

        public VaccineController(VaccineService vaccineService)
        {
            _vaccineService = vaccineService;
        }

        public IEnumerable<Vaccine> GetAll()
        {
            return _vaccineService.GetAll();
        }
        public Vaccine GetById(IEnumerable<Vaccine> vaccines, long id)
        {
            return _vaccineService.FindVaccineById(vaccines, id);
        }
        public Vaccine Create(Vaccine vaccine)
        {
            return _vaccineService.Create(vaccine);
        }
        public bool Update(Vaccine vaccine)
        {
            return _vaccineService.Update(vaccine);
        }
        public bool Delete(long vaccineId)
        {
            return _vaccineService.Delete(vaccineId);
        }
    }
}
