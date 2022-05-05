using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using zdravstvena_ustanova.Repository;
using System.Linq;

namespace zdravstvena_ustanova.Service
{
    public class ItemService
    {
        private readonly ItemRepository _itemRepository;

        public ItemService(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public IEnumerable<Item> GetAll()
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
        public bool Update(Item item)
        {
            return _itemRepository.Update(item);
        }
        public bool Delete(long itemId)
        {
            return _itemRepository.Delete(itemId);
        }
    }
}