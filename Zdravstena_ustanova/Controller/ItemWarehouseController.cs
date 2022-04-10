using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using Model;

namespace Controller
{
    public class ItemWarehouseController
    {
        private readonly ItemWarehouseService _itemWarehouseService;

        public ItemWarehouseController(ItemWarehouseService itemWarehouseService)
        {
            _itemWarehouseService = itemWarehouseService;
        }

        public IEnumerable<ItemWarehouse> GetAll()
        {
            return _itemWarehouseService.GetAll();
        }

        public ItemWarehouse Create(ItemWarehouse itemWarehouse)
        {
            return _itemWarehouseService.Create(itemWarehouse);
        }
        public bool Update(ItemWarehouse itemWarehouse)
        {
            return _itemWarehouseService.Update(itemWarehouse);
        }
        public bool Delete(long itemWarehouseId)
        {
            return _itemWarehouseService.Delete(itemWarehouseId);
        }
    }
}
