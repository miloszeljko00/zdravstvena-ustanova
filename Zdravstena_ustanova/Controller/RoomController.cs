using System;
using Service;
using Model;
using System.Collections.Generic;

namespace Controller
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

        public Room Create(Room room)
        {
            return _roomService.Create(room);
        }
        public void Update(Room room)
        {
            _roomService.Update(room);
        }
        public void Delete(long roomId)
        {
            _roomService.Delete(roomId);
        }
    }
}