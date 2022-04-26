using System;

namespace Model
{
   public class ItemRoom
   {
        public long Id { get; set; }
        public long RoomId { get; set; }
        public int Quantity { get; set; }
        public Item Item { get; set; }

        public ItemRoom(long id, long roomId, int quantity, Item item)
        {
            Id = id;
            RoomId = roomId;
            Quantity = quantity;
            Item = item;
        }
        public ItemRoom(long id, long roomId, long itemId, int quantity)
        {
            Id = id;
            RoomId = roomId;
            Item = new Item(itemId);
            Quantity = quantity;
        }

        public ItemRoom(long id)
        {
            Id = id;
        }
    }
}