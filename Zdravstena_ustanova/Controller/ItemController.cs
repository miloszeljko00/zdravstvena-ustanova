using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using Model;

namespace Controller
{
    public class ItemController
    {
        private readonly ItemService _itemService;

        public ItemController(ItemService itemService)
        {
            _itemService = itemService;
        }

        public IEnumerable<Item> GetAll()
        {
            return _itemService.GetAll();
        }

        public Item Create(Item item)
        {
            return _itemService.Create(item);
        }
        public bool Update(Item item)
        {
            return _itemService.Update(item);
        }
        public bool Delete(long itemId)
        {
            return _itemService.Delete(itemId);
        }
    }
}
