using System;
using System.Collections.Generic;

namespace Model
{
   public class Allergens
   {
      public long Id { get; set; }
      public string Name { get; set; }
      public List<Ingredient> Ingredients { get; set; }

        public Allergens(long id, string name)
        {
            Id = id;
            Name = name;
            Ingredients = new List<Ingredient>();
        }
        public Allergens(long id)
        {
            Id = id;
            Ingredients = new List<Ingredient>();
        }
        public Allergens(long id, string name, List<Ingredient> ingredients)
        {
            Id = id;
            Name = name;
            Ingredients = ingredients;
        }

    }
}