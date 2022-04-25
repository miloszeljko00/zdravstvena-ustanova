using Model;
using System;
using System.Collections.Generic;
using Repository;
using System.Linq;
using Model.Enums;

namespace Service
{
    public class RoomService
    {
        private readonly RoomRepository _roomRepository;
        private readonly StoredItemRepository _storedItemRepository;
        private readonly ItemRepository _itemRepository;

        public RoomService(RoomRepository roomRepository, StoredItemRepository storedItemRepository, ItemRepository itemRepository)
        {
            _roomRepository = roomRepository;
            _storedItemRepository = storedItemRepository;
            _itemRepository = itemRepository;
        }

        public IEnumerable<Room> GetAll()
        {
            var items = _itemRepository.GetAll();
            var storedItems = _storedItemRepository.GetAll();
            var rooms = _roomRepository.GetAll();

            BindItemsWithStoredItems(items, storedItems);
            BindStoredItemsWithRooms(storedItems, rooms);
            return rooms;
        }
        public Room GetById(long id)
        {
            var items = _itemRepository.GetAll();
            var storedItems = _storedItemRepository.GetAll();
            var room = _roomRepository.Get(id);

            BindItemsWithStoredItems(items, storedItems);
            BindStoredItemsWithRoom(storedItems, room);
            return room;
        }
        private void BindItemsWithStoredItems(IEnumerable<Item> items, IEnumerable<StoredItem> storedItems)
        {
            storedItems.ToList().ForEach(storedItem =>
            {
                storedItem.Item = FindItemById(items, storedItem.Item.Id);
            });
        }
        private Item FindItemById(IEnumerable<Item> items, long itemId)
        {
            return items.SingleOrDefault(item => item.Id == itemId);
        }
        private void BindStoredItemsWithRoom(IEnumerable<StoredItem> storedItems, Room room)
        {
            storedItems.ToList().ForEach(storedItem =>
            {
                if (room != null)
                {
                    if (storedItem.StorageType == StorageType.ROOM)
                    {
                        if(room.Id == storedItem.Room.Id)
                        {
                            storedItem.Room = room;
                            room.StoredItems.Add(storedItem);
                        }
                    }
                }
            });
        }

        private void BindStoredItemsWithRooms(IEnumerable<StoredItem> storedItems, IEnumerable<Room> rooms)
        {
            storedItems.ToList().ForEach(storedItem =>
            {
                if (storedItem.StorageType == StorageType.ROOM)
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