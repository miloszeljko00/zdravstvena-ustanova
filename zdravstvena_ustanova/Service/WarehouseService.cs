    using Model;
using System;
using Repository;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class WarehouseService
    {
        private readonly WarehouseRepository _warehouseRepository;
        private readonly ItemRepository _itemRepository;
        private readonly ItemWarehouseRepository _itemWarehouseRepository;


        public WarehouseService(WarehouseRepository warehouseRepository, ItemRepository itemRepository, ItemWarehouseRepository itemWarehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
            _itemRepository = itemRepository;
            _itemWarehouseRepository = itemWarehouseRepository;
        }

        public IEnumerable<Warehouse> GetAll()
        {
            var items = _itemRepository.GetAll();
            var itemWarehouses = _itemWarehouseRepository.GetAll();
            var warehouses = _warehouseRepository.GetAll();

            BindItemsWithItemWarehouses(items, itemWarehouses);
            BindItemWarehousesWithWarehouses(itemWarehouses, warehouses);
            return warehouses;
        }
        public Warehouse GetById(long id)
        {
            var items = _itemRepository.GetAll();
            var itemWarehouses = _itemWarehouseRepository.GetAll();
            var warehouse = _warehouseRepository.Get(id);

            BindItemsWithItemWarehouses(items, itemWarehouses);
            BindItemWarehousesWithWarehouse(itemWarehouses, warehouse);
            return warehouse;
        }

        private void BindItemsWithItemWarehouses(IEnumerable<Item> items, IEnumerable<ItemWarehouse> itemWarehouses)
        {
            itemWarehouses.ToList().ForEach(itemWarehouse =>
            {
                itemWarehouse.Item = FindItemById(items, itemWarehouse.Item.Id);
            });
        }
        private Item FindItemById(IEnumerable<Item> items, long itemId)
        {
            return items.SingleOrDefault(item => item.Id == itemId);
        }
        private void BindItemWarehousesWithWarehouse(IEnumerable<ItemWarehouse> itemWarehouses, Warehouse warehouse)
        {
            itemWarehouses.ToList().ForEach(itemWarehouse =>
            {
                if (warehouse != null)
                {
                    if (warehouse.Id == itemWarehouse.WarehouseId)
                    {
                        warehouse.ItemWarehouses.Add(itemWarehouse);
                    }
                }
            });
        }

        private void BindItemWarehousesWithWarehouses(IEnumerable<ItemWarehouse> itemWarehouses, IEnumerable<Warehouse> warehouses)
        {
            itemWarehouses.ToList().ForEach(itemWarehouse =>
            {
                var warehouse = FindWarehouseById(warehouses, itemWarehouse.WarehouseId);
                if (warehouse != null)
                {
                    if (warehouse.Id == itemWarehouse.WarehouseId)
                    {
                        warehouse.ItemWarehouses.Add(itemWarehouse);
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