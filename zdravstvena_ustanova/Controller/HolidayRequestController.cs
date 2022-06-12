using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;
using System.Windows;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Controller
{
    public class HolidayRequestController
    {
        private readonly IHolidayRequestService _holidayRequestService;

        public HolidayRequestController(IHolidayRequestService holidayRequestService)
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
        public bool ValidateDateFromHolidayRequestForm(DateTime? startDate, DateTime? endDate)
        {
            bool returnValue = true;
            if (startDate == null || endDate == null)
            {
                MessageBox.Show("You must enter time interval!");
                returnValue = false;
            }
            else if (endDate <= startDate)
            {
                MessageBox.Show("You must enter valid date!");
                returnValue = false;
            }
            else if (startDate <= DateTime.Now)
            {
                MessageBox.Show("You cant select past date!");
                returnValue = false;
            }
            else if (startDate <= DateTime.Now.AddDays(2))
            { 
                MessageBox.Show("You have to request at least 3 days earlier!");
                returnValue = false;    
            }          
            return returnValue;
        }
        public bool CheckIfSomeOtherSpecialistHasRequestAtTheTime(bool isUrgent, Doctor myDoctor, DateTime? startDate, DateTime? endDate)
        {
            bool returnValue = true;
            if (!isUrgent)
            {
                List<HolidayRequest> holidayRequestsFromBase = GetAll().ToList();
                foreach (HolidayRequest hr in holidayRequestsFromBase)
                {
                    if (hr.Doctor.Specialty.Id == myDoctor.Specialty.Id)
                    {
                        if ((hr.StartDate <= startDate && startDate <= hr.EndDate) || (hr.StartDate <= endDate && endDate <= hr.EndDate) || (hr.StartDate <= startDate && endDate <= hr.EndDate) || (startDate <= hr.StartDate && hr.EndDate <= endDate))
                        {
                            if (hr.HolidayRequestStatus == zdravstvena_ustanova.Model.Enums.HolidayRequestStatus.ONHOLD || hr.HolidayRequestStatus == zdravstvena_ustanova.Model.Enums.HolidayRequestStatus.ACCEPTED)
                            {
                                MessageBox.Show("Unfortunately right now you cant request for holiday. " +
                               "College of your specialty has alredy requested holiday.");
                                returnValue = false;
                            }
                        }
                    }
                }
            }
            return returnValue;
        }
    }
}
