using System;
using System.Collections.Generic;
using Model;
using Service;

namespace Controller
{
   public class ItemRoomController
   {
      private readonly Service.ItemRoomService _itemRoomService;
        public ItemRoomController(ItemRoomService itemRoomService)
        {
            _itemRoomService = itemRoomService;
        }

        public IEnumerable<ItemRoom> GetAll()
        {
            return _itemRoomService.GetAll();
        }

        public ItemRoom Create(ItemRoom itemRoom)
        {
            return _itemRoomService.Create(itemRoom);
        }
    }
}