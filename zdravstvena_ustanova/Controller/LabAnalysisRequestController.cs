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
    public class LabAnalysisRequestController
    {
        private readonly ILabAnalysisRequestService _labAnalysisRequestService;

        public LabAnalysisRequestController(ILabAnalysisRequestService labAnalysisRequestService)
        {
            _labAnalysisRequestService = labAnalysisRequestService;
        }
        public IEnumerable<LabAnalysisRequest> GetAll()
        {
            return _labAnalysisRequestService.GetAll();
        }
        public LabAnalysisRequest GetById(long id)
        {
            return _labAnalysisRequestService.Get(id);
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
