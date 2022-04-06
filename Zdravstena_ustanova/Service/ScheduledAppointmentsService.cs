using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class ScheduledAppointmentsService
    {
        public List<ScheduledAppointment> ScheduledAppointments { get; set; }
        public ScheduledAppointmentsRepository scheduledAppointmentsRepository;

        public ScheduledAppointmentsService()
        {
            ScheduledAppointments = new List<ScheduledAppointment>();
            scheduledAppointmentsRepository = new ScheduledAppointmentsRepository();
        }

        public bool Create(Model.ScheduledAppointment appointment)
        {
            try
            {
                ScheduledAppointments.Add(appointment);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int appointmentId)
        {
            try {
                foreach (ScheduledAppointment appointment in ScheduledAppointments)
                {
                    if (appointment.AppointmentId == appointmentId)
                    {
                        return ScheduledAppointments.Remove(appointment);
                    }
                }
            }
            catch(Exception ex)
            {
                return false;
            }
            return false;
        }

        public ScheduledAppointment GetById(int id)
        {
            try
            {
                foreach (ScheduledAppointment appointment in ScheduledAppointments)
                {
                    if (appointment.AppointmentId == id)
                    {
                        return appointment;
                    }
                }
            }
            catch(Exception ex)
            {
                return null;
            }
            return null;
        }

        public List<ScheduledAppointment> GetAll()
        {
            return ScheduledAppointments;
        }

        public List<ScheduledAppointment> GetAllByDoctor(Doctor doctor)
        {
            try
            {
                List<ScheduledAppointment> newList = new List<ScheduledAppointment>();
                foreach (ScheduledAppointment appointment in ScheduledAppointments)
                {
                    if (appointment.Doctor == doctor)
                    {
                        newList.Add(appointment);
                    }
                }
                return newList;
            } 
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<ScheduledAppointment> GetAllByPatient(Patient patient)
        {
            try
            {
                List<ScheduledAppointment> newList = new List<ScheduledAppointment>();
                foreach (ScheduledAppointment appointment in ScheduledAppointments)
                {
                    if (appointment.Patient == patient)
                    {
                        newList.Add(appointment);
                    }
                }
                return newList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool Save()
        {
            return scheduledAppointmentsRepository.Save(ScheduledAppointments);
        }

        public bool Read()
        {
            try
            {
                ScheduledAppointments = scheduledAppointmentsRepository.Read();

                if (ScheduledAppointments == null)
                    return false;

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }
}