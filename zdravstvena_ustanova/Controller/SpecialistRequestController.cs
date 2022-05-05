using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Service;

namespace zdravstvena_ustanova.Controller
{
    public class SpecialistRequestController
    {
        private readonly SpecialistRequestService _specialistRequestService;

        public SpecialistRequestController(SpecialistRequestService specialistRequestService)
        {
            _specialistRequestService = specialistRequestService;
        }
        public IEnumerable<SpecialistRequest> GetAll()
        {
            return _specialistRequestService.GetAll();
        }
        public SpecialistRequest GetById(long id)
        {
            return _specialistRequestService.GetById(id);
        }
        public SpecialistRequest Create(SpecialistRequest specialistRequest)
        {
            return _specialistRequestService.Create(specialistRequest);
        }
        public bool Update(SpecialistRequest specialistRequest)
        {
            return _specialistRequestService.Update(specialistRequest);
        }
        public bool Delete(long id)
        {
            return _specialistRequestService.Delete(id);
        }
    }
}
