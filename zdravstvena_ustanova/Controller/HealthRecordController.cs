using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
using Model;

namespace zdravstvena_ustanova.Controller
{
    public class HealthRecordController
    {
        private readonly HealthRecordService _healthRecordService;

        public HealthRecordController(HealthRecordService healthRecordService)
        {
            _healthRecordService = healthRecordService;
        }
    }
}
