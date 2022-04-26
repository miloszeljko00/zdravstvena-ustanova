using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
using Service;
using Model;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Controller
{
    public class LabAnalysisRequestController
    {
        private readonly LabAnalysisRequestService _labAnalysisRequestService;

        public LabAnalysisRequestController(LabAnalysisRequestService labAnalysisRequestService)
        {
            _labAnalysisRequestService = labAnalysisRequestService;
        }
        public IEnumerable<LabAnalysisRequest> GetAll()
        {
            return _labAnalysisRequestService.GetAll();
        }
        public LabAnalysisRequest GetById(long id)
        {
            return _labAnalysisRequestService.GetById(id);
        }
        public LabAnalysisRequest Create(LabAnalysisRequest labAnalysisRequest)
        {
            return _labAnalysisRequestService.Create(labAnalysisRequest);
        }
        public bool Update(LabAnalysisRequest labAnalysisRequest)
        {
            return _labAnalysisRequestService.Update(labAnalysisRequest);
        }
        public bool Delete(long Id)
        {
            return _labAnalysisRequestService.Delete(Id);
        }
    }
}
