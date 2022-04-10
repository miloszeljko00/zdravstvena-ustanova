using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ItemWarehouse
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long WarehouseId { get; set; }
        public int Quantity { get; set; }
        public Item Item { get; set; }
        
        public ItemWarehouse(long id, long itemId, long warehouseId, int quantity)
        {
            Id = id;
            ItemId = itemId;
            WarehouseId = warehouseId;
            Quantity = quantity;
        }
        public ItemWarehouse(long id, long itemId, long warehouseId, int quantity, Item item)
        {
            Id = id;
            ItemId = itemId;
            WarehouseId = warehouseId;
            Quantity = quantity;
            Item = item;
        }
    }
}
