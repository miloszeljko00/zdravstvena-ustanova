using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdravstvena_ustanova.Model
{
    public class MedicationType
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public MedicationType(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public MedicationType(string name)
        {
            Name = name;
        }
        public MedicationType(long id)
        {
            Id = id;
        }
    }
}
