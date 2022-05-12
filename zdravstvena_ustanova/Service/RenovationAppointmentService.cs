using zdravstvena_ustanova.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;

namespace zdravstvena_ustanova.Service
{
    public class RenovationAppointmentService
    {
        private readonly RenovationAppointmentRepository _renovationAppointmentRepository;
        private readonly RoomRepository _roomRepository;
        private readonly StoredItemRepository _itemRoomRepository;
        private readonly ItemRepository _itemRepository;
        private readonly ScheduledAppointmentRepository _scheduledAppointmentRepository;
        private readonly ScheduledAppointmentRepository _unScheduledAppointmentRepository;
        private readonly RenovationTypeRepository _renovationTypeRepository;

        public RenovationAppointmentService(RenovationAppointmentRepository renovationAppointmentRepository,
            RoomRepository roomRepository, StoredItemRepository itemRoomRepository, ItemRepository itemRepository, 
            ScheduledAppointmentRepository scheduledAppointmentRepository,
            ScheduledAppointmentRepository unScheduledAppointmentRepository, RenovationTypeRepository renovationTypeRepository)
        {
            _renovationAppointmentRepository = renovationAppointmentRepository;
            _roomRepository = roomRepository;
            _itemRoomRepository = itemRoomRepository;
            _itemRepository = itemRepository;
            _scheduledAppointmentRepository = scheduledAppointmentRepository;
            _unScheduledAppointmentRepository = unScheduledAppointmentRepository;
            _renovationTypeRepository = renovationTypeRepository;
        }

        public IEnumerable<RenovationAppointment> GetAll()
        {
            var items = _itemRepository.GetAll();
            var itemRooms = _itemRoomRepository.GetAll();
            var rooms = _roomRepository.GetAll();
            var renovationAppointments = _renovationAppointmentRepository.GetAll();
            var renovationTypes = _renovationTypeRepository.GetAll();
            BindRenovationAppointmentsWithRenovationTypes(renovationAppointments, renovationTypes);
            BindItemsWithItemRooms(items, itemRooms);
            BindStoredItemsWithRooms(itemRooms, rooms);
            BindRoomWithRenovationAppointments(renovationAppointments, rooms);

            return renovationAppointments;
        }

        private void BindRenovationAppointmentsWithRenovationTypes(IEnumerable<RenovationAppointment> renovationAppointments,
            IEnumerable<RenovationType> renovationTypes)
        {
            foreach(var renovationAppointment in renovationAppointments)
            {
                BindRenovationAppointmentWithRenovationTypes(renovationAppointment, renovationTypes);
            }
        }

        private void BindRenovationAppointmentWithRenovationTypes(RenovationAppointment renovationAppointment, IEnumerable<RenovationType> renovationTypes)
        {
            renovationAppointment.RenovationType = FindRenovationTypeById(renovationTypes, renovationAppointment.RenovationType.Id);
        }

        private RenovationType FindRenovationTypeById(IEnumerable<RenovationType> renovationTypes, long id)
        {
            return renovationTypes.SingleOrDefault(renovationType => renovationType.Id == id);
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

        public int NumberOfScheduledAppointmentsFromToForRoom(Room room, DateTime from, DateTime to)
        {
            var allScheduledAppointments = _scheduledAppointmentRepository.GetAll();

            List<ScheduledAppointment> scheduledAppointmentsForRequestedRoom = new List<ScheduledAppointment>();

            foreach (var scheduledAppointment in allScheduledAppointments)
            {
                if (scheduledAppointment.Room.Id == room.Id)
                {
                    if(DatesOverlaping(scheduledAppointment.Start, scheduledAppointment.End, from, to))
                    {
                        scheduledAppointmentsForRequestedRoom.Add(scheduledAppointment);
                    }
                }
            }
            return scheduledAppointmentsForRequestedRoom.Count;
        }

        public bool HasRenovationFromTo(Room room, DateTime from, DateTime to)
        {
            var allRenovationAppointments = GetAll();
            bool overlap = false;

            foreach (var renovationAppointment in allRenovationAppointments)
            {
                if (renovationAppointment.FirstRoom.Id == room.Id)
                {
                    overlap = DatesOverlaping(renovationAppointment.StartDate, renovationAppointment.EndDate, from, to);
                    if (overlap) return overlap;
                }
            }
            return overlap;
        }
        private bool DatesOverlaping(DateTime aStart, DateTime aEnd, DateTime bStart, DateTime bEnd)
        {
            return aStart.CompareTo(bEnd) <= 0 && bStart.CompareTo(aEnd) <= 0;
        }
        public RenovationAppointment ScheduleRenovation(RenovationAppointment renovationAppointment)
        {
            var allScheduledAppointments = _scheduledAppointmentRepository.GetAll();
            bool overlap = false;
            List<ScheduledAppointment> scheduledAppointmentsForRequestedRoom = new List<ScheduledAppointment>();

            foreach (var scheduledAppointment in allScheduledAppointments)
            {
                if (scheduledAppointment.Room.Id == renovationAppointment.FirstRoom.Id)
                {
                    scheduledAppointmentsForRequestedRoom.Add(scheduledAppointment);
                }
            }

            foreach (var scheduledAppointment in scheduledAppointmentsForRequestedRoom)
            {
                overlap = DatesOverlaping(scheduledAppointment.Start, scheduledAppointment.End,
                                          renovationAppointment.StartDate, renovationAppointment.EndDate);
                if (overlap)
                {
                    _unScheduledAppointmentRepository.Create(scheduledAppointment);
                    _scheduledAppointmentRepository.Delete(scheduledAppointment.Id);
                }
            }

            renovationAppointment = _renovationAppointmentRepository.Create(renovationAppointment);

            return renovationAppointment;
        }

        public IEnumerable<RenovationAppointment> GetIfContainsDateForRoom(DateTime date, long roomId)
        {
            var listOfAppointments = GetAll();
            var listOfCorrectAppointments = new List<RenovationAppointment>();
            foreach (RenovationAppointment ra in listOfAppointments)
            {
                if (ra.StartDate.CompareTo(date) <= 0 && ra.EndDate.CompareTo(date) >= 0)
                {
                    if(ra.FirstRoom.Id == roomId) listOfCorrectAppointments.Add(ra);
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
                renovationAppointment.FirstRoom = FindRoomById(rooms, renovationAppointment.FirstRoom.Id);
            });
        }

        public RenovationAppointment GetById(long id)
        {
            var items = _itemRepository.GetAll();
            var itemRooms = _itemRoomRepository.GetAll();
            var rooms = _roomRepository.GetAll();
            var renovationAppointment = _renovationAppointmentRepository.Get(id);
            var renovationTypes = _renovationTypeRepository.GetAll();
            BindRenovationAppointmentWithRenovationTypes(renovationAppointment, renovationTypes);
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
                    if (room.Id == renovationAppointment.FirstRoom.Id)
                    {
                        renovationAppointment.FirstRoom = room;
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
