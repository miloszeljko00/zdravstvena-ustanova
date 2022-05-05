using System;

namespace zdravstvena_ustanova.Model
{
   public class Disease
   {
        public long Id { get; set; }
        public string Name { get; set; }

        public Disease(long id, string name)
        {
            Id = id;
            Name = name;
        }
        public Disease(long id)
        {
            Id = id;
        }
    }
}