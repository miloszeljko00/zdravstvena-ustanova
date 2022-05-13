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
        private readonly RoomRepository _roomUnderRenovationRepository;
        private readonly StoredItemRepository _itemRoomRepository;
        private readonly ItemRepository _itemRepository;
        private readonly ScheduledAppointmentRepository _scheduledAppointmentRepository;
        private readonly ScheduledAppointmentRepository _unScheduledAppointmentRepository;
        private readonly RenovationTypeRepository _renovationTypeRepository;

        public RenovationAppointmentService(RenovationAppointmentRepository renovationAppointmentRepository,
            RoomRepository roomRepository, StoredItemRepository itemRoomRepository, ItemRepository itemRepository,
            ScheduledAppointmentRepository scheduledAppointmentRepository,
            ScheduledAppointmentRepository unScheduledAppointmentRepository, RenovationTypeRepository renovationTypeRepository,
            RoomRepository roomUnderRenovationRepository)
        {
            _renovationAppointmentRepository = renovationAppointmentRepository;
            _roomRepository = roomRepository;
            _itemRoomRepository = itemRoomRepository;
            _itemRepository = itemRepository;
            _scheduledAppointmentRepository = scheduledAppointmentRepository;
            _unScheduledAppointmentRepository = unScheduledAppointmentRepository;
            _renovationTypeRepository = renovationTypeRepository;
            _roomUnderRenovationRepository = roomUnderRenovationRepository;

        }

        public IEnumerable<RenovationAppointment> GetAll()
        {
            var items = _itemRepository.GetAll();
            var itemRooms = _itemRoomRepository.GetAll();
            var rooms = _roomRepository.GetAll();
            var roomsUnderRenovation = _roomUnderRenovationRepository.GetAll();
            var renovationAppointments = _renovationAppointmentRepository.GetAll();
            var renovationTypes = _renovationTypeRepository.GetAll();
            BindRenovationAppointmentsWithRenovationTypes(renovationAppointments, renovationTypes);
            BindItemsWithItemRooms(items, itemRooms);
            BindStoredItemsWithRooms(itemRooms, rooms);
            BindRoomWithRenovationAppointments(renovationAppointments, rooms);
            BindRenovationAppointmentsWithRoomsUnderRenovation(renovationAppointments, roomsUnderRenovation, rooms);
            return renovationAppointments;
        }

        public IEnumerable<RenovationAppointment> GetRenovationAppointmentsByMergeRoomForMergeRenovation(long roomId)
        {
            var renovationAppointments = (List<RenovationAppointment>)GetAll();
            renovationAppointments = GetAllMergeRenovations(renovationAppointments);
            renovationAppointments = GetByMergeRoomId(roomId, renovationAppointments);
            return renovationAppointments;
        }

        private static List<RenovationAppointment> GetByMergeRoomId(long roomId, List<RenovationAppointment> renovationAppointments)
        {
            renovationAppointments = renovationAppointments.FindAll(renovationAppointment => renovationAppointment.FirstRoom.Id == roomId);
            return renovationAppointments;
        }

        private static List<RenovationAppointment> GetAllMergeRenovations(List<RenovationAppointment> renovationAppointments)
        {
            renovationAppointments = renovationAppointments.FindAll(renovationAppointment => renovationAppointment.RenovationType.Id == 2);
            return renovationAppointments;
        }

        private void BindRenovationAppointmentsWithRoomsUnderRenovation(IEnumerable<RenovationAppointment> renovationAppointments, IEnumerable<Room> roomsUnderRenovation, IEnumerable<Room> rooms)
        {
            foreach(var renovationAppointment in renovationAppointments)
            {
                BindRenovationAppointmentWithRoomsUnderRenovation(renovationAppointment, roomsUnderRenovation, rooms);
            }
        }

        private void BindRenovationAppointmentWithRoomsUnderRenovation(RenovationAppointment renovationAppointment, IEnumerable<Room> roomsUnderRenovation, IEnumerable<Room> rooms)
        {
            if (renovationAppointment.RenovationType.Id == 2)
            {
                BindRoomsForMergeRenovation(renovationAppointment, roomsUnderRenovation, rooms);
            }
            else if( renovationAppointment.RenovationType.Id == 3)
            {
                BindRoomsForSplitRenovation(renovationAppointment, roomsUnderRenovation);
            }
        }

        private void BindRoomsForSplitRenovation(RenovationAppointment renovationAppointment, IEnumerable<Room> roomsUnderRenovation)
        {
            renovationAppointment.FirstRoom = FindRoomById(roomsUnderRenovation, renovationAppointment.FirstRoom.Id);
            renovationAppointment.SecondRoom = FindRoomById(roomsUnderRenovation, renovationAppointment.SecondRoom.Id);
        }

        private void BindRoomsForMergeRenovation(RenovationAppointment renovationAppointment, IEnumerable<Room> roomsUnderRenovation, IEnumerable<Room> rooms)
        {
            renovationAppointment.FirstRoom = FindRoomById(rooms, renovationAppointment.FirstRoom.Id);
            renovationAppointment.SecondRoom = FindRoomById(roomsUnderRenovation, renovationAppointment.SecondRoom.Id);
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
                if (renovationAppointment.Room.Id == room.Id)
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
            RenovationAppointment scheduledRenovationAppointment = null;

            if (renovationAppointment.RenovationType.Id == 1)
            {
                scheduledRenovationAppointment = ScheduleStandardRenovation(ref renovationAppointment);
            }
            else if (renovationAppointment.RenovationType.Id == 2)
            {
                scheduledRenovationAppointment = ScheduleMergeRenovation(ref renovationAppointment);
            }
            else if (renovationAppointment.RenovationType.Id == 3)
            {
                scheduledRenovationAppointment = ScheduleSplitRenovation(ref renovationAppointment);
            }
            return scheduledRenovationAppointment;
        }

        private RenovationAppointment? ScheduleSplitRenovation(ref RenovationAppointment renovationAppointment)
        {
            var scheduledAppointmentsForRoomInSplitRenovation = GetAllScheduledAppointmentsByRoomId(renovationAppointment.Room.Id);

            UnscheduleAllOverlappingScheduledAppointments(renovationAppointment, scheduledAppointmentsForRoomInSplitRenovation);

            renovationAppointment = Create(renovationAppointment);

            return renovationAppointment;
        }

        private RenovationAppointment? ScheduleMergeRenovation(ref RenovationAppointment renovationAppointment)
        {
            renovationAppointment = ScheduleRenovationForMainRoom(renovationAppointment);

            ScheduleRenovationForMergedRoom(renovationAppointment);

            return renovationAppointment;
        }

        private RenovationAppointment ScheduleRenovationForMainRoom(RenovationAppointment renovationAppointment)
        {
            var scheduledAppointmentsForFirstRoomInMerge = GetAllScheduledAppointmentsByRoomId(renovationAppointment.Room.Id);
            UnscheduleAllOverlappingScheduledAppointments(renovationAppointment, scheduledAppointmentsForFirstRoomInMerge);

            renovationAppointment = Create(renovationAppointment);
            return renovationAppointment;
        }

        private RenovationAppointment ScheduleRenovationForMergedRoom(RenovationAppointment renovationAppointment)
        {
            var scheduledAppointmentsForSecondRoomInMergeRenovation = GetAllScheduledAppointmentsByRoomId(renovationAppointment.FirstRoom.Id);
            UnscheduleAllScheduledAppointmentsAfter(renovationAppointment.StartDate, scheduledAppointmentsForSecondRoomInMergeRenovation);
            
            var mergedRoom = renovationAppointment.FirstRoom;
            var mergedRoomRenovationAppointment = new RenovationAppointment(mergedRoom, renovationAppointment.StartDate,
                DateTime.MaxValue, renovationAppointment.Description, _renovationTypeRepository.GetById(1));

            renovationAppointment = Create(mergedRoomRenovationAppointment);
            return renovationAppointment;
        }

        private void UnscheduleAllScheduledAppointmentsAfter(DateTime date, List<ScheduledAppointment> scheduledAppointments)
        {
            foreach (var scheduledAppointment in scheduledAppointments)
            {
                if (date.CompareTo(scheduledAppointment.End) < 0)
                {
                    _unScheduledAppointmentRepository.Create(scheduledAppointment);
                    _scheduledAppointmentRepository.Delete(scheduledAppointment.Id);
                }
            }
        }

        private RenovationAppointment? ScheduleStandardRenovation(ref RenovationAppointment renovationAppointment)
        {
            var scheduledAppointmentsForRoomInStandardRenovation = GetAllScheduledAppointmentsByRoomId(renovationAppointment.Room.Id);

            UnscheduleAllOverlappingScheduledAppointments(renovationAppointment, scheduledAppointmentsForRoomInStandardRenovation);

            renovationAppointment = Create(renovationAppointment);

            return renovationAppointment;
        }

        private void UnscheduleAllOverlappingScheduledAppointments(RenovationAppointment renovationAppointment,
                                            IEnumerable<ScheduledAppointment> scheduledAppointments)
        {
            foreach (var scheduledAppointment in scheduledAppointments)
            {
                if (DatesOverlaping(scheduledAppointment.Start, scheduledAppointment.End, renovationAppointment.StartDate, renovationAppointment.EndDate))
                {
                    _unScheduledAppointmentRepository.Create(scheduledAppointment);
                    _scheduledAppointmentRepository.Delete(scheduledAppointment.Id);
                }
            }
        }

        private List<ScheduledAppointment> GetAllScheduledAppointmentsByRoomId(long roomId)
        {

            var scheduledAppointments = _scheduledAppointmentRepository.GetAll();

            return (from scheduledAppointment in scheduledAppointments
                    where scheduledAppointment.Room.Id == roomId
                    select scheduledAppointment).ToList();
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
