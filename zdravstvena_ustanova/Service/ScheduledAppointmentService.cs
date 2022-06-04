using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;
using zdravstvena_ustanova.Model.Enums;

namespace zdravstvena_ustanova.Service
{
    public class ScheduledAppointmentService : IScheduledAppointmentService
    {
        private readonly IScheduledAppointmentRepository _scheduledAppointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IAccountRepository _accountRepository;

        public ScheduledAppointmentService(IScheduledAppointmentRepository scheduledAppointmentRepository, IRoomRepository roomRepository,
            IDoctorRepository doctorRepository, IPatientRepository patientRepository, IAccountRepository accountRepository)
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
            var scheduledAppointments = _scheduledAppointmentRepository.GetAll();
            BindPatientDoctorRoomWithScheduledAppointments(patients, doctors, rooms, scheduledAppointments, accounts);
            return scheduledAppointments;
        }

        public IEnumerable<ScheduledAppointment> GetScheduledAppointmentsForPatient(long patientId)
        {
            var scheduledAppointments = GetAll();
            List<ScheduledAppointment> patientsAppointments = new List<ScheduledAppointment>();
            foreach(ScheduledAppointment sa in scheduledAppointments)
                if(sa.Patient.Id == patientId)
                    patientsAppointments.Add(sa);
            return patientsAppointments;
        }

        public ScheduledAppointment GetScheduledAppointmentsForDate(DateTime date, long patientId)
        {
            var scheduledAppointments = GetScheduledAppointmentsForPatient(patientId);
            ScheduledAppointment resultAppointment = null;
            foreach (ScheduledAppointment sa in scheduledAppointments)
            {
                if (sa.Start.Date.Equals(date))
                {
                    resultAppointment = sa;
                    break;
                }
            }
            return resultAppointment;
        }

        public IEnumerable<ScheduledAppointment> GetAllUnbound()
        { 
            return _scheduledAppointmentRepository.GetAll();
            
        }
        public ScheduledAppointment Get(long Id)
        {
            var patients = _patientRepository.GetAll();
            var doctors = _doctorRepository.GetAll();
            var rooms = _roomRepository.GetAll();
            var accounts = _accountRepository.GetAll();
            var scheduledAppointment = _scheduledAppointmentRepository.Get(Id);
            BindPatientDoctorRoomWithScheduledAppointment(patients, doctors, rooms, scheduledAppointment, accounts);
            return scheduledAppointment;
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
        public bool Update(ScheduledAppointment scheduledAppointment)
        {
            return _scheduledAppointmentRepository.Update(scheduledAppointment);
        }
        public bool Delete(long scheduledAppointmentId)
        {
            return _scheduledAppointmentRepository.Delete(scheduledAppointmentId);
        }
        public string[] GetAllAppointmentsAsStringArray()
        {
            return _scheduledAppointmentRepository.GetAllAppointmentsAsStringArray();
        }
        public IEnumerable<Account> GetBusyDoctors(Meeting meeting)
        {
            List<Account> busyDoctors = new List<Account>();
            var _scheduledAppointments = GetForSpecificTime(meeting.Time);
            foreach(Account a in meeting.Participants)
            {
                if (a.AccountType != AccountType.DOCTOR)
                    continue;
                if(IsBusyDoctor((Doctor)a.Person, _scheduledAppointments))
                    busyDoctors.Add(a);
            }
            return busyDoctors;
        }

        private bool IsBusyDoctor(Doctor doctor, IEnumerable<ScheduledAppointment> scheduledAppointments)
        {
            bool ret = false;
            foreach(ScheduledAppointment sa in scheduledAppointments)
            {
                if(sa.Doctor.Id == doctor.Id)
                {
                    ret = true;
                    break;
                }
            }
            return ret;
        }

        private IEnumerable<ScheduledAppointment> GetForSpecificTime(DateTime start)
        {
            var listOfAppointments = GetAll();
            var listOfCorrectAppointments = new List<ScheduledAppointment>();
            foreach (ScheduledAppointment sa in listOfAppointments)
            {
                if (sa.Start == start)
                {
                    listOfCorrectAppointments.Add(sa);
                }
            }

            return listOfCorrectAppointments;
        }
    }
}