using Model.Enums;
using System.Collections.Generic;
using System;

namespace Model
{
    public class Room
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Floor { get; set; }
        public RoomType RoomType { get; set; }
        public List<ItemRoom> ItemRooms { get; set; }

        public Room(long id, string name, int floor, RoomType roomType, List<ItemRoom> itemRooms)
        {
            Id = id;
            Name = name;
            Floor = floor;
            RoomType = roomType;
            ItemRooms = itemRooms;
        }

        public Room(long id, string name, int floor, RoomType roomType)
        {
            Id = id;
            Name = name;
            Floor = floor;
            RoomType = roomType;
            ItemRooms = new List<ItemRoom>();
        }
        public Room(long id)
        {
            Id=id;
        }
    }
}