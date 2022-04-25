using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdravstvena_ustanova.Model
{
    public class Vaccine
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public Vaccine(long id, string name)
        {
            Id = id;
            Name = name;
        }
        public Vaccine(long id)
        {
            Id = id;
        }
    }
}
