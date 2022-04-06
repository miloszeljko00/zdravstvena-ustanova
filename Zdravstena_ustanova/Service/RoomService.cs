using Model;
using System;
using System.Collections.Generic;
using Repository;
using System.Linq;

namespace Service
{
    public class RoomService
    {
        private readonly RoomRepository _roomRepository;
        private ItemRoomService _itemRoomService;

        public RoomService(RoomRepository roomRepository, ItemRoomService itemRoomService)
        {
            _roomRepository = roomRepository;
            _itemRoomService = itemRoomService;
        }

        internal IEnumerable<Room> GetAll()
        {
            var itemRooms = _itemRoomService.GetAll();
            var rooms = _roomRepository.GetAll();
            BindItemRoomsWithRooms(itemRooms, rooms);
            return rooms;
        }

        private void BindItemRoomsWithRooms(IEnumerable<ItemRoom> itemRooms, IEnumerable<Room> rooms)
        {
            itemRooms.ToList().ForEach(itemRoom =>
            {
                var room = FindRoomById(rooms, itemRoom.RoomId);
                room.ItemRooms.Add(itemRoom);
            });
        }

        private Room FindRoomById(IEnumerable<Room> rooms, long roomId)
        {
            return rooms.SingleOrDefault(room => room.Id == roomId);
        }

        public Room Create(Room room)
        {
            return _roomRepository.Create(room);
        }
        public void Update(Room room)
        {
            _roomRepository.Update(room);
        }
        public void Delete(long roomId)
        {
            _roomRepository.Delete(roomId);
        }
    }
}