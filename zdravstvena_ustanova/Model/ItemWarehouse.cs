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
        public long WarehouseId { get; set; }
        public int Quantity { get; set; }
        public Item Item { get; set; }
        
        public ItemWarehouse(long id)
        {
            Id = id;
        }
        public ItemWarehouse(long id, long warehouseId, int quantity, Item item)
        {
            Id = id;
            WarehouseId = warehouseId;
            Quantity = quantity;
            Item = item;
        }
        public ItemWarehouse(long id, long warehouseId, int quantity, long itemId)
        {
            Id = id;
            WarehouseId = warehouseId;
            Quantity = quantity;
            Item = new Item(itemId);
        }
    }
}
