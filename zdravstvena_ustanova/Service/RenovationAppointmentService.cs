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

        private const int StandardRenovationId = 1;
        private const int MergeRenovationId = 2;
        private const int SplitRenovationId = 3;



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
            renovationAppointments = renovationAppointments.FindAll(renovationAppointment => renovationAppointment.RoomForMergeOrFirstRoomOfSplit.Id == roomId);
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
            if (renovationAppointment.RenovationType.Id == MergeRenovationId)
            {
                BindRoomsForMergeRenovation(renovationAppointment, roomsUnderRenovation, rooms);
            }
            else if( renovationAppointment.RenovationType.Id == SplitRenovationId)
            {
                BindRoomsForSplitRenovation(renovationAppointment, roomsUnderRenovation);
            }
        }

        private void BindRoomsForSplitRenovation(RenovationAppointment renovationAppointment, IEnumerable<Room> roomsUnderRenovation)
        {
            renovationAppointment.RoomForMergeOrFirstRoomOfSplit = FindRoomById(roomsUnderRenovation, renovationAppointment.RoomForMergeOrFirstRoomOfSplit.Id);
            renovationAppointment.MergedRoomOrSecondRoomOfSplit = FindRoomById(roomsUnderRenovation, renovationAppointment.MergedRoomOrSecondRoomOfSplit.Id);
        }

        private void BindRoomsForMergeRenovation(RenovationAppointment renovationAppointment, IEnumerable<Room> roomsUnderRenovation, IEnumerable<Room> rooms)
        {
            renovationAppointment.RoomForMergeOrFirstRoomOfSplit = FindRoomById(rooms, renovationAppointment.RoomForMergeOrFirstRoomOfSplit.Id);
            renovationAppointment.MergedRoomOrSecondRoomOfSplit = FindRoomById(roomsUnderRenovation, renovationAppointment.MergedRoomOrSecondRoomOfSplit.Id);
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
            var scheduledAppointments = _scheduledAppointmentRepository.GetAll();

            var scheduledAppointmentsForRequestedRoom = new List<ScheduledAppointment>();

            foreach (var scheduledAppointment in scheduledAppointments)
            {
                if (scheduledAppointment.Room.Id != room.Id) continue;
                if(DatesOverlaping(scheduledAppointment.Start, scheduledAppointment.End, from, to))
                {
                    scheduledAppointmentsForRequestedRoom.Add(scheduledAppointment);
                }
            }
            return scheduledAppointmentsForRequestedRoom.Count;
        }

        public bool HasRenovationFromTo(Room room, DateTime from, DateTime to)
        {
            var renovationAppointments = GetAll();
            bool overlap = false;

            foreach (var renovationAppointment in renovationAppointments)
            {
                if (renovationAppointment.MainRoom.Id == room.Id)
                {
                    overlap = DatesOverlaping(renovationAppointment.StartDate, renovationAppointment.EndDate, from, to);
                    if (overlap) break;
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

            if (renovationAppointment.RenovationType.Id == StandardRenovationId)
            {
                scheduledRenovationAppointment = ScheduleStandardRenovation(ref renovationAppointment);
            }
            else if (renovationAppointment.RenovationType.Id == MergeRenovationId)
            {
                scheduledRenovationAppointment = ScheduleMergeRenovation(ref renovationAppointment);
            }
            else if (renovationAppointment.RenovationType.Id == SplitRenovationId)
            {
                scheduledRenovationAppointment = ScheduleSplitRenovation(ref renovationAppointment);
            }
            return scheduledRenovationAppointment;
        }

        private RenovationAppointment? ScheduleSplitRenovation(ref RenovationAppointment renovationAppointment)
        {
            var scheduledAppointmentsForRoomInSplitRenovation = GetAllScheduledAppointmentsByRoomId(renovationAppointment.MainRoom.Id);

            UnscheduleAllOverlappingScheduledAppointments(renovationAppointment, scheduledAppointmentsForRoomInSplitRenovation);

            renovationAppointment.RoomForMergeOrFirstRoomOfSplit = _roomUnderRenovationRepository.Create(renovationAppointment.RoomForMergeOrFirstRoomOfSplit);
            renovationAppointment.MergedRoomOrSecondRoomOfSplit = _roomUnderRenovationRepository.Create(renovationAppointment.MergedRoomOrSecondRoomOfSplit);
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
            var scheduledAppointmentsForFirstRoomInMerge = GetAllScheduledAppointmentsByRoomId(renovationAppointment.MainRoom.Id);
            UnscheduleAllOverlappingScheduledAppointments(renovationAppointment, scheduledAppointmentsForFirstRoomInMerge);

            renovationAppointment.MergedRoomOrSecondRoomOfSplit = _roomUnderRenovationRepository.Create(renovationAppointment.MergedRoomOrSecondRoomOfSplit);
            renovationAppointment = Create(renovationAppointment);
            return renovationAppointment;
        }

        private RenovationAppointment ScheduleRenovationForMergedRoom(RenovationAppointment renovationAppointment)
        {
            var scheduledAppointmentsForSecondRoomInMergeRenovation = GetAllScheduledAppointmentsByRoomId(renovationAppointment.RoomForMergeOrFirstRoomOfSplit.Id);
            UnscheduleAllScheduledAppointmentsAfter(renovationAppointment.StartDate, scheduledAppointmentsForSecondRoomInMergeRenovation);
            
            var mergedRoom = renovationAppointment.RoomForMergeOrFirstRoomOfSplit;
            var mergedRoomRenovationAppointment = new RenovationAppointment(mergedRoom, renovationAppointment.StartDate,
                DateTime.MaxValue, renovationAppointment.Description, _renovationTypeRepository.Get(1));

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
            var scheduledAppointmentsForRoomInStandardRenovation = GetAllScheduledAppointmentsByRoomId(renovationAppointment.MainRoom.Id);

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
                    if(ra.MainRoom.Id == roomId) listOfCorrectAppointments.Add(ra);
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
                renovationAppointment.MainRoom = FindRoomById(rooms, renovationAppointment.MainRoom.Id);
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
                    if (room.Id == renovationAppointment.MainRoom.Id)
                    {
                        renovationAppointment.MainRoom = room;
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
