using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Enums;
using Repository;

namespace Service
{
    public class ScheduledItemTransferService
    {
        private readonly ScheduledItemTransferRepository _scheduledItemTransferRepository;
        private readonly RoomRepository _roomRepository;
        private readonly WarehouseRepository _warehouseRepository;
        private readonly ItemRepository _itemRepository;
        private readonly StoredItemRepository _storedItemRepository;

        public ScheduledItemTransferService(ScheduledItemTransferRepository scheduledItemTransferRepository, RoomRepository roomRepository,
                         WarehouseRepository warehouseRepository, ItemRepository itemRepository, StoredItemRepository storedItemRepository)
        {
            _scheduledItemTransferRepository = scheduledItemTransferRepository;
            _roomRepository = roomRepository;
            _warehouseRepository = warehouseRepository;
            _itemRepository = itemRepository;
            _storedItemRepository = storedItemRepository;
        }

        public IEnumerable<ScheduledItemTransfer> GetAll()
        {
            var rooms = _roomRepository.GetAll();
            var warehouses = _warehouseRepository.GetAll();
            var items = _itemRepository.GetAll();
            var storedItems = _storedItemRepository.GetAll();
            var scheduledItemTransfers = _scheduledItemTransferRepository.GetAll();

            BindItemsWithStoredItems(items, storedItems);
            BindRoomsAndWarehousesWithStoredItems(rooms, warehouses, storedItems);
            BindScheduledItemTransfersWithItemsAndRoomsAndWarehouses(scheduledItemTransfers, items, rooms, warehouses);
            
            return scheduledItemTransfers;
        }

        public void ScheduleItemTransferFromRoomToWarehouse(ScheduledItemTransfer scheduledItemTransfer)
        {
            throw new NotImplementedException();
        }

        public ScheduledItemTransfer GetById(long id)
        {
            var rooms = _roomRepository.GetAll();
            var warehouses = _warehouseRepository.GetAll();
            var items = _itemRepository.GetAll();
            var storedItems = _storedItemRepository.GetAll();
            var scheduledItemTransfer = _scheduledItemTransferRepository.Get(id);

            BindItemsWithStoredItems(items, storedItems);
            BindRoomsAndWarehousesWithStoredItems(rooms, warehouses, storedItems);
            BindScheduledItemTransferWithItemsAndRoomsAndWarehouses(scheduledItemTransfer, items, rooms, warehouses);

            return scheduledItemTransfer;
        }

        private void BindScheduledItemTransfersWithItemsAndRoomsAndWarehouses(IEnumerable<ScheduledItemTransfer> scheduledItemTransfers,
            IEnumerable<Item> items, IEnumerable<Room> rooms, IEnumerable<Warehouse> warehouses)
        {
            scheduledItemTransfers.ToList().ForEach(scheduledItemTransfer =>
                BindScheduledItemTransferWithItemsAndRoomsAndWarehouses(scheduledItemTransfer, items, rooms, warehouses)
            );
        }
        private void BindScheduledItemTransferWithItemsAndRoomsAndWarehouses(ScheduledItemTransfer scheduledItemTransfer,
            IEnumerable<Item> items, IEnumerable<Room> rooms, IEnumerable<Warehouse> warehouses)
        {
            scheduledItemTransfer.Item = FindItemById(items, scheduledItemTransfer.Item.Id);

            if (scheduledItemTransfer.SourceStorageType == StorageType.ROOM)
            {
                scheduledItemTransfer.SourceRoom = FindRoomById(rooms, scheduledItemTransfer.SourceRoom.Id);
            }
            else
            {
                scheduledItemTransfer.SourceWarehouse = FindWarehouseById(warehouses, scheduledItemTransfer.SourceWarehouse.Id);
            }

            if (scheduledItemTransfer.DestinationStorageType == StorageType.ROOM)
            {
                scheduledItemTransfer.DestinationRoom = FindRoomById(rooms, scheduledItemTransfer.DestinationRoom.Id);
            }
            else
            {
                scheduledItemTransfer.DestinationWarehouse = FindWarehouseById(warehouses, scheduledItemTransfer.DestinationWarehouse.Id);
            }
        }
        private void BindRoomsAndWarehousesWithStoredItems(IEnumerable<Room> rooms, IEnumerable<Warehouse> warehouses,
            IEnumerable<StoredItem> storedItems)
        {
            storedItems.ToList().ForEach(storedItem =>
                {
                    if (storedItem.StorageType == StorageType.ROOM)
                    {
                        storedItem.Room = FindRoomById(rooms, storedItem.Room.Id);
                        storedItem.Room.StoredItems.Add(storedItem);
                    }
                    else
                    {
                        storedItem.Warehouse = FindWarehouseById(warehouses, storedItem.Warehouse.Id);
                        storedItem.Warehouse.StoredItems.Add(storedItem);
                    }
                }
            );
        }

        private void BindItemsWithStoredItems(IEnumerable<Item> items, IEnumerable<StoredItem> storedItems)
        {
            storedItems.ToList().ForEach(storedItem =>
            storedItem.Item = FindItemById(items, storedItem.Item.Id)
            );
        }

        private Room FindRoomById(IEnumerable<Room> rooms, long roomId)
        {
            return rooms.SingleOrDefault(room => room.Id == roomId);
        }
        private Warehouse FindWarehouseById(IEnumerable<Warehouse> warehouses, long warehouseId)
        {
            return warehouses.SingleOrDefault(warehouse => warehouse.Id == warehouseId);
        }
        private Item FindItemById(IEnumerable<Item> items, long itemId)
        {
            return items.SingleOrDefault(item => item.Id == itemId);
        }

        public ScheduledItemTransfer Create(ScheduledItemTransfer scheduledItemTransfer)
        {
            return _scheduledItemTransferRepository.Create(scheduledItemTransfer);
        }
        public void Update(ScheduledItemTransfer scheduledItemTransfer)
        {
            _scheduledItemTransferRepository.Update(scheduledItemTransfer);
        }
        public void Delete(long scheduledItemTransferId)
        {
            _scheduledItemTransferRepository.Delete(scheduledItemTransferId);
        }
    }
}
