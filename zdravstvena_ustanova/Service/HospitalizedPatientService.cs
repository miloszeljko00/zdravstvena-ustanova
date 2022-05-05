using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdravstvena_ustanova.Service
{
    public class HospitalizedPatientService
    {
        private readonly HospitalizedPatientRepository _hospitalizedPatientRepository;
        private readonly PatientRepository _patientRepository;
        private readonly RoomRepository _roomRepository;
        private readonly AccountRepository _accountRepository;

        public HospitalizedPatientService(HospitalizedPatientRepository hospitalizedPatientRepository, PatientRepository patientRepository, RoomRepository roomRepository, AccountRepository accountRepository)
        {
            _hospitalizedPatientRepository = hospitalizedPatientRepository;
            _patientRepository = patientRepository;
            _roomRepository = roomRepository;
            _accountRepository = accountRepository;
        }

        public IEnumerable<HospitalizedPatient> GetAll()
        {
            var hospitalizedPatients = _hospitalizedPatientRepository.GetAll();
            var patients = _patientRepository.GetAll();
            var rooms = _roomRepository.GetAll();
            var accounts = _accountRepository.GetAll();
            BindPatientRoomWithHospitalizedPatients(hospitalizedPatients, patients, rooms, accounts);
            return hospitalizedPatients;
        }

        private void BindPatientRoomWithHospitalizedPatients(IEnumerable<HospitalizedPatient> hospitalizedPatients, IEnumerable<Patient> patients, IEnumerable<Room> rooms, IEnumerable<Account> accounts)
        {
            foreach(HospitalizedPatient hp in hospitalizedPatients)
            {
                BindPatientRoomWithHospitalizedPatient(hp, patients, rooms, accounts);
            }
        }

        public HospitalizedPatient GetById(long id)
        {
            var hospitalizedPatient = _hospitalizedPatientRepository.Get(id);
            var patients = _patientRepository.GetAll();
            var rooms = _roomRepository.GetAll();
            var accounts = _accountRepository.GetAll();
            BindPatientRoomWithHospitalizedPatient(hospitalizedPatient, patients, rooms, accounts);
            return hospitalizedPatient;
        }

        private void BindPatientRoomWithHospitalizedPatient(HospitalizedPatient hospitalizedPatient, IEnumerable<Patient> patients, IEnumerable<Room> rooms, IEnumerable<Account> accounts)
        {
            foreach(Patient p in patients)
            {
                if(p.Id==hospitalizedPatient.Patient.Id)
                {
                    hospitalizedPatient.Patient=p;
                    break;
                }
            }
            foreach(Account a in accounts)
            {
                if(a.Id==hospitalizedPatient.Patient.Account.Id)
                {
                    hospitalizedPatient.Patient.Account = a;
                }
            }
            foreach(Room r in rooms)
            {
                if(r.Id==hospitalizedPatient.Room.Id)
                {
                    hospitalizedPatient.Room = r;
                }
            }
            
        }

        public HospitalizedPatient Create(HospitalizedPatient hospitalizedPatient)
        {
            return _hospitalizedPatientRepository.Create(hospitalizedPatient);
        }
        public bool Update(HospitalizedPatient hospitalizedPatient)
        {
            return _hospitalizedPatientRepository.Update(hospitalizedPatient);
        }
        public bool Delete(long hospitalizedPatientId)
        {
            return _hospitalizedPatientRepository.Delete(hospitalizedPatientId);
        }
    }
}
