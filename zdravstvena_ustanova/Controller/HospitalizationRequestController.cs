using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Service;
using Model;

namespace zdravstvena_ustanova.Controller
{
    public class HospitalizationRequestController
    {
        private readonly HospitalizationRequestService _hospitalizationRequestService;

        public HospitalizationRequestController(HospitalizationRequestService hospitalizationRequestService)
        {
            _hospitalizationRequestService = hospitalizationRequestService;
        }
        public IEnumerable<HospitalizationRequest> GetAll()
        {
            return _hospitalizationRequestService.GetAll();
        }
        public HospitalizationRequest GetById(long id)
        {
            return _hospitalizationRequestService.GetById(id);
        }
        public HospitalizationRequest Create(HospitalizationRequest hospitalizationRequest)
        {
            return _hospitalizationRequestService.Create(hospitalizationRequest);
        }
        public bool Update(HospitalizationRequest hospitalizationRequest)
        {
            return _hospitalizationRequestService.Update(hospitalizationRequest);
        }
        public bool Delete(long id)
        {
            return _hospitalizationRequestService.Delete(id);
        }
    }
}
