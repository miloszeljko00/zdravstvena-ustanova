using System;

namespace Model
{
    [Serializable]
    public abstract class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public double Id  { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Address Address { get; set; }
        public Account Account { get; set; }

        protected Person(string name, string surname, double id, string phoneNumber, string email, DateTime dateOfBirth, Address address, Account account)
        {
            Name = name;
            Surname = surname;
            Id = id;
            PhoneNumber = phoneNumber;
            Email = email;
            DateOfBirth = dateOfBirth;
            Address = address;
            Account = account;
        }
    }
}