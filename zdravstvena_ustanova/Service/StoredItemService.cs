using System;
using System.Collections.Generic;
using Repository;
using Model;
using System.Linq;
using Model.Enums;

namespace Service
{
   public class StoredItemService
   {

        private readonly ItemRepository _itemRepository;
        private readonly StoredItemRepository _itemRoomRepository;

        public StoredItemService(StoredItemRepository itemRoomRepository, ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
            _itemRoomRepository = itemRoomRepository;
        }

        public IEnumerable<StoredItem> GetAll()
        {
            var items = _itemRepository.GetAll();
            var itemRooms = _itemRoomRepository.GetAll();
            BindItemsWithItemRooms(items, itemRooms);

            return itemRooms;
        }
        public StoredItem GetById(long id)
        {
            var items = _itemRepository.GetAll();
            var itemRoom = _itemRoomRepository.Get(id);
            BindItemWithItemRoom(items, itemRoom);

            return itemRoom;
        }

        public int GetTotalItemCount(Item item)
        {
            int totalCount = 0;

            var storedItems = GetAll();
            foreach (var storedItem in storedItems)
            {
                if (storedItem.Item.Id == item.Id)
                {
                    totalCount += storedItem.Quantity;
                }
            }

            return totalCount;
        }

        public bool MoveItemFromTo(Warehouse fromWarehouse, Room toRoom, Item item, int quantity)
        {
            foreach (var si in fromWarehouse.StoredItems)
            {
                if (si.Item.Id == item.Id)
                {
                    foreach(StoredItem roomStoredItem in toRoom.StoredItems)
                    {
                        if (si.Item.Id == roomStoredItem.Item.Id)
                        {
                            roomStoredItem.Quantity += quantity;
                            si.Quantity -= quantity;
                            if (si.Quantity == 0)
                            {
                                Delete(si.Id);
                                fromWarehouse.StoredItems.Remove(si);
                            }
                            else
                            {
                                Update(si);
                            }
                            Update(roomStoredItem);
                            return true;
                        }
                    }


                    si.Quantity -= quantity;
                    if (si.Quantity == 0)
                    {
                        Delete(si.Id);
                        fromWarehouse.StoredItems.Remove(si);
                    }
                    else
                    {
                        Update(si);
                    }
                    var storedItem = Create(new StoredItem(item, quantity, StorageType.ROOM, toRoom));
                    toRoom.StoredItems.Add(storedItem);
                    return true;
                }
            }
            return false;
        }

        public bool MoveItemFromTo(Room fromRoom, Room toRoom, Item item, int quantity)
        {
            foreach (var si in fromRoom.StoredItems)
            {
                if (si.Item.Id == item.Id)
                {
                    if (si.Quantity == quantity)
                    {
                        StoredItem storedItem = new StoredItem(si.Item, quantity, StorageType.ROOM, toRoom);
                        storedItem = Create(storedItem);
                        toRoom.StoredItems.Add(storedItem);
                        Delete(si.Id);
                        fromRoom.StoredItems.Remove(si);
                        return true;
                    }
                    else
                    {
                        si.Quantity -= quantity;
                        StoredItem storedItem = new StoredItem(si.Item, quantity, StorageType.ROOM, toRoom);
                        storedItem = Create(storedItem);
                        toRoom.StoredItems.Add(storedItem);
                        Update(si);
                        return true;
                    }
                }
            }
            return false;
        }
        public bool MoveItemFromTo(Room fromRoom, Warehouse toWarehouse, Item item, int quantity)
        {
            foreach (var si in fromRoom.StoredItems)
            {
                if (si.Item.Id == item.Id)
                {
                    if (si.Quantity == quantity)
                    {
                        StoredItem storedItem = new StoredItem(si.Item, quantity, StorageType.WAREHOUSE, toWarehouse);
                        storedItem = Create(storedItem);
                        toWarehouse.StoredItems.Add(storedItem);
                        Delete(si.Id);
                        fromRoom.StoredItems.Remove(si);
                        return true;
                    }
                    else
                    {
                        si.Quantity -= quantity;
                        StoredItem storedItem = new StoredItem(si.Item, quantity, StorageType.WAREHOUSE, toWarehouse);
                        storedItem = Create(storedItem);
                        toWarehouse.StoredItems.Add(storedItem);
                        Update(si);
                        return true;
                    }
                }
            }
            return false;
        }
        private void BindItemsWithItemRooms(IEnumerable<Item> items, IEnumerable<StoredItem> itemRooms)
        {
            itemRooms.ToList().ForEach(itemRoom =>
            {
                BindItemWithItemRoom(items, itemRoom);
            });
        }
        private void BindItemWithItemRoom(IEnumerable<Item> items, StoredItem itemRoom)
        {
            itemRoom.Item = FindItemById(items, itemRoom.Item.Id);
        }

        private Item FindItemById(IEnumerable<Item> items, long itemId)
        {
            return items.SingleOrDefault(item => item.Id == itemId);
        }

        public StoredItem Create(StoredItem itemRoom)
        {
            return _itemRoomRepository.Create(itemRoom);
        }

        public bool Update(StoredItem itemRoom)
        {
            return _itemRoomRepository.Update(itemRoom);
        }

        public bool Delete(long itemRoomId)
        {
            return _itemRoomRepository.Delete(itemRoomId);
        }
    }
}