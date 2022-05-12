using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdravstvena_ustanova.Model
{
    public class RenovationType
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public RenovationType(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public RenovationType(long id)
        {
            Id = id;
        }
    }
}
