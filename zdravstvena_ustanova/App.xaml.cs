using System;
using System.Windows;
using Controller;
using Repository;
using Service;
using Model;

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
        private string ITEM_FILE = ProjectPath + "\\Resources\\Data\\Items.csv";
        private string ITEM_ROOM_FILE = ProjectPath + "\\Resources\\Data\\ItemRooms.csv";
        private string ROOM_FILE = ProjectPath + "\\Resources\\Data\\Rooms.csv";
        private string SCHEDULED_APPOINTMENT_FILE = ProjectPath + "\\Resources\\Data\\ScheduledAppointments.csv";
        private string DOCTOR_FILE = ProjectPath + "\\Resources\\Data\\Doctors.csv";
        private string PATIENT_FILE = ProjectPath + "\\Resources\\Data\\Patients.csv";
        private string MANAGER_FILE = ProjectPath + "\\Resources\\Data\\Managers.csv";
        private string SECRETARY_FILE = ProjectPath + "\\Resources\\Data\\Secretary.csv";
        private string ACCOUNT_FILE = ProjectPath + "\\Resources\\Data\\Accounts.csv";
        private string SPECIALTY_FILE = ProjectPath + "\\Resources\\Data\\Specialty.csv";
        private string DOCSPEC_FILE = ProjectPath + "\\Resources\\Data\\DoctorSpecialist.csv";

        public ItemController ItemController { get; set; }
        public ItemRoomController ItemRoomController { get; set; }
        public RoomController RoomController { get; set; }
        public ScheduledAppointmentController ScheduledAppointmentController { get; set; }
        public DoctorController DoctorController { get; set; }
        public PatientController PatientController { get; set; }
        public AccountController AccountController { get; set; }
        public SecretaryController SecretaryController { get; set; }
        public ManagerController ManagerController { get; set; }
        public SpecialtyController SpecialtyController { get; set; }

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Manager Manager { get; set; }
        public Secretary Secretary { get; set; }

        public App()
        {
            var itemRepository = new ItemRepository(ITEM_FILE, CSV_DELIMITER);
            var itemRoomRepository = new ItemRoomRepository(ITEM_ROOM_FILE, CSV_DELIMITER);
            var roomRepository = new RoomRepository(ROOM_FILE, CSV_DELIMITER);
            var scheduledAppointmentRepository = new ScheduledAppointmentRepository(SCHEDULED_APPOINTMENT_FILE, CSV_DELIMITER);
            var doctorRepository = new DoctorRepository(DOCTOR_FILE, CSV_DELIMITER);
            var patientRepository = new PatientRepository(PATIENT_FILE, CSV_DELIMITER);
            var accountRepository = new AccountRepository(ACCOUNT_FILE, CSV_DELIMITER);
            var managerRepository = new ManagerRepository(MANAGER_FILE, CSV_DELIMITER);
            var secretaryRepository = new SecretaryRepository(SECRETARY_FILE, CSV_DELIMITER);
            var specialtyRepository = new SpecialtyRepository(SPECIALTY_FILE, CSV_DELIMITER);

            var itemService = new ItemService(itemRepository);
            var itemRoomService = new ItemRoomService(itemRoomRepository, itemRepository);
            var roomService = new RoomService(roomRepository, itemRoomRepository, itemRepository);
            var doctorService = new DoctorService(doctorRepository, roomRepository, accountRepository, specialtyRepository);
            var specialtyService = new SpecialtyService(specialtyRepository);
            var patientService = new PatientService(patientRepository, accountRepository);
            var managerService = new ManagerService(managerRepository, accountRepository);
            var secretaryService = new SecretaryService(secretaryRepository, accountRepository);
            var ScheduledAppointmentService = new ScheduledAppointmentService(scheduledAppointmentRepository,roomRepository, doctorRepository,
                patientRepository, accountRepository);
            var accountService = new AccountService(accountRepository, patientRepository, doctorRepository, secretaryRepository, managerRepository);
            
            

            ItemController = new ItemController(itemService);
            ItemRoomController = new ItemRoomController(itemRoomService);
            RoomController = new RoomController(roomService);
            DoctorController = new DoctorController(doctorService);
            PatientController = new PatientController(patientService);
            ScheduledAppointmentController = new ScheduledAppointmentController(ScheduledAppointmentService);
            AccountController = new AccountController(accountService);
            ManagerController = new ManagerController(managerService);
            SecretaryController = new SecretaryController(secretaryService);
            SpecialtyController = new SpecialtyController(specialtyService);
        }
    }
}
