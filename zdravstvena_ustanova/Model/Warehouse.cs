using System;
using System.Collections.Generic;

namespace Model
{
    public class Warehouse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<StoredItem> StoredItems { get; set; }

        public Warehouse(long id, string name, List<StoredItem> itemWarehouses)
        {
            Id = id;
            Name = name;
            StoredItems = itemWarehouses;
        }

        public Warehouse(long id, string name)
        {
            Id = id;
            Name = name;
            StoredItems = new List<StoredItem>();
        }

        public Warehouse(long id)
        {
            Id = id;
        }
    }
}