using System;
using System.Windows;
using Controller;
using Repository;
using Service;

namespace Zdravstena_ustanova
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string CSV_DELIMITER = ";";
        private static string _projectPath = System.Reflection.Assembly.GetExecutingAssembly().Location
               .Split(new string[] { "bin" }, StringSplitOptions.None)[0];
        private string ITEM_FILE = _projectPath + "\\Resources\\Data\\Items.csv";
        private string ITEM_ROOM_FILE = _projectPath + "\\Resources\\Data\\ItemRooms.csv";
        private string ROOM_FILE = _projectPath + "\\Resources\\Data\\Rooms.csv";
        private string SCHEDULED_APPOINTMENT_FILE = _projectPath + "\\Resources\\Data\\ScheduledAppointments.csv";
        private string DOCTOR_FILE = _projectPath + "\\Resources\\Data\\Doctors.csv";
        private string PATIENT_FILE = _projectPath + "\\Resources\\Data\\Patients.csv";

        public ItemController ItemController { get; set; }
        public ItemRoomController ItemRoomController { get; set; }
        public RoomController RoomController { get; set; }
        public ScheduledAppointmentController ScheduledAppointmentController { get; set; }
        public DoctorController DoctorController { get; set; }
        public PatientController PatientController { get; set; }

        public App()
        {
            var itemRepository = new ItemRepository(ITEM_FILE, CSV_DELIMITER);
            var itemRoomRepository = new ItemRoomRepository(ITEM_ROOM_FILE, CSV_DELIMITER);
            var roomRepository = new RoomRepository(ROOM_FILE, CSV_DELIMITER);
            var scheduledAppointmentRepository = new ScheduledAppointmentRepository(SCHEDULED_APPOINTMENT_FILE, CSV_DELIMITER);
            var doctorRepository = new DoctorRepository(DOCTOR_FILE, CSV_DELIMITER);
            var patientRepository = new PatientRepository(PATIENT_FILE, CSV_DELIMITER);

            var itemService = new ItemService(itemRepository);
            var itemRoomService = new ItemRoomService(itemRoomRepository, itemService);
            var roomService = new RoomService(roomRepository, itemRoomService);
            var doctorService = new DoctorService(doctorRepository);
            var patientService = new PatientService(patientRepository);
            var ScheduledAppointmentService = new ScheduledAppointmentService(scheduledAppointmentRepository, roomService, doctorService, patientService);


            ItemController = new ItemController(itemService);
            ItemRoomController = new ItemRoomController(itemRoomService);
            RoomController = new RoomController(roomService);
            DoctorController = new DoctorController(doctorService);
            PatientController = new PatientController(patientService);
            ScheduledAppointmentController = new ScheduledAppointmentController(ScheduledAppointmentService);
        }
    }
}
