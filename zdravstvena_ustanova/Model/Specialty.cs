using System;
namespace zdravstvena_ustanova.Model
{

    public class Specialty
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public Specialty(long id, string name)
        {
            Id = id;
            Name = name;
        }
        public Specialty(long id)
        {
            Id = id;
        }
    }
}