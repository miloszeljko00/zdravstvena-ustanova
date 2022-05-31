using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Repository;
using zdravstvena_ustanova.Repository.RepositoryInterface;

namespace zdravstvena_ustanova.Service
{
    public class MedicalExaminationService
    {
        private readonly IMedicalExaminationRepository _medicalExaminationRepository;
        private readonly IScheduledAppointmentRepository _scheduledAppointmentRepository;
        private readonly IAnamnesisRepository _anamnesisRepository;
        private readonly ISpecialistRequestRepository _specialistRequestRepository;
        private readonly ILabAnalysisRequestRepository _labAnalysisRequestRepository;
        private readonly IHospitalizationRequestRepository _hospitalizationRequestRepository;
        private readonly IPrescribedMedicineRepository _prescribedMedicineRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly ILabAnalysisComponentRepository _labAnalysisComponentRepository;
        private readonly IMedicationRepository _medicationRepository;
        private readonly IIngredientRepository _ingredientRepository;

        public MedicalExaminationService(IMedicalExaminationRepository medicalExaminationRepository,
            IScheduledAppointmentRepository scheduledAppointmentRepository, IAnamnesisRepository anamnesisRepository,
            ISpecialistRequestRepository specialistRequestRepository, ILabAnalysisRequestRepository labAnalysisRequestRepository,
            IHospitalizationRequestRepository hospitalizationRequestRepository, IPrescribedMedicineRepository prescribedMedicineRepository,
            IDoctorRepository doctorRepository, IPatientRepository patientRepository, IRoomRepository roomRepository,
            IAccountRepository accountRepository, ISpecialtyRepository specialtyRepository,
            ILabAnalysisComponentRepository labAnalysisComponentRepository, IMedicationRepository medicationRepository,
            IIngredientRepository ingredientRepository)
        {
            _medicalExaminationRepository = medicalExaminationRepository;
            _scheduledAppointmentRepository = scheduledAppointmentRepository;
            _anamnesisRepository = anamnesisRepository;
            _specialistRequestRepository = specialistRequestRepository;
            _labAnalysisRequestRepository = labAnalysisRequestRepository;
            _hospitalizationRequestRepository = hospitalizationRequestRepository;
            _prescribedMedicineRepository = prescribedMedicineRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _roomRepository = roomRepository;
            _accountRepository = accountRepository;
            _specialtyRepository = specialtyRepository;
            _labAnalysisComponentRepository = labAnalysisComponentRepository;
            _medicationRepository = medicationRepository;
            _ingredientRepository = ingredientRepository;
        }

        public IEnumerable<MedicalExamination> GetAll()
        {
            var medicalExaminations = _medicalExaminationRepository.GetAll();
            var scheduledAppointments = _scheduledAppointmentRepository.GetAll();
            var anamnesiess = _anamnesisRepository.GetAll();
            var specialistRequests = _specialistRequestRepository.GetAll();
            var labAnalysisRequests = _labAnalysisRequestRepository.GetAll();
            var hospitalizationRequests = _hospitalizationRequestRepository.GetAll();
            var prescribedMedicines = _prescribedMedicineRepository.GetAll();
            var doctors = _doctorRepository.GetAll();
            var patients = _patientRepository.GetAll();
            var rooms = _roomRepository.GetAll();
            var accounts = _accountRepository.GetAll();
            var specialties = _specialtyRepository.GetAll();
            var components = _labAnalysisComponentRepository.GetAll();
            var medications = _medicationRepository.GetAll();
            var ingredients = _ingredientRepository.GetAll();
            BindScheduledAppointmentsWithMedicalExaminations(medicalExaminations, scheduledAppointments, doctors, patients, rooms, accounts);
            BindAnamnesissWithMedicalExaminations(medicalExaminations, anamnesiess);
            BindSpecialistRequestsWithMedicalExaminations(medicalExaminations, specialistRequests, specialties);
            BindLabAnalysisRequestsWithMedicalExaminations(medicalExaminations, labAnalysisRequests, components);
            BindHospitalizationRequestsWithMedicalExaminations(medicalExaminations, hospitalizationRequests);
            BindPrescribedMedicinesWithMedicalExaminations(medicalExaminations, prescribedMedicines, medications, ingredients);
            return medicalExaminations;
        }

        public ScheduledAppointment GetScheduledAppointmentForAnamnesis(long anamnesisId)
        {
            var medicalExamination = GetAll();
            ScheduledAppointment scheduledAppointment = null;
            foreach (MedicalExamination me in medicalExamination)
            {
                if (me.Anamnesis.Id == anamnesisId)
                {
                    scheduledAppointment = me.ScheduledAppointment;
                }
                break;
            }
            return scheduledAppointment;
        }

        public MedicalExamination GetById(long id)
        {
            var medicalExamination = _medicalExaminationRepository.Get(id);
            var scheduledAppointments = _scheduledAppointmentRepository.GetAll();
            var anamnesiess = _anamnesisRepository.GetAll();
            var specialistRequests = _specialistRequestRepository.GetAll();
            var labAnalysisRequests = _labAnalysisRequestRepository.GetAll();
            var hospitalizationRequests = _hospitalizationRequestRepository.GetAll();
            var prescribedMedicines = _prescribedMedicineRepository.GetAll();
            var doctors = _doctorRepository.GetAll();
            var patients = _patientRepository.GetAll();
            var rooms = _roomRepository.GetAll();
            var accounts = _accountRepository.GetAll();
            var specialties = _specialtyRepository.GetAll();
            var components = _labAnalysisComponentRepository.GetAll();
            var medications = _medicationRepository.GetAll();
            var ingredients = _ingredientRepository.GetAll();
            BindScheduledAppointmentWithMedicalExamination(medicalExamination, scheduledAppointments, doctors, patients, rooms, accounts);
            BindAnamnesisWithMedicalExamination(medicalExamination, anamnesiess);
            BindSpecialistRequestWithMedicalExamination(medicalExamination, specialistRequests, specialties);
            BindLabAnalysisRequestWithMedicalExamination(medicalExamination, labAnalysisRequests, components);
            BindHospitalizationRequestWithMedicalExamination(medicalExamination, hospitalizationRequests);
            BindPrescribedMedicinesWithMedicalExamination(medicalExamination, prescribedMedicines, medications, ingredients);
            return medicalExamination;
        }

        private void BindScheduledAppointmentWithMedicalExamination(MedicalExamination medicalExamination,
            IEnumerable<ScheduledAppointment> scheduledAppointments, IEnumerable<Doctor> doctors, IEnumerable<Patient> patients, IEnumerable<Room> rooms, IEnumerable<Account> accounts)
        {
            BindScheduledAppointmentWithMedicalExaminationById(medicalExamination, scheduledAppointments);
            BindDoctorWithScheduledAppointmentInCertainMedicalExamination(medicalExamination, doctors, accounts);
            BindPatientWithScheduledAppointmentInCertainMedicalExamination(medicalExamination, patients, accounts);
            BindRoomWithScheduledAppointmentInCertainMedicalExamination(medicalExamination, rooms);
        }

        private void BindRoomWithScheduledAppointmentInCertainMedicalExamination(MedicalExamination medicalExamination, IEnumerable<Room> rooms)
        {
            foreach (Room r in rooms)
            {
                if (medicalExamination.ScheduledAppointment.Room.Id == r.Id)
                {
                    medicalExamination.ScheduledAppointment.Room = r;
                    break;
                }
            }
        }

        private void BindPatientWithScheduledAppointmentInCertainMedicalExamination(MedicalExamination medicalExamination, IEnumerable<Patient> patients, IEnumerable<Account> accounts)
        {
            foreach (Patient p in patients)
            {
                if (medicalExamination.ScheduledAppointment.Patient.Id == p.Id)
                {
                    BindPatientWithAccount(p, accounts);
                    medicalExamination.ScheduledAppointment.Patient = p;
                    break;
                }
            }
        }

        private void BindPatientWithAccount(Patient p, IEnumerable<Account> accounts)
        {
            foreach (Account a in accounts)
            {
                if (p.Account.Id == a.Id)
                {
                    p.Account = a;
                    break;
                }
            }
        }

        private void BindDoctorWithScheduledAppointmentInCertainMedicalExamination(MedicalExamination medicalExamination, IEnumerable<Doctor> doctors, IEnumerable<Account> accounts)
        {
            foreach (Doctor d in doctors)
            {
                if (medicalExamination.ScheduledAppointment.Doctor.Id == d.Id)
                {
                    BindDoctorWithAccount(d, accounts);
                    medicalExamination.ScheduledAppointment.Doctor = d;
                    break;
                }
            }
        }

        private void BindDoctorWithAccount(Doctor d, IEnumerable<Account> accounts)
        {
            foreach (Account a in accounts)
            {
                if (d.Id == a.Id)
                {
                    d.Account = a;
                    break;
                }
            }
        }

        private void BindScheduledAppointmentWithMedicalExaminationById(MedicalExamination medicalExamination, IEnumerable<ScheduledAppointment> scheduledAppointments)
        {
            foreach (ScheduledAppointment sa in scheduledAppointments)
            {
                if (medicalExamination.ScheduledAppointment.Id == sa.Id)
                {
                    medicalExamination.ScheduledAppointment = sa;
                    break;
                }
            }
        }

        private void BindScheduledAppointmentsWithMedicalExaminations(IEnumerable<MedicalExamination> medicalExaminations,
           IEnumerable<ScheduledAppointment> scheduledAppointments, IEnumerable<Doctor> doctors, IEnumerable<Patient> patients, IEnumerable<Room> rooms, IEnumerable<Account> accounts)
        {
            foreach (MedicalExamination me in medicalExaminations)
            {
                BindScheduledAppointmentWithMedicalExamination(me, scheduledAppointments, doctors, patients, rooms, accounts);
            }
        }
        private void BindAnamnesisWithMedicalExamination(MedicalExamination medicalExamination, IEnumerable<Anamnesis> anamnesiess)
        {
            foreach (Anamnesis a in anamnesiess)
            {
                if (medicalExamination.Anamnesis.Id == a.Id)
                {
                    medicalExamination.Anamnesis = a;
                    break;
                }
            }
        }
        private void BindAnamnesissWithMedicalExaminations(IEnumerable<MedicalExamination> medicalExaminations, IEnumerable<Anamnesis> anamnesiess)
        {
            foreach (MedicalExamination me in medicalExaminations)
            {
                BindAnamnesisWithMedicalExamination(me, anamnesiess);
            }
        }
        private void BindSpecialistRequestWithMedicalExamination(MedicalExamination medicalExamination, IEnumerable<SpecialistRequest> specialistRequests, IEnumerable<Specialty> specialties)
        {
            foreach (SpecialistRequest sr in specialistRequests)
            {
                if (medicalExamination.SpecialistRequest.Id == sr.Id)
                {
                    medicalExamination.SpecialistRequest = sr;
                    foreach (Specialty s in specialties)
                    {
                        if (medicalExamination.SpecialistRequest.Specialty.Id == s.Id)
                        {
                            medicalExamination.SpecialistRequest.Specialty = s;
                            break;
                        }
                    }
                    break;
                }
            }
        }
        private void BindSpecialistRequestsWithMedicalExaminations(IEnumerable<MedicalExamination> medicalExaminations, IEnumerable<SpecialistRequest> specialistRequests, IEnumerable<Specialty> specialties)
        {
            foreach (MedicalExamination me in medicalExaminations)
            {
                BindSpecialistRequestWithMedicalExamination(me, specialistRequests, specialties);
            }
        }
        private void BindLabAnalysisRequestWithMedicalExamination(MedicalExamination medicalExamination, IEnumerable<LabAnalysisRequest> labAnalysisRequests, IEnumerable<LabAnalysisComponent> components)
        {
            foreach (LabAnalysisRequest lar in labAnalysisRequests)
            {
                if (medicalExamination.LabAnalysisRequest.Id == lar.Id)
                {
                    medicalExamination.LabAnalysisRequest = lar;
                    List<LabAnalysisComponent> componentsBinded = new List<LabAnalysisComponent>();
                    foreach (LabAnalysisComponent lac1 in medicalExamination.LabAnalysisRequest.LabAnalysisComponent)
                    {
                        foreach (LabAnalysisComponent lac2 in components)
                        {
                            if (lac1.Id == lac2.Id)
                            {
                                componentsBinded.Add(lac2);
                                break;
                            }
                        }
                    }
                    medicalExamination.LabAnalysisRequest.LabAnalysisComponent = componentsBinded;
                    break;
                }
            }
        }
        private void BindLabAnalysisRequestsWithMedicalExaminations(IEnumerable<MedicalExamination> medicalExaminations, IEnumerable<LabAnalysisRequest> labAnalysisRequests, IEnumerable<LabAnalysisComponent> components)
        {
            foreach (MedicalExamination me in medicalExaminations)
            {
                BindLabAnalysisRequestWithMedicalExamination(me, labAnalysisRequests, components);
            }
        }
        private void BindHospitalizationRequestWithMedicalExamination(MedicalExamination medicalExamination, IEnumerable<HospitalizationRequest> hospitalizationRequests)
        {
            foreach (HospitalizationRequest hr in hospitalizationRequests)
            {
                if (medicalExamination.HospitalizationRequest.Id == hr.Id)
                {
                    medicalExamination.HospitalizationRequest = hr;
                    break;
                }
            }
        }

        private void BindHospitalizationRequestsWithMedicalExaminations(IEnumerable<MedicalExamination> medicalExaminations, IEnumerable<HospitalizationRequest> hospitalizationRequests)
        {
            foreach (MedicalExamination me in medicalExaminations)
            {
                BindHospitalizationRequestWithMedicalExamination(me, hospitalizationRequests);
            }
        }
        private void BindPrescribedMedicinesWithMedicalExamination(MedicalExamination medicalExamination, IEnumerable<PrescribedMedicine> prescribedMedicines, IEnumerable<Medication> medications, IEnumerable<Ingredient> ingredients)
        {
            List<PrescribedMedicine> prescribedMedicinesBinded = new List<PrescribedMedicine>();
            foreach (PrescribedMedicine pm1 in medicalExamination.PrescribedMedicine)
            {
                foreach (PrescribedMedicine pm2 in prescribedMedicines)
                {
                    if (pm1.Id == pm2.Id)
                    {
                        prescribedMedicinesBinded.Add(pm2);
                        break;
                    }
                }
            }
            foreach (PrescribedMedicine pm3 in prescribedMedicinesBinded)
            {
                foreach (Medication m1 in medications)
                {
                    if (pm3.Medication.Id == m1.Id)
                    {
                        pm3.Medication = m1;
                        break;
                    }
                }
            }
            List<Ingredient> ingredientsBinded = new List<Ingredient>();
            foreach (PrescribedMedicine pm4 in prescribedMedicinesBinded)
            {
                foreach (Ingredient i1 in pm4.Medication.Ingredients)
                {
                    foreach (Ingredient i2 in ingredients)
                    {
                        if (i1.Id == i2.Id)
                        {
                            ingredientsBinded.Add(i2);
                            break;
                        }
                    }
                }
                pm4.Medication.Ingredients = new List<Ingredient>(ingredientsBinded);
                ingredientsBinded.Clear();
            }
            medicalExamination.PrescribedMedicine = prescribedMedicinesBinded;
        }
        private void BindPrescribedMedicinesWithMedicalExaminations(IEnumerable<MedicalExamination> medicalExaminations, IEnumerable<PrescribedMedicine> prescribedMedicines, IEnumerable<Medication> medications, IEnumerable<Ingredient> ingredients)
        {
            foreach (MedicalExamination me in medicalExaminations)
            {
                BindPrescribedMedicinesWithMedicalExamination(me, prescribedMedicines, medications, ingredients);
            }
        }
        public MedicalExamination Create(MedicalExamination medicalExamination)
        {
            return _medicalExaminationRepository.Create(medicalExamination);
        }
        public bool Update(MedicalExamination medicalExamination)
        {
            return _medicalExaminationRepository.Update(medicalExamination);
        }
        public bool Delete(long id)
        {
            return _medicalExaminationRepository.Delete(id);
        }
        public MedicalExamination FindByScheduledAppointmentId(long id)
        {
            var medicalExaminations = GetAll();
            foreach(MedicalExamination me in medicalExaminations)
            {
                if(me.ScheduledAppointment.Id==id)
                {
                    return me;
                }               
            }
            return null;
        }
    }
}
