using zdravstvena_ustanova.Model;
using System.Collections.Generic;
using System.Linq;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class HolidayRequestService : IHolidayRequestService
    {
        private readonly IHolidayRequestRepository _holidayRequestRepository;
        private readonly IDoctorRepository _doctorRepository;

        public HolidayRequestService(IHolidayRequestRepository holidayRequestRepository, IDoctorRepository doctorRepository)
        {
            _holidayRequestRepository = holidayRequestRepository;
            _doctorRepository = doctorRepository;
        }

        public IEnumerable<HolidayRequest> GetAll()
        {
            var holidayRequests = _holidayRequestRepository.GetAll();
            var doctors = _doctorRepository.GetAll();
            BindHolidayRequestsWithDoctors(holidayRequests, doctors);
            return holidayRequests;
        }
        public HolidayRequest Get(long id)
        {
            var holidayRequests = GetAll();
            HolidayRequest holidayRequest = FindHolidayRequestById(holidayRequests, id);
            return holidayRequest;
        }

        private void BindHolidayRequestsWithDoctors(IEnumerable<HolidayRequest> holidayRequests, IEnumerable<Doctor> doctors)
        {
            foreach(HolidayRequest hr in holidayRequests)
            {
                BindHolidayRequestWithDoctor(hr, doctors);
            }
        }

        public void BindHolidayRequestWithDoctor(HolidayRequest holidayRequest, IEnumerable<Doctor> doctors)
        {
            foreach (Doctor d in doctors)
            {
                if (d.Id == holidayRequest.Doctor.Id)
                {
                    holidayRequest.Doctor = d;
                }
            }
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