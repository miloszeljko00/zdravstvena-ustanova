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
        private readonly DoctorRepository _doctorRepository;

        public HolidayRequestService(HolidayRequestRepository holidayRequestRepository, DoctorRepository doctorRepository)
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
        public HolidayRequest GetById(long id)
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