using System;

namespace zdravstvena_ustanova.Model
{
    public class Address
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public Address(string street, string number, string city, string country)
        {
            Street = street;
            Number = number;
            City = city;
            Country = country;
        }
    }
}