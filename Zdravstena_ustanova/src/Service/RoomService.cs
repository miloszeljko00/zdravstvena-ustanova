using Model;
using System;
using System.Collections.Generic;

namespace Service
{
    public class RoomService
    {
        private void MoveItemsToWarehouse()
        {
            throw new NotImplementedException();
        }

        private void DeleteItems()
        {
            throw new NotImplementedException();
        }

        public bool CreateRoom(Room room)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRoom(Room room)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRoom(int roomId)
        {
            throw new NotImplementedException();
        }

        public Room GetRoomById(int roomId)
        {
            throw new NotImplementedException();
        }

        public List<Room> GetAllRooms()
        {
            throw new NotImplementedException();
        }

        public Repository.RoomRepository roomRepository;
        public ItemService itemService;

    }
}