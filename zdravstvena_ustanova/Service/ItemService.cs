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
        private readonly ItemTypeRepository _itemTypeRepository;

        public ItemService(ItemRepository itemRepository, ItemTypeRepository itemTypeRepository)
        {
            _itemRepository = itemRepository;
            _itemTypeRepository = itemTypeRepository;
        }

        public IEnumerable<Item> GetAll()
        {
            var items = _itemRepository.GetAll();
            var itemTypes = _itemTypeRepository.GetAll();
            BindItemsWithItemTypes(items, itemTypes);
            return items;
        }

        private void BindItemsWithItemTypes(IEnumerable<Item> items, IEnumerable<ItemType> itemTypes)
        {
            foreach(var item in items)
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

        public Item GetById(long id)
        { 
            var item = _itemRepository.GetById(id);
            var itemTypes = _itemTypeRepository.GetAll();
            BindItemWithItemTypes(item, itemTypes);
            return item;
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