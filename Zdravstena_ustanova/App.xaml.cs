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
        private static string _projectPath = System.Reflection.Assembly.GetExecutingAssembly().Location
               .Split(new string[] { "bin" }, StringSplitOptions.None)[0];
        private string ITEM_FILE = _projectPath + "\\Resources\\Data\\Items.csv";
        private string ITEM_ROOM_FILE = _projectPath + "\\Resources\\Data\\ItemRooms.csv";
        private string ROOM_FILE = _projectPath + "\\Resources\\Data\\Rooms.csv";
        private const string CSV_DELIMITER = ";";

        public ItemController ItemController { get; set; }
        public ItemRoomController ItemRoomController { get; set; }
        public RoomController RoomController { get; set; }

        public App()
        {
            var itemRepository = new ItemRepository(ITEM_FILE, CSV_DELIMITER);
            var itemRoomRepository = new ItemRoomRepository(ITEM_ROOM_FILE, CSV_DELIMITER);
            var RoomRepository = new RoomRepository(ROOM_FILE, CSV_DELIMITER);

            var itemService = new ItemService(itemRepository);
            var itemRoomService = new ItemRoomService(itemRoomRepository, itemService);
            var RoomService = new RoomService(RoomRepository, itemRoomService);


            ItemController = new ItemController(itemService);
            ItemRoomController = new ItemRoomController(itemRoomService);
            RoomController = new RoomController(RoomService);
        }
    }
}
