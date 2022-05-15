using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Controller
{
    public class AntiTrollMechanismController
    {
        private readonly AntiTrollMechanismService _antiTrollMechanismService;

        public AntiTrollMechanismController(AntiTrollMechanismService antiTrollMechanismService)
        {
            _antiTrollMechanismService = antiTrollMechanismService;
        }

        public IEnumerable<AntiTrollMechanism> GetAll()
        {
            return _antiTrollMechanismService.GetAll();
        }
        public AntiTrollMechanism GetById(long Id)
        {
            return _antiTrollMechanismService.GetById(Id);
        }

        public AntiTrollMechanism Create(AntiTrollMechanism antiTrollMechanism)
        {
            return _antiTrollMechanismService.Create(antiTrollMechanism);
        }
        public void Update(AntiTrollMechanism antiTrollMechanism)
        {
            _antiTrollMechanismService.Update(antiTrollMechanism);
        }
    }
}
