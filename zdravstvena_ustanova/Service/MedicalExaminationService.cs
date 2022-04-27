using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using zdravstvena_ustanova.Model;
using Repository;
using zdravstvena_ustanova.Repository;

namespace zdravstvena_ustanova.Service
{
    public class MedicalExaminationService
    {
        private readonly MedicalExaminationRepository _medicalExaminationRepository;
        private readonly ScheduledAppointmentRepository _scheduledAppointmentRepository;
        private readonly AnamnesisRepository _anamnesisRepository;
        private readonly SpecialistRequestRepository _specialistRequestRepository;
        private readonly LabAnalysisRequestRepository _labAnalysisRequestRepository;
        private readonly HospitalizationRequestRepository _hospitalizationRequestRepository;
        private readonly PrescribedMedicineRepository _prescribedMedicineRepository;
        private readonly DoctorRepository _doctorRepository;
        private readonly PatientRepository _patientRepository;
        private readonly RoomRepository _roomRepository;
        private readonly AccountRepository _accountRepository;
        private readonly SpecialtyRepository _specialtyRepository;
        private readonly LabAnalysisComponentRepository _labAnalysisComponentRepository;
        private readonly MedicationRepository _medicationRepository;
        private readonly IngredientRepository _ingredientRepository;

        public MedicalExaminationService(MedicalExaminationRepository medicalExaminationRepository, ScheduledAppointmentRepository scheduledAppointmentRepository,
            AnamnesisRepository anamnesisRepository, SpecialistRequestRepository specialistRequestRepository,
            LabAnalysisRequestRepository labAnalysisRequestRepository, HospitalizationRequestRepository hospitalizationRequestRepository,
            PrescribedMedicineRepository prescribedMedicineRepository, DoctorRepository doctorRepository, PatientRepository patientRepository,
            RoomRepository roomRepository, AccountRepository accountRepository, SpecialtyRepository specialtyRepository, LabAnalysisComponentRepository labAnalysisComponentRepository,
            MedicationRepository medicationRepository, IngredientRepository ingredientRepository)
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
            foreach(ScheduledAppointment sa in scheduledAppointments)
            {
                if(medicalExamination.ScheduledAppointment.Id==sa.Id)
                {
                    medicalExamination.ScheduledAppointment=sa;
                    break;
                }
            }

            foreach(Doctor d in doctors)
            {
                if (medicalExamination.ScheduledAppointment.Doctor.Id == d.Id)
                {
                    medicalExamination.ScheduledAppointment.Doctor = d;
                    foreach(Account a in accounts)
                    {
                        if(medicalExamination.ScheduledAppointment.Doctor.Account.Id==a.Id)
                        {
                            medicalExamination.ScheduledAppointment.Doctor.Account = a;
                            break;
                        }
                    }
                    break;
                }
            }
            foreach(Patient p in patients)
            {
                if(medicalExamination.ScheduledAppointment.Patient.Id==p.Id)
                {
                    medicalExamination.ScheduledAppointment.Patient = p;
                    foreach(Account a in accounts)
                    {
                        if(medicalExamination.ScheduledAppointment.Patient.Account.Id==a.Id)
                        {
                            medicalExamination.ScheduledAppointment.Patient.Account = a;
                            break;
                        }
                    }
                    break;
                }
            }
            foreach(Room r in rooms)
            {
                if(medicalExamination.ScheduledAppointment.Room.Id==r.Id)
                {
                    medicalExamination.ScheduledAppointment.Room = r;
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
                pm4.Medication.Ingredients =new List<Ingredient>(ingredientsBinded);
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

    }
}
