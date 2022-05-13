using System;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;
using System.Collections.Generic;

namespace zdravstvena_ustanova.Controller
{
   public class RoomController
   {
        private readonly RoomService _roomService;

        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }

        public IEnumerable<Room> GetAll()
        {
            return _roomService.GetAll();
        }
        public Room GetById(long id)
        {
            return _roomService.GetById(id);
        }
        public Room Create(Room room)
        {
            return _roomService.Create(room);
        }
        public bool Update(Room room)
        {
            return _roomService.Update(room);
        }
        public bool Delete(long roomId)
        {
            return _roomService.Delete(roomId);
        }

        public IEnumerable<StoredItem> FilterStoredItemsByName(long roomId, string searchText)
        {
            return _roomService.FilterStoredItemsByName(roomId, searchText);
        }

        public IEnumerable<StoredItem> FilterStoredItemsByType(long roomId, ItemType itemType)
        {
            return _roomService.FilterStoredItemsByType(roomId, itemType);
        }
    }
}