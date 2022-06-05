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
    public class HospitalizationRecordController
    {
        private readonly IHospitalizationRecordService _hospitalizationRecordService;

        public HospitalizationRecordController(IHospitalizationRecordService hospitalizationRecordService)
        {
            _hospitalizationRecordService = hospitalizationRecordService;
        }

        public IEnumerable<HospitalizationRecord> GetAll()
        {
            return _hospitalizationRecordService.GetAll();
        }

        public HospitalizationRecord GetById(long id)
        {
            return _hospitalizationRecordService.Get(id);
        }

        public HospitalizationRecord Create(HospitalizationRecord hospitalizationRecord)
        {
            return _hospitalizationRecordService.Create(hospitalizationRecord);
        }
        public bool Update(HospitalizationRecord hospitalizationRecord)
        {
            return _hospitalizationRecordService.Update(hospitalizationRecord);
        }
        public bool Delete(long hospitalizationRecordId)
        {
            return _hospitalizationRecordService.Delete(hospitalizationRecordId);
        }
    }
}
