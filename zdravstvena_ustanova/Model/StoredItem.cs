using Model.Enums;
using System;

namespace Model
{
   public class StoredItem
   {
        public long Id { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public StorageType StorageType { get; set; }
        public Room Room { get; set; }
        public Warehouse Warehouse { get; set; }

        public StoredItem(long id, Item item, int quantity, StorageType storageType, Room room)
        {
            Id = id;
            Item = item;
            Quantity = quantity;
            StorageType = storageType;
            Room = room;
        }
        public StoredItem(long id, Item item, int quantity, StorageType storageType, Warehouse warehouse)
        {
            Id = id;
            Item = item;
            Quantity = quantity;
            StorageType = storageType;
            Warehouse = warehouse;
        }
        public StoredItem(long id, long itemId, int quantity, StorageType storageType, long roomOrWarehouseId)
        {
            Id = id;
            Item = new Item(itemId);
            Quantity = quantity;
            StorageType = storageType;
            if (StorageType == StorageType.ROOM) Room = new Room(roomOrWarehouseId);
            else Warehouse = new Warehouse(roomOrWarehouseId);

        }
        public StoredItem(long id)
        {
            Id = id;
        }
        public StoredItem(Item item, int quantity, StorageType storageType, Room room)
        {
            Item = item;
            Quantity = quantity;
            StorageType = storageType;
            Room = room;
        }
        public StoredItem(Item item, int quantity, StorageType storageType, Warehouse warehouse)
        {
            Item = item;
            Quantity = quantity;
            StorageType = storageType;
            Warehouse = warehouse;
        }
    }
}