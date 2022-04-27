using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Enums;

namespace Service
{
    public class RenovationAppointmentService
    {
        private readonly RenovationAppointmentRepository _renovationAppointmentRepository;
        private readonly RoomRepository _roomRepository;
        private readonly StoredItemRepository _itemRoomRepository;
        private readonly ItemRepository _itemRepository;

        public RenovationAppointmentService(RenovationAppointmentRepository renovationAppointmentRepository, RoomRepository roomRepository, StoredItemRepository itemRoomRepository, ItemRepository itemRepository)
        {
            _renovationAppointmentRepository = renovationAppointmentRepository;
            _roomRepository = roomRepository;
            _itemRoomRepository = itemRoomRepository;
            _itemRepository = itemRepository;
        }

        public IEnumerable<RenovationAppointment> GetAll()
        {
            var items = _itemRepository.GetAll();
            var itemRooms = _itemRoomRepository.GetAll();
            var rooms = _roomRepository.GetAll();
            var renovationAppointments = _renovationAppointmentRepository.GetAll();

            BindItemsWithItemRooms(items, itemRooms);
            BindStoredItemsWithRooms(itemRooms, rooms);
            BindRoomWithRenovationAppointments(renovationAppointments, rooms);

            return renovationAppointments;
        }
        public IEnumerable<RenovationAppointment> GetFromToDates(DateTime start, DateTime end)
        {
            var listOfAppointments = GetAll();
            var listOfCorrectAppointments = new List<RenovationAppointment>();
            foreach (RenovationAppointment ra in listOfAppointments)
            {
                if (ra.StartDate >= start && ra.EndDate <= end)
                {
                    listOfCorrectAppointments.Add(ra);
                }
            }

            return listOfCorrectAppointments;
        }
        public IEnumerable<RenovationAppointment> GetIfContainsDateForRoom(DateTime date, long roomId)
        {
            var listOfAppointments = GetAll();
            var listOfCorrectAppointments = new List<RenovationAppointment>();
            foreach (RenovationAppointment ra in listOfAppointments)
            {
                if (ra.StartDate.CompareTo(date) <= 0 && ra.EndDate.CompareTo(date) >= 0)
                {
                    if(ra.Room.Id == roomId) listOfCorrectAppointments.Add(ra);
                }
            }

            return listOfCorrectAppointments;
        }

        public IEnumerable<RenovationAppointment> GetIfContainsDate(DateTime date)
        {
            var listOfAppointments = GetAll();
            var listOfCorrectAppointments = new List<RenovationAppointment>();
            foreach (RenovationAppointment ra in listOfAppointments)
            {
                if (ra.StartDate.CompareTo(date) <= 0 && ra.EndDate.CompareTo(date) >= 0)
                {
                    listOfCorrectAppointments.Add(ra);
                }
            }

            return listOfCorrectAppointments;
        }
        private void BindRoomWithRenovationAppointments(IEnumerable<RenovationAppointment> renovationAppointments, IEnumerable<Room> rooms)
        {
            renovationAppointments.ToList().ForEach(renovationAppointment =>
            {
                renovationAppointment.Room = FindRoomById(rooms, renovationAppointment.Room.Id);
            });
        }

        public RenovationAppointment GetById(long id)
        {
            var items = _itemRepository.GetAll();
            var itemRooms = _itemRoomRepository.GetAll();
            var rooms = _roomRepository.GetAll();
            var renovationAppointment = _renovationAppointmentRepository.Get(id);

            BindItemsWithItemRooms(items, itemRooms);
            BindStoredItemsWithRooms(itemRooms, rooms);
            BindRoomWithRenovationAppointment(renovationAppointment, rooms);
            return renovationAppointment;
        }

        private void BindRoomWithRenovationAppointment(RenovationAppointment renovationAppointment, IEnumerable<Room> rooms)
        {
            rooms.ToList().ForEach(room =>
            {
                if (room != null)
                {
                    if (room.Id == renovationAppointment.Room.Id)
                    {
                        renovationAppointment.Room = room;
                    }
                }
            });
        }

        private void BindItemsWithItemRooms(IEnumerable<Item> items, IEnumerable<StoredItem> itemRooms)
        {
            itemRooms.ToList().ForEach(itemRoom =>
            {
                itemRoom.Item = FindItemById(items, itemRoom.Item.Id);
            });
        }
        private Item FindItemById(IEnumerable<Item> items, long itemId)
        {
            return items.SingleOrDefault(item => item.Id == itemId);
        }
       
        private void BindStoredItemsWithRooms(IEnumerable<StoredItem> storedItems, IEnumerable<Room> rooms)
        {
            storedItems.ToList().ForEach(storedItem =>
            {
                if(storedItem.StorageType == StorageType.ROOM)
                {
                    var room = FindRoomById(rooms, storedItem.Room.Id);
                    if (room != null)
                    {
                        if (room.Id == storedItem.Room.Id)
                        {
                            storedItem.Room = room;
                            room.StoredItems.Add(storedItem);
                        }
                    }
                }
               
            });
        }

        private Room FindRoomById(IEnumerable<Room> rooms, long roomId)
        {
            return rooms.SingleOrDefault(room => room.Id == roomId);
        }

        public RenovationAppointment Create(RenovationAppointment renovationAppointment)
        {
            return _renovationAppointmentRepository.Create(renovationAppointment);
        }
        public bool Update(RenovationAppointment renovationAppointment)
        {
            return _renovationAppointmentRepository.Update(renovationAppointment);
        }
        public bool Delete(long renovationAppointmentId)
        {
            return _renovationAppointmentRepository.Delete(renovationAppointmentId);
        }
    }
}
