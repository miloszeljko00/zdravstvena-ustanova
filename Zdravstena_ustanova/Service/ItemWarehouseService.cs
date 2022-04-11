using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;

namespace Service
{
    public class ItemWarehouseService
    {
        private readonly ItemWarehouseRepository _itemWarehouseRepository;
        private readonly ItemRepository _itemRepository;

        public ItemWarehouseService(ItemWarehouseRepository itemWarehouseRepository, ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
            _itemWarehouseRepository = itemWarehouseRepository;
        }

        public IEnumerable<ItemWarehouse> GetAll()
        {
            var items = _itemRepository.GetAll();
            var itemWarehouses = _itemWarehouseRepository.GetAll();
            BindItemsWithItemWarehouses(items, itemWarehouses);
            return itemWarehouses;
        }
        public ItemWarehouse GetById(long id)
        {
            var items = _itemRepository.GetAll();
            var itemWarehouse = _itemWarehouseRepository.Get(id);
            BindItemWithItemWarehouse(items, itemWarehouse);
            return itemWarehouse;
        }

        private void BindItemWithItemWarehouse(IEnumerable<Item> items, ItemWarehouse itemWarehouse)
        { 
            itemWarehouse.Item = FindItemById(items, itemWarehouse.Item.Id); 
        }

        private void BindItemsWithItemWarehouses(IEnumerable<Item> items, IEnumerable<ItemWarehouse> itemWarehouses)
        {
            itemWarehouses.ToList().ForEach(itemWarehouse =>
            {
                BindItemWithItemWarehouse(items, itemWarehouse);
            });
        }

        private Item FindItemById(IEnumerable<Item> items, long itemId)
        {
            return items.SingleOrDefault(item => item.Id == itemId);
        }

        public ItemWarehouse Create(ItemWarehouse itemWarehouse)
        {
            return _itemWarehouseRepository.Create(itemWarehouse);
        }

        public bool Update(ItemWarehouse itemWarehouse)
        {
            return _itemWarehouseRepository.Update(itemWarehouse);
        }

        public bool Delete(long itemWarehouseId)
        {
            return _itemWarehouseRepository.Delete(itemWarehouseId);
        }
    }
}
