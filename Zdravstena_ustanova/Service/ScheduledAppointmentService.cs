using Model;
using System;
using System.Collections.Generic;
using Repository;
using System.Linq;

namespace Service
{
    public class ScheduledAppointmentService
    {
        private readonly ScheduledAppointmentRepository _scheduledAppointmentRepository;
        private PatientService _patientService;
        private DoctorService _doctorService;
        private RoomService _roomService;

        public ScheduledAppointmentService(ScheduledAppointmentRepository scheduledAppointmentRepository, RoomService roomService, DoctorService doctorService, PatientService patientService)
        {
            _scheduledAppointmentRepository = scheduledAppointmentRepository;
            _roomService = roomService;
            _doctorService = doctorService;
            _patientService = patientService;
        }

        internal IEnumerable<ScheduledAppointment> GetAll()
        {
            var patients = _patientService.GetAll();
            var doctors = _doctorService.GetAll();
            var rooms = _roomService.GetAll();
            var scheduledAppointmets = _scheduledAppointmentRepository.GetAll();
            BindPatientDoctorRoomWithScheduledAppointments(patients, doctors, rooms, scheduledAppointmets);
            return scheduledAppointmets;
        }

        private void BindPatientDoctorRoomWithScheduledAppointments(IEnumerable<Patient> patients, IEnumerable<Doctor> doctors, IEnumerable<Room> rooms, IEnumerable<ScheduledAppointment> scheduledAppointments)
        {
            scheduledAppointments.ToList().ForEach(scheduledAppointment =>
            {
                scheduledAppointment.Patient = FindPatientById(patients, scheduledAppointment.PatientId);
                scheduledAppointment.Doctor = FindDoctorById(doctors, scheduledAppointment.DoctorId);
                scheduledAppointment.Room = FindRoomById(rooms, scheduledAppointment.RoomId);
            });
        }

        private Patient FindPatientById(IEnumerable<Patient> patients, long patientId)
        {
            return patients.SingleOrDefault(patient => patient.Id == patientId);
        }
        private Doctor FindDoctorById(IEnumerable<Doctor> doctors, long doctorId)
        {
            return doctors.SingleOrDefault(doctor => doctor.Id == doctorId);
        }
        private Room FindRoomById(IEnumerable<Room> rooms, long roomId)
        {
            return rooms.SingleOrDefault(room => room.Id == roomId);
        }

        public ScheduledAppointment Create(ScheduledAppointment scheduledAppointment)
        {
            return _scheduledAppointmentRepository.Create(scheduledAppointment);
        }
        public void Update(ScheduledAppointment scheduledAppointment)
        {
            _scheduledAppointmentRepository.Update(scheduledAppointment);
        }
        public void Delete(long scheduledAppointmentId)
        {
            _scheduledAppointmentRepository.Delete(scheduledAppointmentId);
        }
    }
}