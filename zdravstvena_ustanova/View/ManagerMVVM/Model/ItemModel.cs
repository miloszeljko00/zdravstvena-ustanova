using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.View.ManagerMVVM.Model
{
    public class ItemModel : BindableBase
    {
        private Item _item;

        public Item Item
        {
            get => _item;
            set => SetProperty(ref _item, value);
        }

        private  int _totalCount;
        public int TotalCount
        {
            get => _totalCount;
            set => SetProperty(ref _totalCount, value);
        }

        public ItemModel(Item item, int totalCount)
        {
            Item = item;
            TotalCount = totalCount;
        }

        public ItemModel()
        {
            Item = new Item();
            TotalCount = 0;
        }
    }
}
