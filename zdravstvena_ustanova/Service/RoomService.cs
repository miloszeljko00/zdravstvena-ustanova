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
        private readonly ItemRoomRepository _itemRoomRepository;
        private readonly ItemRepository _itemRepository;

        public RoomService(RoomRepository roomRepository, ItemRoomRepository itemRoomRepository, ItemRepository itemRepository)
        {
            _roomRepository = roomRepository;
            _itemRoomRepository = itemRoomRepository;
            _itemRepository = itemRepository;
        }

        public IEnumerable<Room> GetAll()
        {
            var items = _itemRepository.GetAll();
            var itemRooms = _itemRoomRepository.GetAll();
            var rooms = _roomRepository.GetAll();

            BindItemsWithItemRooms(items, itemRooms);
            BindItemRoomsWithRooms(itemRooms, rooms);
            return rooms;
        }
        public Room GetById(long id)
        {
            var items = _itemRepository.GetAll();
            var itemRooms = _itemRoomRepository.GetAll();
            var room = _roomRepository.Get(id);

            BindItemsWithItemRooms(items, itemRooms);
            BindItemRoomsWithRoom(itemRooms, room);
            return room;
        }
        private void BindItemsWithItemRooms(IEnumerable<Item> items, IEnumerable<ItemRoom> itemRooms)
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