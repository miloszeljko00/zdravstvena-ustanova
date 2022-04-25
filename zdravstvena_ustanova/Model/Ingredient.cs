using System;

namespace Model
{
   public class Ingredient
   {
        public long Id { get; set; }
        public string Name { get; set; }

        public Ingredient(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public Ingredient(long id)
        {
            Id = id;
        }
    }
}