using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;

namespace Controller
{
    public class SystemController
    {
        private readonly SystemService _systemService;
        public SystemController(SystemService systemService)
        {
            _systemService = systemService;
        }

        public void StartCheckingForScheduledItemTransfers(int numberOfSecondsBetweenTwoChecks)
        {
            _systemService.StartCheckingForScheduledItemTransfers(numberOfSecondsBetweenTwoChecks);
        }
    }
}
