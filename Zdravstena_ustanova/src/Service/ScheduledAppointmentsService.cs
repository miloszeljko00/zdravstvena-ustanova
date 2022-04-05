using Model;
using System;
using System.Collections.Generic;

namespace Service
{
    public class ScheduledAppointmentsService
    {
        public bool Create(Model.ScheduledAppointment appointment)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int appointmentId)
        {
            throw new NotImplementedException();
        }

        public ScheduledAppointment GetById(string id)
        {
            throw new NotImplementedException();
        }

        public List<ScheduledAppointment> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<ScheduledAppointment> GetAllByDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public List<ScheduledAppointment> GetAllByPatient(Patient patient)
        {
            throw new NotImplementedException();
        }

        public Repository.ScheduledAppointmentsRepository scheduledAppointmentsRepository;

    }
}