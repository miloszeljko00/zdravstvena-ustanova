using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Repository;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using zdravstvena_ustanova.Repository.RepositoryInterface;

namespace zdravstvena_ustanova.Service
{
    public class MedicationApprovalRequestService
    {
        private readonly IMedicationApprovalRequestRepository _medicationApprovalRequestRepository;
        private readonly IMedicationRepository _medicationRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMedicationTypeRepository _medicationTypeRepository;
        private readonly IDoctorRepository _doctorRepository;


        public MedicationApprovalRequestService(IMedicationApprovalRequestRepository medicationApprovalRequestRepository,
            IMedicationRepository medicationRepository, IIngredientRepository ingredientRepository,
            IMedicationTypeRepository medicationTypeRepository, IDoctorRepository doctorRepository)
        {
            _medicationApprovalRequestRepository = medicationApprovalRequestRepository;
            _medicationRepository = medicationRepository;
            _ingredientRepository = ingredientRepository;
            _medicationTypeRepository = medicationTypeRepository;
            _doctorRepository = doctorRepository;
        }

        public IEnumerable<MedicationApprovalRequest> GetAll()
        {
            var medicationApprovalRequests = _medicationApprovalRequestRepository.GetAll();
            var doctors = _doctorRepository.GetAll();
            var medications = _medicationRepository.GetAll();
            var ingredients = _ingredientRepository.GetAll();
            var medicationTypes = _medicationTypeRepository.GetAll();
            BindMedicationsWithIngredients(medications, ingredients);
            BindMedicationsWithMedicationTypes(medications, medicationTypes);
            BindMedicationApprovalRequestsWithDoctors(medicationApprovalRequests, doctors);
            BindMedicationApprovalRequestsWithMedications(medicationApprovalRequests, medications);
            return medicationApprovalRequests;
        }

        public bool CheckIfAlreadyWaitingForApproval(Medication medication)
        {
            var medicationApprovalRequests = (List<MedicationApprovalRequest>)GetByRequestStatus(RequestStatus.WAITING_FOR_APPROVAL);
            foreach(var medicationApprovalRequest in medicationApprovalRequests)
            {
                if(medicationApprovalRequest.Medication.Id == medication.Id)
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<MedicationApprovalRequest> GetByRequestStatus(RequestStatus requestStatus)
        {
            var medicationApprovalRequests = GetAll();

            return (from medicationApprovalRequest in medicationApprovalRequests
                    where medicationApprovalRequest.RequestStatus == requestStatus
                    select medicationApprovalRequest).ToList();
        }

        public IEnumerable<MedicationApprovalRequest> GetAllByApprovingDoctorId(long doctorId)
        {
            var medicationApprovalRequests = GetAll();

            return ((List<MedicationApprovalRequest>)medicationApprovalRequests).FindAll(
                medicationApprovalRequest => medicationApprovalRequest.ApprovingDoctor.Id == doctorId);
        }

        private void BindMedicationApprovalRequestsWithMedications(IEnumerable<MedicationApprovalRequest> medicationApprovalRequests,
            IEnumerable<Medication> medications)
        {
            foreach (MedicationApprovalRequest medicationApprovalRequest in medicationApprovalRequests)
            {
                BindMedicationApprovalRequestWithMedications(medicationApprovalRequest, medications);
            }
        }

        private void BindMedicationApprovalRequestWithMedications(MedicationApprovalRequest medicationApprovalRequest,
            IEnumerable<Medication> medications)
        {
            medicationApprovalRequest.Medication = FindMedicationById(medications, medicationApprovalRequest.Medication.Id);
        }

        private Medication FindMedicationById(IEnumerable<Medication> medications, long medicationId)
        {
            return medications.SingleOrDefault(medication => medication.Id == medicationId);
        }

        private void BindMedicationApprovalRequestsWithDoctors(IEnumerable<MedicationApprovalRequest> medicationApprovalRequests, 
            IEnumerable<Doctor> doctors)
        {
            foreach (MedicationApprovalRequest medicationApprovalRequest in medicationApprovalRequests)
            {
                BindMedicationApprovalRequestWithDoctors(medicationApprovalRequest, doctors);
            }
        }
        private void BindMedicationApprovalRequestWithDoctors(MedicationApprovalRequest medicationApprovalRequest,
           IEnumerable<Doctor> doctors)
        {
            medicationApprovalRequest.ApprovingDoctor = FindDoctorById(doctors, medicationApprovalRequest.ApprovingDoctor.Id);
        }

        private Doctor FindDoctorById(IEnumerable<Doctor> doctors, long doctorId)
        {
            return doctors.SingleOrDefault(doctor => doctor.Id == doctorId);
        }

        private void BindMedicationsWithMedicationTypes(IEnumerable<Medication> medications, IEnumerable<MedicationType> medicationTypes)
        {
            foreach (Medication medication in medications)
            {
                BindMedicationWithMedicationTypes(medication, medicationTypes);
            }
        }

        private void BindMedicationWithMedicationTypes(Medication medication, IEnumerable<MedicationType> medicationTypes)
        {
            medication.MedicationType = FindMedicationTypeById(medicationTypes, medication.MedicationType.Id);
        }
        private MedicationType FindMedicationTypeById(IEnumerable<MedicationType> medicationTypes, long medicationTypeId)
        {
            return medicationTypes.SingleOrDefault(medicationType => medicationType.Id == medicationTypeId);
        }
        private void BindMedicationsWithIngredients(IEnumerable<Medication> medications, IEnumerable<Ingredient> ingredients)
        {
            foreach (Medication m in medications)
            {
                BindMedicationWithIngredients(m, ingredients);
            }
        }

        public MedicationApprovalRequest GetById(long id)
        {
            var medicationApprovalRequest = _medicationApprovalRequestRepository.Get(id);
            var doctors = _doctorRepository.GetAll();
            var medications = _medicationRepository.GetAll();
            var ingredients = _ingredientRepository.GetAll();
            var medicationTypes = _medicationTypeRepository.GetAll();
            BindMedicationsWithIngredients(medications, ingredients);
            BindMedicationsWithMedicationTypes(medications, medicationTypes);
            BindMedicationApprovalRequestWithDoctors(medicationApprovalRequest, doctors);
            BindMedicationApprovalRequestWithMedications(medicationApprovalRequest, medications);
            return medicationApprovalRequest;
        }

        private void BindMedicationWithIngredients(Medication medication, IEnumerable<Ingredient> ingredients)
        {
            List<Ingredient> ingredientsBinded = new List<Ingredient>();
            foreach (Ingredient i1 in medication.Ingredients)
            {
                foreach (Ingredient i2 in ingredients)
                {
                    if (i2.Id == i1.Id)
                    {
                        ingredientsBinded.Add(i2);
                        break;
                    }
                }
            }
            medication.Ingredients = ingredientsBinded;
        }

        public MedicationApprovalRequest Create(MedicationApprovalRequest medicationApprovalRequest)
        {
            return _medicationApprovalRequestRepository.Create(medicationApprovalRequest);
        }
        public bool Update(MedicationApprovalRequest medicationApprovalRequest)
        {
            return _medicationApprovalRequestRepository.Update(medicationApprovalRequest);
        }
        public bool Delete(long medicationApprovalRequestId)
        {
            return _medicationApprovalRequestRepository.Delete(medicationApprovalRequestId);
        }
    }
}
