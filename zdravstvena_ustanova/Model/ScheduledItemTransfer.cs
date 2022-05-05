using zdravstvena_ustanova.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdravstvena_ustanova.Model
{
    public class ScheduledItemTransfer
    {
        public long Id { get; set; }
        public Item Item { get; set; }
        public int ItemsForTransfer { get; set; }
        public StorageType SourceStorageType { get; set; }
        public Room SourceRoom { get; set; }
        public Warehouse SourceWarehouse { get; set; }
        public StorageType DestinationStorageType { get; set; }
        public Room DestinationRoom { get; set; }
        public Warehouse DestinationWarehouse { get; set; }
        public DateTime TransferDate { get; set; }

        public ScheduledItemTransfer(long id, Item item, int itemsForTransfer, Room sourceRoom,
           Room destinationRoom, DateTime transferDate)
        {
            Id = id;
            Item = item;
            ItemsForTransfer = itemsForTransfer;
            TransferDate = transferDate;

            SourceStorageType = StorageType.ROOM;
            DestinationStorageType = StorageType.ROOM;

            SourceRoom = sourceRoom;
            DestinationRoom = destinationRoom;
        }
        public ScheduledItemTransfer(Item item, int itemsForTransfer, Room sourceRoom,
           Room destinationRoom, DateTime transferDate)
        {
            Item = item;
            ItemsForTransfer = itemsForTransfer;
            TransferDate = transferDate;

            SourceStorageType = StorageType.ROOM;
            DestinationStorageType = StorageType.ROOM;

            SourceRoom = sourceRoom;
            DestinationRoom = destinationRoom;
        }
        public ScheduledItemTransfer(Item item, int itemsForTransfer, Room sourceRoom,
           DateTime transferDate)
        {
            Item = item;
            ItemsForTransfer = itemsForTransfer;
            TransferDate = transferDate;

            SourceStorageType = StorageType.ROOM;
            DestinationStorageType = StorageType.VOID;

            SourceRoom = sourceRoom;
        }
        public ScheduledItemTransfer(long id, Item item, int itemsForTransfer, Warehouse sourceWarehouse,
           Room destinationRoom, DateTime transferDate)
        {
            Id = id;
            Item = item;
            ItemsForTransfer = itemsForTransfer;
            TransferDate = transferDate;

            SourceStorageType = StorageType.WAREHOUSE;
            DestinationStorageType = StorageType.ROOM;

            SourceWarehouse = sourceWarehouse;
            DestinationRoom = destinationRoom;
        }
        public ScheduledItemTransfer(Item item, int itemsForTransfer, Warehouse sourceWarehouse,
          Room destinationRoom, DateTime transferDate)
        {
            Item = item;
            ItemsForTransfer = itemsForTransfer;
            TransferDate = transferDate;

            SourceStorageType = StorageType.WAREHOUSE;
            DestinationStorageType = StorageType.ROOM;

            SourceWarehouse = sourceWarehouse;
            DestinationRoom = destinationRoom;
        }
        public ScheduledItemTransfer(long id, Item item, int itemsForTransfer, Room sourceRoom,
           Warehouse destinationWarehouse, DateTime transferDate)
        {
            Id = id;
            Item = item;
            ItemsForTransfer = itemsForTransfer;
            TransferDate = transferDate;

            SourceStorageType = StorageType.ROOM;
            DestinationStorageType = StorageType.WAREHOUSE;

            SourceRoom = sourceRoom;
            DestinationWarehouse = destinationWarehouse;
        }
        public ScheduledItemTransfer(Item item, int itemsForTransfer, Room sourceRoom,
           Warehouse destinationWarehouse, DateTime transferDate)
        {
            Item = item;
            ItemsForTransfer = itemsForTransfer;
            TransferDate = transferDate;

            SourceStorageType = StorageType.ROOM;
            DestinationStorageType = StorageType.WAREHOUSE;

            SourceRoom = sourceRoom;
            DestinationWarehouse = destinationWarehouse;
        }
        public ScheduledItemTransfer(long id, long itemId, int itemsForTransfer, StorageType sourceStorageType, long sourceStorageId,
           StorageType destinationStorageType, long destinationStorageId, DateTime transferDate)
        {
            Id = id;
            Item = new Item(itemId);
            ItemsForTransfer = itemsForTransfer;
            TransferDate = transferDate;

            SourceStorageType = sourceStorageType;
            DestinationStorageType = destinationStorageType;

            if (SourceStorageType == StorageType.ROOM) SourceRoom = new Room(sourceStorageId);
            else if(SourceStorageType == StorageType.WAREHOUSE) SourceWarehouse = new Warehouse(sourceStorageId);

            if (DestinationStorageType == StorageType.ROOM) DestinationRoom = new Room(destinationStorageId);
            else if (DestinationStorageType == StorageType.WAREHOUSE) DestinationWarehouse = new Warehouse(destinationStorageId);
        }
        public ScheduledItemTransfer(long itemId, int itemsForTransfer, StorageType sourceStorageType, long sourceStorageId,
           StorageType destinationStorageType, long destinationStorageId, DateTime transferDate)
        {
            Item = new Item(itemId);
            ItemsForTransfer = itemsForTransfer;
            TransferDate = transferDate;

            SourceStorageType = sourceStorageType;
            DestinationStorageType = destinationStorageType;

            if (SourceStorageType == StorageType.ROOM) SourceRoom = new Room(sourceStorageId);
            else if (SourceStorageType == StorageType.WAREHOUSE)  SourceWarehouse = new Warehouse(sourceStorageId);

            if (DestinationStorageType == StorageType.ROOM) DestinationRoom = new Room(destinationStorageId);
            else if (SourceStorageType == StorageType.WAREHOUSE)  DestinationWarehouse = new Warehouse(destinationStorageId);
        }
    }
}
