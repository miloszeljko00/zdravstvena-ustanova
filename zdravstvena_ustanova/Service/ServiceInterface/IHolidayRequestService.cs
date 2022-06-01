using System.Collections.Generic;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Service.ServiceInterface;

public interface IHolidayRequestService : IService<HolidayRequest>
{
    HolidayRequest FindHolidayRequestById(IEnumerable<HolidayRequest> holidayRequests, long id);
}