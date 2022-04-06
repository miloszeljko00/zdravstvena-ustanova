using System;
using System.Collections.Generic;

namespace Model
{
    [Serializable]
    public class Warehouse
    {
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
                        AddItems(oItem);
                }
            }
        }
        public void AddItems(Item newItem)
        {
            if (newItem == null)
                return;
            if (this.Items == null)
                this.Items = new System.Collections.Generic.List<Item>();
            if (!this.Items.Contains(newItem))
                this.Items.Add(newItem);
        }


        public void RemoveItems(Item oldItem)
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

    public Warehouse(List<Item> items)
        {
            Items = items;
        }

        public bool InsertItem(Model.Item item)
        {
            try { 
                
                Items.Add(item);
                return true;

            } catch (Exception ex) { return false; }
        }

        public bool RemoveItem(int itemId)
        {
            try
            {

                Items.Remove(FindByItemID(itemId));
                return true;

            }
            catch (Exception ex) { return false; }
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
            catch (Exception ex) { return null; }
        }
    }
}