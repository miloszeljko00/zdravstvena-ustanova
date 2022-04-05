using Model.Enums;
using System.Collections.Generic;
using System;

namespace Model
{
    [Serializable]
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Floor { get; set; }
        public Enums.RoomType RoomType { get; set; }

        public Room(int id, string name, int floor, RoomType roomType, List<Item> items)
        {
            Id = id;
            Name = name;
            Floor = floor;
            RoomType = roomType;
            Items = items;
        }

        public System.Collections.Generic.List<Item> Items
        {
            get
            {
                if (Items == null)
                    Items = new System.Collections.Generic.List<Item>();
                return Items;
            }
            set
            {
                RemoveAllItems();
                if (value != null)
                {
                    foreach (Item oItem in value)
                        AddItem(oItem);
                }
            }
        }


        public void AddItem(Item newItem)
        {
            if (newItem == null)
                return;
            if (this.Items == null)
                this.Items = new System.Collections.Generic.List<Item>();
            if (!this.Items.Contains(newItem))
                this.Items.Add(newItem);
        }


        public void RemoveItem(Item oldItem)
        {
            if (oldItem == null)
                return;
            if (this.Items != null)
                if (this.Items.Contains(oldItem))
                    this.Items.Remove(oldItem);
        }


        public void RemoveAllItems()
        {
            if (Items != null)
                Items.Clear();
        }

        public Model.Item FindByItemID(int itemId)
        {
            try
            {

                foreach (Model.Item item in Items)
                {
                    if (item.Id == itemId)
                    {
                        return item;
                    }
                }
                return null;

            }
            catch (System.Exception ex) { return null; }
        }
    }

}