using System;
namespace Model
{

    public class Specialty
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Specialty(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}