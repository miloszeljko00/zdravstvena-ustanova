using Model;
using System;
using System.Collections.Generic;
using Repository;
using System.Linq;

namespace Service
{
    public class ItemService
    {
        private readonly ItemRepository _itemRepository;

        public ItemService(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        internal IEnumerable<Item> GetAll()
        {
            return _itemRepository.GetAll();
        }

        private Item FindItemById(IEnumerable<Item> items, long id)
        { 
            return items.SingleOrDefault(item => item.Id == id);
        }

        public Item Create(Item item)
        {
            return _itemRepository.Create(item);
        }
    }
}