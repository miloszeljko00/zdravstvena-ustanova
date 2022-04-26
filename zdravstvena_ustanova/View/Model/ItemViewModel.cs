using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdravstvena_ustanova.View.Model
{
    public class ItemViewModel
    {
        public Item Item { get; set; }
        public int TotalCount { get; set; }

        public ItemViewModel(Item item, int totalCount)
        {
            Item = item;
            TotalCount = totalCount;
        }
    }
}
