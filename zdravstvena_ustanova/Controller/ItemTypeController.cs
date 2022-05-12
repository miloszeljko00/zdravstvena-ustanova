using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Service;

namespace zdravstvena_ustanova.Controller
{
    public class ItemTypeController
    {
        private readonly ItemTypeService _itemTypeService;

        public ItemTypeController(ItemTypeService itemTypeService)
        {
            _itemTypeService = itemTypeService;
        }

        public IEnumerable<ItemType> GetAll()
        {
            return _itemTypeService.GetAll();
        }

        public ItemType Create(ItemType itemType)
        {
            return _itemTypeService.Create(itemType);
        }
        public bool Update(ItemType itemType)
        {
            return _itemTypeService.Update(itemType);
        }
        public bool Delete(long itemTypeId)
        {
            return _itemTypeService.Delete(itemTypeId);
        }
    }
}
