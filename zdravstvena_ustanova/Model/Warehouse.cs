using System;
using System.Collections.Generic;

namespace Model
{
    public class Warehouse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<ItemWarehouse> ItemWarehouses { get; set; }

        public Warehouse(long id, string name, List<ItemWarehouse> itemWarehouses)
        {
            Id = id;
            Name = name;
            ItemWarehouses = itemWarehouses;
        }

        public Warehouse(long id, string name)
        {
            Id = id;
            Name = name;
            ItemWarehouses = new List<ItemWarehouse>();
        }
    }
}