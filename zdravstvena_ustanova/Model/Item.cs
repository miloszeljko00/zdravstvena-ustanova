using System;

namespace zdravstvena_ustanova.Model
{
    public class Item
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemType ItemType { get; set; }

        public Item(long id, string name, string description, ItemType itemType)
        {
            Id = id;
            Name = name;
            Description = description;
            ItemType = itemType;
        }
        public Item(string name, string description, ItemType itemType)
        {
            Name = name;
            Description = description;
            ItemType = itemType;
        }
        public Item(long id, string name, string description, long itemTypeId)
        {
            Id = id;
            Name = name;
            Description = description;
            ItemType = new ItemType(itemTypeId);
        }
        public Item(string name, string description, long itemTypeId)
        {
            Name = name;
            Description = description;
            ItemType = new ItemType(itemTypeId);
        }

        public Item(long id)
        {
            Id = id;
        }
    }
}