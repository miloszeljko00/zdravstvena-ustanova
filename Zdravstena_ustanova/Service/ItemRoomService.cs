using System;
using System.Collections.Generic;
using Repository;
using Model;
using System.Linq;

namespace Service
{
   public class ItemRoomService
   {

        private readonly ItemService _itemService;
        private readonly ItemRoomRepository _itemRoomRepository;

        public ItemRoomService(ItemRoomRepository itemRoomRepository, ItemService itemService)
        {
            _itemService = itemService;
            _itemRoomRepository = itemRoomRepository;
        }

        public IEnumerable<ItemRoom> GetAll()
        {
            var items = _itemService.GetAll();
            var itemRooms = _itemRoomRepository.GetAll();
            BindItemsWithItemRooms(items, itemRooms);
            return itemRooms;
        }

        private void BindItemsWithItemRooms(IEnumerable<Item> items, IEnumerable<ItemRoom> itemRooms)
        {
            itemRooms.ToList().ForEach(itemRoom =>
            {
                itemRoom.Item = FindItemById(items, itemRoom.Item.Id);
            });
        }

        private Item FindItemById(IEnumerable<Item> items, long itemId)
        {
            return items.SingleOrDefault(item => item.Id == itemId);
        }

        public ItemRoom Create(ItemRoom itemRoom)
        {
            return _itemRoomRepository.Create(itemRoom);
        }

        public bool Update(ItemRoom itemRoom)
        {
            return _itemRoomRepository.Update(itemRoom);
        }

        public bool Delete(long itemRoomId)
        {
            return _itemRoomRepository.Delete(itemRoomId);
        }
    }
}