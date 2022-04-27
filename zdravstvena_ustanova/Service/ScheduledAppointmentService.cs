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
        private PatientRepository _patientRepository;
        private DoctorRepository _doctorRepository;
        private RoomRepository _roomRepository;
        private AccountRepository _accountRepository;

        public ScheduledAppointmentService(ScheduledAppointmentRepository scheduledAppointmentRepository, RoomRepository roomRepository,
            DoctorRepository doctorRepository, PatientRepository patientRepository, AccountRepository accountRepository)
        {
            _scheduledAppointmentRepository = scheduledAppointmentRepository;
            _roomRepository = roomRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _accountRepository = accountRepository;
        }

        public IEnumerable<ScheduledAppointment> GetAll()
        {
            var patients = _patientRepository.GetAll();
            var doctors = _doctorRepository.GetAll();
            var rooms = _roomRepository.GetAll();
            var accounts = _accountRepository.GetAll();
            var scheduledAppointmets = _scheduledAppointmentRepository.GetAll();
            BindPatientDoctorRoomWithScheduledAppointments(patients, doctors, rooms, scheduledAppointmets, accounts);
            return scheduledAppointmets;
        }
        public ScheduledAppointment GetById(long Id)
        {
            var patients = _patientRepository.GetAll();
            var doctors = _doctorRepository.GetAll();
            var rooms = _roomRepository.GetAll();
            var accounts = _accountRepository.GetAll();
            var scheduledAppointmet = _scheduledAppointmentRepository.Get(Id);
            BindPatientDoctorRoomWithScheduledAppointment(patients, doctors, rooms, scheduledAppointmet, accounts);
            return scheduledAppointmet;
        }
        public IEnumerable<ScheduledAppointment> GetFromToDates(DateTime start, DateTime end)
        {
            var listOfAppointments = GetAll();
            var listOfCorrectAppointments = new List<ScheduledAppointment>();
            foreach (ScheduledAppointment sa in listOfAppointments)
            {
                if (sa.Start >= start && sa.Start <= end)
                {
                    listOfCorrectAppointments.Add(sa);
                }
            }

            return listOfCorrectAppointments;
        }
        public IEnumerable<ScheduledAppointment> GetFromToDatesForRoom(DateTime start, DateTime end, long roomId)
        {
            var listOfAppointments = GetAll();
            var listOfCorrectAppointments = new List<ScheduledAppointment>();
            foreach (ScheduledAppointment sa in listOfAppointments)
            {
                if (sa.Start >= start && sa.Start <= end)
                {
                    if(sa.Room.Id == roomId) listOfCorrectAppointments.Add(sa);
                }
            }

            return listOfCorrectAppointments;
        }
        private void BindPatientDoctorRoomWithScheduledAppointment(IEnumerable<Patient> patients, IEnumerable<Doctor> doctors,
            IEnumerable<Room> rooms, ScheduledAppointment scheduledAppointment, IEnumerable<Account> accounts)
        {
            scheduledAppointment.Patient = FindPatientById(patients, scheduledAppointment.Patient.Id);
            BindPatientWithAccount(accounts, scheduledAppointment.Patient);
            scheduledAppointment.Doctor = FindDoctorById(doctors, scheduledAppointment.Doctor.Id);
            BindDoctorWithRoom(rooms, scheduledAppointment.Doctor);
            BindDoctorWithAccount(accounts, scheduledAppointment.Doctor);
            scheduledAppointment.Room = FindRoomById(rooms, scheduledAppointment.Room.Id);

        }

        private void BindPatientDoctorRoomWithScheduledAppointments(IEnumerable<Patient> patients, IEnumerable<Doctor> doctors,
            IEnumerable<Room> rooms, IEnumerable<ScheduledAppointment> scheduledAppointments, IEnumerable<Account> accounts)
        {
            scheduledAppointments.ToList().ForEach(scheduledAppointment =>
            {
                BindPatientDoctorRoomWithScheduledAppointment(patients, doctors, rooms, scheduledAppointment, accounts);
            });
        }
        private void BindDoctorWithRoom(IEnumerable<Room> rooms, Doctor doctor)
        {
            doctor.Room = FindRoomById(rooms, doctor.Room.Id);
        }
        private void BindDoctorWithAccount(IEnumerable<Account> accounts, Doctor doctor)
        {
            doctor.Account = FindAccountById(accounts, doctor.Account.Id);
            doctor.Account.Person = doctor;
        }
        private void BindPatientWithAccount(IEnumerable<Account> accounts, Patient patient)
        {
            patient.Account = FindAccountById(accounts, patient.Account.Id);
            patient.Account.Person = patient;
        }

        private Account FindAccountById(IEnumerable<Account> accounts, long accountId)
        {
            return accounts.SingleOrDefault(account => account.Id == accountId);
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