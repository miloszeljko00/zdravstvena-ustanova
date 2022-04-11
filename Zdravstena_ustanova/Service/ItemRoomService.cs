using System;
using System.Collections.Generic;
using Repository;
using Model;
using System.Linq;

namespace Service
{
   public class ItemRoomService
   {

        private readonly ItemRepository _itemRepository;
        private readonly ItemRoomRepository _itemRoomRepository;

        public ItemRoomService(ItemRoomRepository itemRoomRepository, ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
            _itemRoomRepository = itemRoomRepository;
        }

        public IEnumerable<ItemRoom> GetAll()
        {
            var items = _itemRepository.GetAll();
            var itemRooms = _itemRoomRepository.GetAll();
            BindItemsWithItemRooms(items, itemRooms);

            return itemRooms;
        }
        public ItemRoom GetById(long id)
        {
            var items = _itemRepository.GetAll();
            var itemRoom = _itemRoomRepository.Get(id);
            BindItemWithItemRoom(items, itemRoom);

            return itemRoom;
        }

        private void BindItemsWithItemRooms(IEnumerable<Item> items, IEnumerable<ItemRoom> itemRooms)
        {
            itemRooms.ToList().ForEach(itemRoom =>
            {
                BindItemWithItemRoom(items, itemRoom);
            });
        }
        private void BindItemWithItemRoom(IEnumerable<Item> items, ItemRoom itemRoom)
        {
            itemRoom.Item = FindItemById(items, itemRoom.Item.Id);
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