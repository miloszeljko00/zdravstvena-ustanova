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

        public IEnumerable<Room> GetAll()
        {
            var itemRooms = _itemRoomService.GetAll();
            var rooms = _roomRepository.GetAll();
            BindItemRoomsWithRooms(itemRooms, rooms);
            return rooms;
        }
        public Room GetById(long id)
        {
            var itemRooms = _itemRoomService.GetAll();
            var room = _roomRepository.Get(id);
            BindItemRoomsWithRoom(itemRooms, room);
            return room;
        }
        private void BindItemRoomsWithRoom(IEnumerable<ItemRoom> itemRooms, Room room)
        {
            itemRooms.ToList().ForEach(itemRoom =>
            {
                if (room != null)
                {
                    if (room.Id == itemRoom.RoomId)
                    {
                        room.ItemRooms.Add(itemRoom);
                    }
                }
            });
        }

        private void BindItemRoomsWithRooms(IEnumerable<ItemRoom> itemRooms, IEnumerable<Room> rooms)
        {
            itemRooms.ToList().ForEach(itemRoom =>
            {
                var room = FindRoomById(rooms, itemRoom.RoomId);
                if(room != null)
                {
                    if(room.Id == itemRoom.RoomId)
                    {
                        room.ItemRooms.Add(itemRoom);
                    }
                }
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
        public bool Update(Room room)
        {
            return _roomRepository.Update(room);
        }
        public bool Delete(long roomId)
        {
            return _roomRepository.Delete(roomId);
        }
    }
}