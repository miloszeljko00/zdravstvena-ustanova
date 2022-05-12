using zdravstvena_ustanova.Model;
using System;
using zdravstvena_ustanova.Repository;
using System.Collections.Generic;
using System.Linq;
using zdravstvena_ustanova.Model.Enums;

namespace zdravstvena_ustanova.Service
{
    public class WarehouseService
    {
        private readonly WarehouseRepository _warehouseRepository;
        private readonly ItemRepository _itemRepository;
        private readonly ItemTypeRepository _itemTypeRepository;
        private readonly StoredItemRepository _storedItemRepository;


        public WarehouseService(WarehouseRepository warehouseRepository, ItemRepository itemRepository,
            StoredItemRepository storedItemRepository, ItemTypeRepository itemTypeRepository)
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
        public Warehouse GetById(long id)
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