using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdravstvena_ustanova.Model
{
    public class ItemType
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ItemType(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public ItemType(long id)
        {
            Id = id;
        }

        public ItemType()
        {
            Id = -1;
            Name = "";
        }
    }
}
