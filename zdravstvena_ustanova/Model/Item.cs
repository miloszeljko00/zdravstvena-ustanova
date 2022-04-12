using System;

namespace Model
{
    public class Item
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Item(long id, string name, string description)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
        }
        public Item(long id)
        {
            Id = id;
        }
        public Item(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}