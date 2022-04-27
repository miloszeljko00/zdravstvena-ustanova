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
        public List<StoredItem> StoredItems { get; set; }

        public Room(long id, string name, int floor, RoomType roomType, List<StoredItem> itemRooms)
        {
            Id = id;
            Name = name;
            Floor = floor;
            RoomType = roomType;
            StoredItems = itemRooms;
        }

        public Room(long id, string name, int floor, RoomType roomType)
        {
            Id = id;
            Name = name;
            Floor = floor;
            RoomType = roomType;
            StoredItems = new List<StoredItem>();
        }
        public Room(string name, int floor, RoomType roomType)
        {
            Name = name;
            Floor = floor;
            RoomType = roomType;
            StoredItems = new List<StoredItem>();
        }
        public Room(long id)
        {
            Id=id;
        }
    }
}