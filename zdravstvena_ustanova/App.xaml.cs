using System;
using System.Windows;
using Controller;
using Repository;
using Service;
using Model;
using zdravstvena_ustanova.Controller;
using zdravstvena_ustanova.Repository;
using zdravstvena_ustanova.Service;

namespace zdravstvena_ustanova
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string ProjectPath { get; set; } = System.Reflection.Assembly.GetExecutingAssembly().Location
               .Split(new string[] { "bin" }, StringSplitOptions.None)[0];

        private const string CSV_DELIMITER = ";";

        private readonly string ITEM_FILE = ProjectPath + "\\Resources\\Data\\Items.csv";
        private readonly string STORED_ITEM_FILE = ProjectPath + "\\Resources\\Data\\StoredItems.csv";
        private readonly string ROOM_FILE = ProjectPath + "\\Resources\\Data\\Rooms.csv";
        private readonly string WAREHOUSE_FILE = ProjectPath + "\\Resources\\Data\\Warehouses.csv";
        private readonly string RENOVATION_APPOINTMENT_FILE = ProjectPath + "\\Resources\\Data\\RenovationAppointments.csv";
        private readonly string SCHEDULED_APPOINTMENT_FILE = ProjectPath + "\\Resources\\Data\\ScheduledAppointments.csv";
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

        public ItemController ItemController { get; set; }
        public StoredItemController StoredItemController { get; set; }
        public RoomController RoomController { get; set; }
        public WarehouseController WarehouseController { get; set; }
        public RenovationAppointmentController RenovationAppointmentController { get; set; }
        public ScheduledAppointmentController ScheduledAppointmentController { get; set; }
        public DoctorController DoctorController { get; set; }
        public PatientController PatientController { get; set; }
        public AccountController AccountController { get; set; }
        public SecretaryController SecretaryController { get; set; }
        public ManagerController ManagerController { get; set; }
        public SpecialtyController SpecialtyController { get; set; }
        public AnamnesisController AnamnesisController { get; set; }
        public MedicationController MedicationController { get; set; }
        public IngredientController IngredientController { get; set; }
        public PrescribedMedicineController PrescribedMedicineController { get; set; }


        public Person LoggedInUser { get; set; }

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Manager Manager { get; set; }
        public Secretary Secretary { get; set; }

        public App()
        {
            var itemRepository = new ItemRepository(ITEM_FILE, CSV_DELIMITER);
            var storedItemRepository = new StoredItemRepository(STORED_ITEM_FILE, CSV_DELIMITER);
            var roomRepository = new RoomRepository(ROOM_FILE, CSV_DELIMITER);
            var warehouseRepository = new WarehouseRepository(WAREHOUSE_FILE, CSV_DELIMITER);
            var renovationAppointmentRepository = new RenovationAppointmentRepository(RENOVATION_APPOINTMENT_FILE, CSV_DELIMITER);
            var scheduledAppointmentRepository = new ScheduledAppointmentRepository(SCHEDULED_APPOINTMENT_FILE, CSV_DELIMITER);
            var doctorRepository = new DoctorRepository(DOCTOR_FILE, CSV_DELIMITER);
            var patientRepository = new PatientRepository(PATIENT_FILE, CSV_DELIMITER);
            var accountRepository = new AccountRepository(ACCOUNT_FILE, CSV_DELIMITER);
            var managerRepository = new ManagerRepository(MANAGER_FILE, CSV_DELIMITER);
            var secretaryRepository = new SecretaryRepository(SECRETARY_FILE, CSV_DELIMITER);
            var specialtyRepository = new SpecialtyRepository(SPECIALTY_FILE, CSV_DELIMITER);
            var anamnesisRepository = new AnamnesisRepository(ANAMNESIS_FILE, CSV_DELIMITER);

            var medicationRepository = new MedicationRepository(MEDICATION_FILE, CSV_DELIMITER);
            var ingredientRepository = new IngredientRepository(INGREDIENT_FILE, CSV_DELIMITER);
            var prescribedMedicineRepository = new PrescribedMedicineRepository(PRESCRIBED_MEDICINE_FILE, CSV_DELIMITER);


            var itemService = new ItemService(itemRepository);
            var storedItemService = new StoredItemService(storedItemRepository, itemRepository);
            var roomService = new RoomService(roomRepository, storedItemRepository, itemRepository);
            var warehouseService = new WarehouseService(warehouseRepository, itemRepository, storedItemRepository);
            var doctorService = new DoctorService(doctorRepository, roomRepository, accountRepository, specialtyRepository);
            var specialtyService = new SpecialtyService(specialtyRepository);
            var patientService = new PatientService(patientRepository, accountRepository);
            var managerService = new ManagerService(managerRepository, accountRepository);
            var secretaryService = new SecretaryService(secretaryRepository, accountRepository);
            var renovationAppointmentService = new RenovationAppointmentService(renovationAppointmentRepository, roomRepository,
                storedItemRepository, itemRepository);
            var ScheduledAppointmentService = new ScheduledAppointmentService(scheduledAppointmentRepository,roomRepository, doctorRepository,
                patientRepository, accountRepository);
            var accountService = new AccountService(accountRepository, patientRepository, doctorRepository, secretaryRepository, managerRepository);
            var anamnesisService = new AnamnesisService(anamnesisRepository);
            var medicationService = new MedicationService(medicationRepository, ingredientRepository);
            var ingredientService = new IngredientService(ingredientRepository);
            var prescribedMedicineService = new PrescribedMedicineService(prescribedMedicineRepository, medicationRepository, ingredientRepository);


            
            


            ItemController = new ItemController(itemService);
            StoredItemController = new StoredItemController(storedItemService);
            RoomController = new RoomController(roomService);
            WarehouseController = new WarehouseController(warehouseService);
            DoctorController = new DoctorController(doctorService);
            PatientController = new PatientController(patientService);
            RenovationAppointmentController = new RenovationAppointmentController(renovationAppointmentService);
            ScheduledAppointmentController = new ScheduledAppointmentController(ScheduledAppointmentService);
            AccountController = new AccountController(accountService);
            ManagerController = new ManagerController(managerService);
            SecretaryController = new SecretaryController(secretaryService);
            SpecialtyController = new SpecialtyController(specialtyService);
            AnamnesisController = new AnamnesisController(anamnesisService);
            MedicationController = new MedicationController(medicationService);
            IngredientController = new IngredientController(ingredientService);
            PrescribedMedicineController = new PrescribedMedicineController(prescribedMedicineService);

        }
    }
}
