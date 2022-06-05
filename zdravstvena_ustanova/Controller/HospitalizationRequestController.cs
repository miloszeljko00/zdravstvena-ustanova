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
    public class HospitalizationRequestController
    {
        private readonly IHospitalizationRequestService _hospitalizationRequestService;

        public HospitalizationRequestController(IHospitalizationRequestService hospitalizationRequestService)
        {
            _hospitalizationRequestService = hospitalizationRequestService;
        }
        public IEnumerable<HospitalizationRequest> GetAll()
        {
            return _hospitalizationRequestService.GetAll();
        }
        public HospitalizationRequest GetById(long id)
        {
            return _hospitalizationRequestService.Get(id);
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
