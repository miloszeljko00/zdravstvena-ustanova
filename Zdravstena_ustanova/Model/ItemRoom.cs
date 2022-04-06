using System;

namespace Model
{
   public class ItemRoom
   {
        public long Id { get; set; }
        public long RoomId { get; set; }
        public long ItemId { get; set; }
        public int Quantity { get; set; }
        public Item Item { get; set; }

        public ItemRoom(long id, long roomId, long itemId, int quantity, Item item)
        {
            Id = id;
            RoomId = roomId;
            ItemId = itemId;
            Quantity = quantity;
            Item = item;
        }
        public ItemRoom(long id, long roomId, long itemId, int quantity)
        {
            Id = id;
            RoomId = roomId;
            ItemId = itemId;
            Quantity = quantity;
        }

        public ItemRoom(long roomId, long itemId, int quantity)
        {
            RoomId = roomId;
            ItemId = itemId;
            Quantity = quantity;
        }
    }
}