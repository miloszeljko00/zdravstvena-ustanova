using System;
using System.Collections.Generic;

namespace Model
{
    public class Medication
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public List<Ingredient> Ingredient { get; set; }

        public Medication(long id, string name)
        {
            Id = id;
            Name = name;
            Ingredient = new List<Ingredient>();

        }

        public Medication(long id)
        {
            Id = id;
        }
    }
}