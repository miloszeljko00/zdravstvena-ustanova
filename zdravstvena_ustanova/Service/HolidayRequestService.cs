using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using zdravstvena_ustanova.Repository;
using System.Linq;

namespace zdravstvena_ustanova.Service
{
    public class HolidayRequestService
    {
        private readonly HolidayRequestRepository _holidayRequestRepository;

        public HolidayRequestService(HolidayRequestRepository holidayRequestRepository)
        {
            _holidayRequestRepository = holidayRequestRepository;
        }

        public IEnumerable<HolidayRequest> GetAll()
        {
            return _holidayRequestRepository.GetAll();
        }

        public HolidayRequest FindHolidayRequestById(IEnumerable<HolidayRequest> holidayRequests, long id)
        {
            return holidayRequests.SingleOrDefault(holidayRequest => holidayRequest.Id == id);
        }

        public HolidayRequest Create(HolidayRequest holidayRequest)
        {
            return _holidayRequestRepository.Create(holidayRequest);
        }
        public bool Update(HolidayRequest holidayRequest)
        {
            return _holidayRequestRepository.Update(holidayRequest);
        }
        public bool Delete(long holidayRequestId)
        {
            return _holidayRequestRepository.Delete(holidayRequestId);
        }
    }
}