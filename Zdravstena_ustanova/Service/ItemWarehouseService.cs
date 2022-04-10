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
        private readonly ItemService _itemService;

        public ItemWarehouseService(ItemWarehouseRepository itemWarehouseRepository, ItemService itemService)
        {
            _itemService = itemService;
            _itemWarehouseRepository = itemWarehouseRepository;
        }

        public IEnumerable<ItemWarehouse> GetAll()
        {
            var items = _itemService.GetAll();
            var itemWarehouses = _itemWarehouseRepository.GetAll();
            BindItemsWithItemWarehouses(items, itemWarehouses);
            return itemWarehouses;
        }

        private void BindItemsWithItemWarehouses(IEnumerable<Item> items, IEnumerable<ItemWarehouse> itemWarehouses)
        {
            itemWarehouses.ToList().ForEach(itemWarehouse =>
            {
                itemWarehouse.Item = FindItemById(items, itemWarehouse.ItemId);
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
