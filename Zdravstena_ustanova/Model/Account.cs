using Model.Enums;
using System;

namespace Model
{
    public class Account
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsEnabled { get; set; } = true;

        public Person Person { get; set; }
        public long PersonId { get; set; }


        public AccountType AccountType {get; set;}

        public Account(long id, string username, string password, bool isEnabled, Person person, long personId, AccountType accountType)
        {
            Id = id;
            Username = username;
            Password = password;
            IsEnabled = isEnabled;
            Person = person;
            PersonId = personId;
            AccountType = accountType;
        }
        public Account(long id, string username, string password, bool isEnabled, long personId, AccountType accountType)
        {
            Id = id;
            Username = username;
            Password = password;
            IsEnabled = isEnabled;
            PersonId = personId;
            AccountType = accountType;
        }

        public Account(long id)
        {
            Id = id;
        }
    }
}