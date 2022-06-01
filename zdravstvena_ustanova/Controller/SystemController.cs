using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Controller
{
    public class SystemController
    {
        private readonly ISystemService _systemService;
        public SystemController(ISystemService systemService)
        {
            _systemService = systemService;
        }

        public void StartCheckingForScheduledItemTransfers(int numberOfSecondsBetweenTwoChecks)
        {
            _systemService.StartCheckingForScheduledItemTransfers(numberOfSecondsBetweenTwoChecks);
        }
        public void StartCheckingForRenovationAppointments(int numberOfSecondsBetweenTwoChecks)
        {
            _systemService.StartCheckingForRenovationAppointments(numberOfSecondsBetweenTwoChecks);
        }
    }
}
