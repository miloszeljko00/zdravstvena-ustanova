using System;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Controller
{
   public class ScheduledAppointmentController
   {
        private readonly IScheduledAppointmentService _scheduledAppointmentService;

        public ScheduledAppointmentController(IScheduledAppointmentService scheduledAppointmentService)
        {
            _scheduledAppointmentService = scheduledAppointmentService;
        }

        public IEnumerable<ScheduledAppointment> GetAll()
        {
            return _scheduledAppointmentService.GetAll();
        }
        public IEnumerable<ScheduledAppointment> GetAllUnbound()
        {
            return _scheduledAppointmentService.GetAllUnbound();
        }

        public ScheduledAppointment GetScheduledAppointmentsForDate(DateTime date, long patientId)
        {
            return _scheduledAppointmentService.GetScheduledAppointmentsForDate(date, patientId);
        }

        public IEnumerable<ScheduledAppointment> GetScheduledAppointmentsForPatient(long patientId)
        {
            return _scheduledAppointmentService.GetScheduledAppointmentsForPatient(patientId);
        }

        public IEnumerable<ScheduledAppointment> GetFromToDates(DateTime start, DateTime end)
        {
            return _scheduledAppointmentService.GetFromToDates(start, end);
        }
        public IEnumerable<ScheduledAppointment> GetFromToDatesForRoom(DateTime start, DateTime end, long roomId)
        {
            return _scheduledAppointmentService.GetFromToDatesForRoom(start, end, roomId);
        }
        public ScheduledAppointment GetById(long Id)
        {
            return _scheduledAppointmentService.Get(Id);
        }

        public ScheduledAppointment Create(ScheduledAppointment scheduledAppointment)
        {
            return _scheduledAppointmentService.Create(scheduledAppointment);
        }
        public void Update(ScheduledAppointment scheduledAppointment)
        {
            _scheduledAppointmentService.Update(scheduledAppointment);
        }
        public void Delete(long scheduledAppointmentId)
        {
            _scheduledAppointmentService.Delete(scheduledAppointmentId);
        }

        public string[] GetAllAppointmentsAsStringArray()
        {
            return _scheduledAppointmentService.GetAllAppointmentsAsStringArray();
        }
        public bool ValidateTime(DateTime selectedDate)
        {
            if(selectedDate<DateTime.Now)
            {
                MessageBox.Show("Ne mozete zakazivati termine u proslost!");
                return false;
            }
            return true;
        }
        public bool ValidateForm(string selectedTime, DateTime selectedDate, Patient selectedPatient, string selectedTypeOfAnAppointment, string selectedTimeOfAnAppointment)
        {
            if (selectedDate < DateTime.Now)
            {
                MessageBox.Show("Ne mozete zakazivati termine u proslost!");
                return false;
            }
            else if (selectedPatient == null || selectedTypeOfAnAppointment == null || selectedTimeOfAnAppointment == null || selectedTypeOfAnAppointment == "" || selectedTimeOfAnAppointment == "")
            {
                MessageBox.Show("Morate odabrati sve podatke!");
                return false;
            }
            return true;
        }
        public bool ValidateFormForSpecialistAppointment(string specialty, string doctor, string time, DateTime? date)
        {
            if(specialty == "" || doctor == "" || time=="" || date == null)
            {
                MessageBox.Show("Morate odabrati sve podatke(specijalnost, doktora, vreme i datum pregleda...)!");
                return false;
            }
            return true;
        }

        public IEnumerable<Account> GetBusyDoctors(Meeting meeting)
        {
            return _scheduledAppointmentService.GetBusyDoctors(meeting);
        }

        public IEnumerable<string> GetPossibleHoursForNewAppointment(DateTime dateTime, Doctor doctor, Patient patient, Room room)
        {
            return _scheduledAppointmentService.GetPossibleHoursForNewAppointment(dateTime, doctor, patient, room);
        }

        public DateTime FindFirstFreeAppointmentTime(ScheduledAppointment scheduledAppointment, DateTime today)
        {
            return _scheduledAppointmentService.FindFirstFreeAppointmentTime(scheduledAppointment, today);
        }

        public IEnumerable<ScheduledAppointment> GetFromToDatesForDoctor(DateTime start, DateTime end, long doctorId)
        {
            return _scheduledAppointmentService.GetFromToDatesForDoctor(start, end, doctorId);
        }
    }
}