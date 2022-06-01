using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Controller
{
    public class AntiTrollMechanismController
    {
        private readonly IAntiTrollMechanismService _antiTrollMechanismService;

        public AntiTrollMechanismController(IAntiTrollMechanismService antiTrollMechanismService)
        {
            _antiTrollMechanismService = antiTrollMechanismService;
        }

        public IEnumerable<AntiTrollMechanism> GetAll()
        {
            return _antiTrollMechanismService.GetAll();
        }
        public AntiTrollMechanism GetById(long Id)
        {
            return _antiTrollMechanismService.Get(Id);
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
