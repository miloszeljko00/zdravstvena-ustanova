using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Controller
{
    public class HolidayRequestController
    {
        private readonly HolidayRequestService _holidayRequestService;

        public HolidayRequestController(HolidayRequestService holidayRequestService)
        {
            _holidayRequestService = holidayRequestService;
        }

        public IEnumerable<HolidayRequest> GetAll()
        {
            return _holidayRequestService.GetAll();
        }
        public HolidayRequest GetById(IEnumerable<HolidayRequest> holidayRequests, long id)
        {
            return _holidayRequestService.FindHolidayRequestById(holidayRequests, id);
        }
        public HolidayRequest Create(HolidayRequest holidayRequest)
        {
            return _holidayRequestService.Create(holidayRequest);
        }
        public bool Update(HolidayRequest holidayRequest)
        {
            return _holidayRequestService.Update(holidayRequest);
        }
        public bool Delete(long holidayRequestId)
        {
            return _holidayRequestService.Delete(holidayRequestId);
        }
    }
}
