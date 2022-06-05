using System.Collections.Generic;
using zdravstvena_ustanova.Model;
using System.Linq;
using zdravstvena_ustanova.Model.Enums;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
   public class StoredItemService : IStoredItemService
   {

        private readonly IItemRepository _itemRepository;
        private readonly IItemTypeRepository _itemTypeRepository;
        private readonly IStoredItemRepository _itemRoomRepository;

        public StoredItemService(IStoredItemRepository itemRoomRepository, IItemRepository itemRepository,
            IItemTypeRepository itemTypeRepository)
        {
            _itemRepository = itemRepository;
            _itemRoomRepository = itemRoomRepository;
            _itemTypeRepository = itemTypeRepository;
        }

        public IEnumerable<StoredItem> GetAll()
        {
            var items = _itemRepository.GetAll();
            var itemTypes = _itemTypeRepository.GetAll();
            var storedItems = _itemRoomRepository.GetAll();
            BindItemsWithStoredItems(items, storedItems);
            BindItemsWithItemTypes(items, itemTypes);
            return storedItems;
        }
        public StoredItem Get(long id)
        {
            var items = _itemRepository.GetAll();
            var itemTypes = _itemTypeRepository.GetAll();
            var storedItem = _itemRoomRepository.Get(id);
            BindItemWithStoredItem(items, storedItem);
            BindItemsWithItemTypes(items, itemTypes);
            return storedItem;
        }

        public StoredItem GetByWarehouseItemId(long id)
        {
            var storedItems = GetAll();
            StoredItem ret = null;
            foreach (StoredItem storedItem in storedItems)
            {
                if(storedItem.Item.Id == id && storedItem.StorageType == StorageType.WAREHOUSE)
                    ret = storedItem;
            }
            return ret;
        }
        private void BindItemsWithItemTypes(IEnumerable<Item> items, IEnumerable<ItemType> itemTypes)
        {
            foreach (var item in items)
            {
                BindItemWithItemTypes(item, itemTypes);
            }
        }

        private void BindItemWithItemTypes(Item item, IEnumerable<ItemType> itemTypes)
        {
            item.ItemType = FindItemTypeById(itemTypes, item.ItemType.Id);
        }

        private ItemType FindItemTypeById(IEnumerable<ItemType> itemTypes, long id)
        {
            return itemTypes.SingleOrDefault(itemType => itemType.Id == id);
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
            bool moved = false;
            foreach (var si in fromWarehouse.StoredItems)
            {
                if (si.Item.Id == item.Id)
                {
                    moved = MoveItemInRoomWhereItemExists(fromWarehouse, toRoom, quantity, si);

                    if (moved) break;

                    moved = MoveItemInRoomWhereItemDoesNotExist(fromWarehouse, toRoom, item, quantity, si);
                }
            }
            return moved;
        }

        private bool MoveItemInRoomWhereItemDoesNotExist(Warehouse fromWarehouse, Room toRoom, Item item, int quantity,
            StoredItem si)
        {
            si.Quantity -= quantity;
            UpdateOrDeleteItemFromWarehouse(fromWarehouse, si);

            var storedItem = Create(new StoredItem(item, quantity, StorageType.ROOM, toRoom));
            toRoom.StoredItems.Add(storedItem);
            return true;
        }

        private bool MoveItemInRoomWhereItemExists(Warehouse fromWarehouse, Room toRoom, int quantity, StoredItem si)
        {
            bool moved = false;
            foreach (StoredItem roomStoredItem in toRoom.StoredItems)
            {
                if (si.Item.Id == roomStoredItem.Item.Id)
                {
                    roomStoredItem.Quantity += quantity;
                    si.Quantity -= quantity;
                    UpdateOrDeleteItemFromWarehouse(fromWarehouse, si);

                    Update(roomStoredItem);
                    moved = true;
                }
            }

            return moved;
        }

        private void UpdateOrDeleteItemFromWarehouse(Warehouse fromWarehouse, StoredItem si)
        {
            if (si.Quantity == 0)
            {
                Delete(si.Id);
                fromWarehouse.StoredItems.Remove(si);
            }
            else
            {
                Update(si);
            }
        }

        public bool MoveItemFromTo(Room fromRoom, Room toRoom, Item item, int quantity)
        {
            bool moved = false;
            foreach (var si in fromRoom.StoredItems)
            {
                if (si.Item.Id == item.Id)
                {
                    moved = MoveItemFromRoomToRoom(fromRoom, toRoom, quantity, si);
                }
            }
            return moved;
        }

        private bool MoveItemFromRoomToRoom(Room fromRoom, Room toRoom, int quantity, StoredItem si)
        {
            if (si.Quantity == quantity)
            {
                MovingAllItems(fromRoom, toRoom, quantity, si);
            }
            else
            {
                MovingSomeOfItems(toRoom, quantity, si);
            }
            return true;
        }

        private void MovingSomeOfItems(Room toRoom, int quantity, StoredItem si)
        {
            si.Quantity -= quantity;
            StoredItem storedItem = new StoredItem(si.Item, quantity, StorageType.ROOM, toRoom);
            storedItem = Create(storedItem);
            toRoom.StoredItems.Add(storedItem);
            Update(si);
        }

        private void MovingAllItems(Room fromRoom, Room toRoom, int quantity, StoredItem si)
        {
            StoredItem storedItem = new StoredItem(si.Item, quantity, StorageType.ROOM, toRoom);
            storedItem = Create(storedItem);
            toRoom.StoredItems.Add(storedItem);
            Delete(si.Id);
            fromRoom.StoredItems.Remove(si);
        }

        public bool MoveItemFromTo(Room fromRoom, Warehouse toWarehouse, Item item, int quantity)
        {
            bool moved = false;
            foreach (var si in fromRoom.StoredItems)
            {
                if (si.Item.Id == item.Id)
                {
                    moved =  MoveItemFromRoomToWarehouse(fromRoom, toWarehouse, quantity, si);
                }
            }
            return moved;
        }

        private bool MoveItemFromRoomToWarehouse(Room fromRoom, Warehouse toWarehouse, int quantity, StoredItem si)
        {
            if (si.Quantity == quantity)
            {
                MovingAllItemsToWarehouse(fromRoom, toWarehouse, quantity, si);
            }
            else
            {
                MovingSomeOfItemsToWarehouse(toWarehouse, quantity, si);
            }
            return true;
        }

        private void MovingSomeOfItemsToWarehouse(Warehouse toWarehouse, int quantity, StoredItem si)
        {
            si.Quantity -= quantity;
            StoredItem storedItem = new StoredItem(si.Item, quantity, StorageType.WAREHOUSE, toWarehouse);
            storedItem = Create(storedItem);
            toWarehouse.StoredItems.Add(storedItem);
            Update(si);
        }

        private void MovingAllItemsToWarehouse(Room fromRoom, Warehouse toWarehouse, int quantity, StoredItem si)
        {
            StoredItem storedItem = new StoredItem(si.Item, quantity, StorageType.WAREHOUSE, toWarehouse);
            storedItem = Create(storedItem);
            toWarehouse.StoredItems.Add(storedItem);
            Delete(si.Id);
            fromRoom.StoredItems.Remove(si);
        }

        private void BindItemsWithStoredItems(IEnumerable<Item> items, IEnumerable<StoredItem> itemRooms)
        {
            itemRooms.ToList().ForEach(itemRoom =>
            {
                BindItemWithStoredItem(items, itemRoom);
            });
        }
        private void BindItemWithStoredItem(IEnumerable<Item> items, StoredItem itemRoom)
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