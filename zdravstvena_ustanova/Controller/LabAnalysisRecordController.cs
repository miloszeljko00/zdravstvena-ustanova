using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Controller
{
    public class LabAnalysisRecordController
    {
        private readonly LabAnalysisRecordService _labAnalysisRecordService;

        public LabAnalysisRecordController(LabAnalysisRecordService labAnalysisRecordService)
        {
            _labAnalysisRecordService = labAnalysisRecordService;
        }

        public IEnumerable<LabAnalysisRecord> GetAll()
        {
            return _labAnalysisRecordService.GetAll();
        }
        public LabAnalysisRecord GetById(long id)
        {
            return _labAnalysisRecordService.GetById(id);
        }
        public LabAnalysisRecord Create(LabAnalysisRecord labAnalysisRecord)
        {
            return _labAnalysisRecordService.Create(labAnalysisRecord);
        }
        public bool Update(LabAnalysisRecord labAnalysisRecord)
        {
            return _labAnalysisRecordService.Update(labAnalysisRecord);
        }
        public bool Delete(long labAnalysisRecordId)
        {
            return _labAnalysisRecordService.Delete(labAnalysisRecordId);
        }
    }
}
