using System;
using System.Collections.Generic;

namespace zdravstvena_ustanova.Model
{
    public class Medication
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public MedicationType MedicationType { get; set; }
        public int Quantity { get; set; }
        public bool IsApproved { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public Medication(long id, string name)
        {
            Id = id;
            Name = name;
            Ingredients = new List<Ingredient>();

        }

        public Medication(long id)
        {
            Id = id;
        }

        public Medication(long id, string name, List<Ingredient> ingredients)
        {
            Id = id;
            Name = name;
            Ingredients = ingredients;
        }
    }
}