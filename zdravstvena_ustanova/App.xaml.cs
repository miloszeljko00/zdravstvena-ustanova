using System;
using System.Windows;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Repository;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Controller;

namespace zdravstvena_ustanova
{
    public partial class App : Application
    {
        public static string ProjectPath { get; set; } = System.Reflection.Assembly.GetExecutingAssembly().Location
               .Split(new string[] { "bin" }, StringSplitOptions.None)[0];

        private const string CSV_DELIMITER = ";";

        private readonly string ITEM_FILE = ProjectPath + "\\Resources\\Data\\Items.csv";
        private readonly string ITEM_TYPE_FILE = ProjectPath + "\\Resources\\Data\\ItemTypes.csv";
        private readonly string STORED_ITEM_FILE = ProjectPath + "\\Resources\\Data\\StoredItems.csv";
        private readonly string ROOM_FILE = ProjectPath + "\\Resources\\Data\\Rooms.csv";
        private readonly string ROOM_UNDER_RENOVATION_FILE = ProjectPath + "\\Resources\\Data\\RoomsUnderRenovation.csv";        
        private readonly string WAREHOUSE_FILE = ProjectPath + "\\Resources\\Data\\Warehouses.csv";
        private readonly string RENOVATION_APPOINTMENT_FILE = ProjectPath + "\\Resources\\Data\\RenovationAppointments.csv";
        private readonly string RENOVATION_TYPE_FILE = ProjectPath + "\\Resources\\Data\\RenovationTypes.csv";
        private readonly string SCHEDULED_APPOINTMENT_FILE = ProjectPath + "\\Resources\\Data\\ScheduledAppointments.csv";
        private readonly string UNSCHEDULED_APPOINTMENT_FILE = ProjectPath + "\\Resources\\Data\\UnScheduledAppointments.csv";
        private readonly string SCHEDULED_ITEM_TRANSFER_FILE = ProjectPath + "\\Resources\\Data\\ScheduledItemTransfers.csv";
        private readonly string DOCTOR_FILE = ProjectPath + "\\Resources\\Data\\Doctors.csv";
        private readonly string PATIENT_FILE = ProjectPath + "\\Resources\\Data\\Patients.csv";
        private readonly string MANAGER_FILE = ProjectPath + "\\Resources\\Data\\Managers.csv";
        private readonly string SECRETARY_FILE = ProjectPath + "\\Resources\\Data\\Secretary.csv";
        private readonly string ACCOUNT_FILE = ProjectPath + "\\Resources\\Data\\Accounts.csv";
        private readonly string SPECIALTY_FILE = ProjectPath + "\\Resources\\Data\\Specialty.csv";
        private readonly string ANAMNESIS_FILE = ProjectPath + "\\Resources\\Data\\Anamnesis.csv";
        private readonly string MEDICATION_FILE = ProjectPath + "\\Resources\\Data\\Medications.csv";
        private readonly string INGREDIENT_FILE = ProjectPath + "\\Resources\\Data\\Ingredients.csv";
        private readonly string PRESCRIBED_MEDICINE_FILE = ProjectPath + "\\Resources\\Data\\PrescribedMedicine.csv";
        private readonly string ORDERED_ITEM_FILE = ProjectPath + "\\Resources\\Data\\OrderedItems.csv";
        
        private readonly string MEDICAL_EXAMINATION_FILE = ProjectPath + "\\Resources\\Data\\MedicalExamination.csv";
        private readonly string SPECIALIST_REQUEST_FILE = ProjectPath + "\\Resources\\Data\\SpecialistRequest.csv";
        private readonly string LAB_ANALYSIS_REQUEST_FILE = ProjectPath + "\\Resources\\Data\\LabAnalysisRequest.csv";
        private readonly string HOSPITALIZATION_REQUEST_FILE = ProjectPath + "\\Resources\\Data\\HospitalizationRequest.csv";
        private readonly string LAB_ANALYSIS_COMPONENT_FILE = ProjectPath + "\\Resources\\Data\\LabAnalysisComponent.csv";

        private readonly string HEALTH_RECORD_FILE = ProjectPath + "\\Resources\\Data\\HealthRecords.csv";
        private readonly string ALLERGEN_FILE = ProjectPath + "\\Resources\\Data\\Allergens.csv";

        private readonly string HOLIDAY_REQUEST_FILE = ProjectPath + "\\Resources\\Data\\HolidayRequest.csv";

        private readonly string MEDICATION_TYPE_FILE = ProjectPath + "\\Resources\\Data\\MedicationTypes.csv";
        private readonly string MEDICATION_APPROVAL_REQUEST_FILE = ProjectPath + "\\Resources\\Data\\MedicationApprovalRequests.csv";

        private readonly string SURVEY_QUESTIONS_FILE = ProjectPath + "\\Resources\\Data\\SurveyQuestions.csv";
        private readonly string SURVEY_ANSWERS_FILE = ProjectPath + "\\Resources\\Data\\SurveyAnswers.csv";
        private readonly string ANTI_TROLL_FILE = ProjectPath + "\\Resources\\Data\\AntiTrollMechanism.csv";

        private readonly string NOTE_FILE = ProjectPath + "\\Resources\\Data\\Note.csv";
        private readonly string MEETINGS_FILE = ProjectPath + "\\Resources\\Data\\Meetings.csv";


        public ItemController ItemController { get; set; }
        public ItemTypeController ItemTypeController { get; set; }
        public StoredItemController StoredItemController { get; set; }
        public RoomController RoomController { get; set; }
        public WarehouseController WarehouseController { get; set; }
        public RenovationAppointmentController RenovationAppointmentController { get; set; }
        public RenovationTypeController RenovationTypeController { get; set; }
        public ScheduledAppointmentController ScheduledAppointmentController { get; set; }
        public ScheduledAppointmentController UnScheduledAppointmentController { get; set; }
        public ScheduledItemTransferController ScheduledItemTransferController { get; set; }
        public DoctorController DoctorController { get; set; }
        public PatientController PatientController { get; set; }
        public AccountController AccountController { get; set; }
        public SecretaryController SecretaryController { get; set; }
        public ManagerController ManagerController { get; set; }
        public SpecialtyController SpecialtyController { get; set; }
        public AnamnesisController AnamnesisController { get; set; }
        public MedicationController MedicationController { get; set; }
        public MedicationTypeController MedicationTypeController { get; set; }
        public MedicationApprovalRequestController MedicationApprovalRequestController { get; set; }
        public IngredientController IngredientController { get; set; }
        public PrescribedMedicineController PrescribedMedicineController { get; set; }
        public MedicalExaminationController MedicalExaminationController { get; set; }
        public SpecialistRequestController SpecialistRequestController { get; set; }
        public LabAnalysisRequestController LabAnalysisRequestController { get; set; }
        public HospitalizationRequestController HospitalizationRequestController { get; set; }
        public LabAnalysisComponentController LabAnalysisComponentController { get; set; }

        public HealthRecordController HealthRecordController { get; set; }

        public AllergensController AllergensController { get; set; }

        public SystemController SystemController { get; set; }

        public HolidayRequestController HolidayRequestController { get; set; }

        public StoredItemController OrderedItemController { get; set; }

        public SurveyQuestionsController SurveyQuestionsController { get; set; }
        public SurveyAnswersController SurveyAnswersController { get; set; }
        public AntiTrollMechanismController AntiTrollMechanismController { get; set; }
        public NoteController NoteController { get; set; }

        public MeetingController MeetingController { get; set; }


        public Person? LoggedInUser { get; set; }

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Manager Manager { get; set; }
        public Secretary Secretary { get; set; }

        public App()
        {
            var itemRepository = new ItemRepository(ITEM_FILE, CSV_DELIMITER);
            var itemTypeRepository = new ItemTypeRepository(ITEM_TYPE_FILE, CSV_DELIMITER);
            var storedItemRepository = new StoredItemRepository(STORED_ITEM_FILE, CSV_DELIMITER);
            var roomRepository = new RoomRepository(ROOM_FILE, CSV_DELIMITER);
            var roomUnderRenovationRepository = new RoomRepository(ROOM_UNDER_RENOVATION_FILE, CSV_DELIMITER);
            var warehouseRepository = new WarehouseRepository(WAREHOUSE_FILE, CSV_DELIMITER);
            var renovationAppointmentRepository = new RenovationAppointmentRepository(RENOVATION_APPOINTMENT_FILE, CSV_DELIMITER);
            var renovationTypeRepository = new RenovationTypeRepository(RENOVATION_TYPE_FILE, CSV_DELIMITER);
            var scheduledAppointmentRepository = new ScheduledAppointmentRepository(SCHEDULED_APPOINTMENT_FILE, CSV_DELIMITER);
            var unScheduledAppointmentRepository = new ScheduledAppointmentRepository(UNSCHEDULED_APPOINTMENT_FILE, CSV_DELIMITER);
            var ScheduledItemTransferRepository = new ScheduledItemTransferRepository(SCHEDULED_ITEM_TRANSFER_FILE, CSV_DELIMITER);
            var doctorRepository = new DoctorRepository(DOCTOR_FILE, CSV_DELIMITER);
            var patientRepository = new PatientRepository(PATIENT_FILE, CSV_DELIMITER);
            var accountRepository = new AccountRepository(ACCOUNT_FILE, CSV_DELIMITER);
            var managerRepository = new ManagerRepository(MANAGER_FILE, CSV_DELIMITER);
            var secretaryRepository = new SecretaryRepository(SECRETARY_FILE, CSV_DELIMITER);
            var specialtyRepository = new SpecialtyRepository(SPECIALTY_FILE, CSV_DELIMITER);
            var anamnesisRepository = new AnamnesisRepository(ANAMNESIS_FILE, CSV_DELIMITER);

            var medicationRepository = new MedicationRepository(MEDICATION_FILE, CSV_DELIMITER);
            var ingredientRepository = new IngredientRepository(INGREDIENT_FILE, CSV_DELIMITER);
            var medicationTypeRepository = new MedicationTypeRepository(MEDICATION_TYPE_FILE, CSV_DELIMITER);
            var medicationApprovalRequestRepository = new MedicationApprovalRequestRepository(MEDICATION_APPROVAL_REQUEST_FILE, CSV_DELIMITER);

            var prescribedMedicineRepository = new PrescribedMedicineRepository(PRESCRIBED_MEDICINE_FILE, CSV_DELIMITER);

            var medicalExaminationRepository = new MedicalExaminationRepository(MEDICAL_EXAMINATION_FILE, CSV_DELIMITER);
            var specialistRequestRepository = new SpecialistRequestRepository(SPECIALIST_REQUEST_FILE, CSV_DELIMITER);
            var labAnalysisRequestRepository = new LabAnalysisRequestRepository(LAB_ANALYSIS_REQUEST_FILE, CSV_DELIMITER);
            var hospitalizationRequestRepository = new HospitalizationRequestRepository(HOSPITALIZATION_REQUEST_FILE, CSV_DELIMITER);
            var labAnalysisComponentRepository = new LabAnalysisComponentRepository(LAB_ANALYSIS_COMPONENT_FILE, CSV_DELIMITER);

            var healthRecordRepository = new HealthRecordRepository(HEALTH_RECORD_FILE, CSV_DELIMITER);
            var allergenRepository = new AllergensRepository(ALLERGEN_FILE, CSV_DELIMITER);


            var holidayRequestRepository = new HolidayRequestRepository(HOLIDAY_REQUEST_FILE, CSV_DELIMITER);
            var OrderedItemRepository = new StoredItemRepository(ORDERED_ITEM_FILE, CSV_DELIMITER);

            var surveyQuestionsRepository = new SurveyQuestionsRepository(SURVEY_QUESTIONS_FILE, CSV_DELIMITER);
            var surveyAnswersRepository = new SurveyAnswersRepository(SURVEY_ANSWERS_FILE, CSV_DELIMITER);
            var antiTrollMechanismRepository = new AntiTrollMechanismRepository(ANTI_TROLL_FILE, CSV_DELIMITER);
            var noteRepository = new NoteRepository(NOTE_FILE, CSV_DELIMITER);
            var meetingRepository = new MeetingRepository(MEETINGS_FILE, CSV_DELIMITER);



            //var itemService = new ItemService(itemRepository);
            //var storedItemService = new StoredItemService(storedItemRepository, itemRepository);
            //var roomService = new RoomService(roomRepository, storedItemRepository, itemRepository);
            //var warehouseService = new WarehouseService(warehouseRepository, itemRepository, storedItemRepository);

            var itemService = new ItemService(itemRepository, itemTypeRepository);
            var itemTypeService = new ItemTypeService(itemTypeRepository);
            var storedItemService = new StoredItemService(storedItemRepository, itemRepository, itemTypeRepository);
            var roomService = new RoomService(roomRepository, storedItemRepository, itemRepository, itemTypeRepository);
            var warehouseService = new WarehouseService(warehouseRepository, itemRepository, storedItemRepository, itemTypeRepository);

            var doctorService = new DoctorService(doctorRepository, roomRepository, accountRepository, specialtyRepository);
            var specialtyService = new SpecialtyService(specialtyRepository);
            var patientService = new PatientService(patientRepository, accountRepository);
            var managerService = new ManagerService(managerRepository, accountRepository);
            var secretaryService = new SecretaryService(secretaryRepository, accountRepository);
            var renovationAppointmentService = new RenovationAppointmentService(renovationAppointmentRepository, roomRepository,
                storedItemRepository, itemRepository, scheduledAppointmentRepository, unScheduledAppointmentRepository, renovationTypeRepository, roomUnderRenovationRepository);
            var renovationTypeService = new RenovationTypeService(renovationTypeRepository);
            var scheduledAppointmentService = new ScheduledAppointmentService(scheduledAppointmentRepository,roomRepository, doctorRepository,
                patientRepository, accountRepository);
            var unScheduledAppointmentService = new ScheduledAppointmentService(unScheduledAppointmentRepository, roomRepository, doctorRepository,
                patientRepository, accountRepository);
            var scheduledItemTransferService = new ScheduledItemTransferService(ScheduledItemTransferRepository, roomRepository, warehouseRepository,
                itemRepository, storedItemRepository, itemTypeRepository);

            var accountService = new AccountService(accountRepository, patientRepository, doctorRepository, secretaryRepository, managerRepository, roomRepository);

            var anamnesisService = new AnamnesisService(anamnesisRepository);
            var medicationService = new MedicationService(medicationRepository, ingredientRepository, medicationTypeRepository);
            var medicationTypeService = new MedicationTypeService(medicationTypeRepository);
            var medicationApprovalRequestService = new MedicationApprovalRequestService(medicationApprovalRequestRepository,
                medicationRepository,ingredientRepository,medicationTypeRepository,doctorRepository);

            var ingredientService = new IngredientService(ingredientRepository);
            var prescribedMedicineService = new PrescribedMedicineService(prescribedMedicineRepository, medicationRepository, medicationTypeRepository, ingredientRepository);
            var labAnalysisComponentService = new LabAnalysisComponentService(labAnalysisComponentRepository);

            var medicalExaminationService = new MedicalExaminationService(medicalExaminationRepository, scheduledAppointmentRepository,
             anamnesisRepository, specialistRequestRepository,
             labAnalysisRequestRepository, hospitalizationRequestRepository,
             prescribedMedicineRepository, doctorRepository, patientRepository,
             roomRepository, accountRepository, specialtyRepository, labAnalysisComponentRepository,
             medicationRepository, ingredientRepository);


            var allergenService = new AllergensService(allergenRepository, ingredientRepository);
            var healthRecordService = new HealthRecordService(patientRepository, anamnesisRepository, healthRecordRepository, ingredientRepository, allergenRepository);

            var holidayRequestService = new HolidayRequestService(holidayRequestRepository, doctorRepository);

            var systemService = new SystemService(ScheduledItemTransferRepository, storedItemRepository,
                renovationAppointmentRepository, roomRepository, roomUnderRenovationRepository);
            var orderedItemService = new StoredItemService(OrderedItemRepository, itemRepository, itemTypeRepository);

            var surveyQuestionsService = new SurveyQuestionsService(surveyQuestionsRepository, scheduledAppointmentRepository);
            var surveyAnswersService = new SurveyAnswersService(surveyAnswersRepository, patientRepository, surveyQuestionsRepository);
            var antiTrollMechanismService = new AntiTrollMechanismService(antiTrollMechanismRepository, patientRepository);
            var noteService = new NoteService(noteRepository, patientRepository);

            var meetingService = new MeetingService(meetingRepository, accountRepository, roomRepository);



            ItemController = new ItemController(itemService);
            ItemTypeController = new ItemTypeController(itemTypeService);
            StoredItemController = new StoredItemController(storedItemService);
            RoomController = new RoomController(roomService);
            WarehouseController = new WarehouseController(warehouseService);
            DoctorController = new DoctorController(doctorService);
            PatientController = new PatientController(patientService);
            RenovationAppointmentController = new RenovationAppointmentController(renovationAppointmentService);
            RenovationTypeController = new RenovationTypeController(renovationTypeService);
            ScheduledAppointmentController = new ScheduledAppointmentController(scheduledAppointmentService);
            UnScheduledAppointmentController = new ScheduledAppointmentController(unScheduledAppointmentService);
            ScheduledItemTransferController = new ScheduledItemTransferController(scheduledItemTransferService);
            AccountController = new AccountController(accountService);
            ManagerController = new ManagerController(managerService);
            SecretaryController = new SecretaryController(secretaryService);
            SpecialtyController = new SpecialtyController(specialtyService);
            AnamnesisController = new AnamnesisController(anamnesisService);
            MedicationController = new MedicationController(medicationService); 
            MedicationTypeController = new MedicationTypeController(medicationTypeService);
            MedicationApprovalRequestController = new MedicationApprovalRequestController(medicationApprovalRequestService);

            IngredientController = new IngredientController(ingredientService);
            PrescribedMedicineController = new PrescribedMedicineController(prescribedMedicineService);
            LabAnalysisComponentController = new LabAnalysisComponentController(labAnalysisComponentService);

            MedicalExaminationController = new MedicalExaminationController(medicalExaminationService);


            AllergensController = new AllergensController(allergenService);
            HealthRecordController = new HealthRecordController(healthRecordService);

            HolidayRequestController = new HolidayRequestController(holidayRequestService);

            SystemController = new SystemController(systemService);
            SystemController.StartCheckingForScheduledItemTransfers(300);
            SystemController.StartCheckingForRenovationAppointments(300);

            OrderedItemController = new StoredItemController(orderedItemService);

            SurveyQuestionsController = new SurveyQuestionsController(surveyQuestionsService);
            SurveyAnswersController = new SurveyAnswersController(surveyAnswersService);
            AntiTrollMechanismController = new AntiTrollMechanismController(antiTrollMechanismService);
            NoteController = new NoteController(noteService);
            MeetingController = new MeetingController(meetingService);
        }

        public void ChangeLanguage(string currLang)
        {
            if (currLang.Equals("en-US"))
            {
                TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            }
            else
            {
                TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("sr-LATN");
            }
        }
    }
}
