using zdravstvena_ustanova.Model;
using System;
using zdravstvena_ustanova.Repository;
using System.Collections.Generic;
using System.Linq;
using zdravstvena_ustanova.Model.Enums;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IItemTypeRepository _itemTypeRepository;
        private readonly IStoredItemRepository _storedItemRepository;


        public WarehouseService(IWarehouseRepository warehouseRepository, IItemRepository itemRepository,
            IStoredItemRepository storedItemRepository, IItemTypeRepository itemTypeRepository)
        {
            _warehouseRepository = warehouseRepository;
            _itemRepository = itemRepository;
            _storedItemRepository = storedItemRepository;
            _itemTypeRepository = itemTypeRepository;
        }

        public IEnumerable<Warehouse> GetAll()
        {
            var items = _itemRepository.GetAll();
            var itemTypes = _itemTypeRepository.GetAll();
            var storedItems = _storedItemRepository.GetAll();
            var warehouses = _warehouseRepository.GetAll();
            BindItemsWithItemTypes(items, itemTypes);
            BindItemsWithStoredItems(items, storedItems);
            BindStoredItemsWithWarehouses(storedItems, warehouses);
            return warehouses;
        }
        public Warehouse Get(long id)
        {
            var items = _itemRepository.GetAll();
            var itemTypes = _itemTypeRepository.GetAll();
            var storedItems = _storedItemRepository.GetAll();
            var warehouse = _warehouseRepository.Get(id);
            BindItemsWithItemTypes(items, itemTypes);
            BindItemsWithStoredItems(items, storedItems);
            BindStoredItemsWithWarehouse(storedItems, warehouse);
            return warehouse;
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
        private void BindStoredItemsWithWarehouse(IEnumerable<StoredItem> storedItems, Warehouse warehouse)
        {
            storedItems.ToList().ForEach(storedItem =>
            {
                if (warehouse != null)
                {
                    if(storedItem.StorageType == StorageType.WAREHOUSE)
                    {
                        if (warehouse.Id == storedItem.Warehouse.Id)
                        {
                            storedItem.Warehouse = warehouse;
                            warehouse.StoredItems.Add(storedItem);
                        }
                    }
                }
            });
        }

        private void BindStoredItemsWithWarehouses(IEnumerable<StoredItem> storedItems, IEnumerable<Warehouse> warehouses)
        {
            storedItems.ToList().ForEach(storedItem =>
            {
                if (storedItem.StorageType == StorageType.WAREHOUSE)
                {
                    var warehouse = FindWarehouseById(warehouses, storedItem.Warehouse.Id);
                    if (warehouse != null)
                    {
                        if (warehouse.Id == storedItem.Warehouse.Id)
                        {
                            storedItem.Warehouse = warehouse;
                            warehouse.StoredItems.Add(storedItem);
                        }
                    }
                }
                
            });
        }
        private Warehouse FindWarehouseById(IEnumerable<Warehouse> warehouses, long warehouseId)
        {
            return warehouses.SingleOrDefault(warehouse => warehouse.Id == warehouseId);
        }

        public Warehouse Create(Warehouse warehouse)
        {
            return _warehouseRepository.Create(warehouse);
        }
        public bool Update(Warehouse warehouse)
        {
            return _warehouseRepository.Update(warehouse);
        }
        public bool Delete(long warehouseId)
        {
            return _warehouseRepository.Delete(warehouseId);
        }

    }
}