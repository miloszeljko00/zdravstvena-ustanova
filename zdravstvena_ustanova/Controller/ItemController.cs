using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Controller
{
    public class ItemController
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
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
