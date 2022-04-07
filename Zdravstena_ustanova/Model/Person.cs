using System;

namespace Model
{
 
    public abstract class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public long Id  { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Address Address { get; set; }
        public Account Account { get; set; }

        public long AccountId { get; set; }

        public Person(string name, string surname, long id, string phoneNumber, string email, DateTime dateOfBirth, Address address, Account account, long accountId)
        {
            Name = name;
            Surname = surname;
            Id = id;
            PhoneNumber = phoneNumber;
            Email = email;
            DateOfBirth = dateOfBirth;
            Address = address;
            Account = account;
            AccountId = accountId;
        }

        protected Person(string name, string surname, string phoneNumber, string email, DateTime dateOfBirth, Address address, Account account, long accountId)
        {
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
            Email = email;
            DateOfBirth = dateOfBirth;
            Address = address;
            Account = account;
            AccountId = accountId;
        }

        protected Person(string name, string surname, long id, string phoneNumber, string email, DateTime dateOfBirth, Address address, long accountId)
        {
            Name = name;
            Surname = surname;
            Id = id;
            PhoneNumber = phoneNumber;
            Email = email;
            DateOfBirth = dateOfBirth;
            Address = address;
            AccountId = accountId;
        }
    }
}