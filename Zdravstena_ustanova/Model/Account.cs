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

        public long PersonID { get; set; }

        public AccountType AccountType {get; set;}


        public Account(string username, string password, bool isEnabled, Person person, AccountType accountType)
        {
            Username = username;
            Password = password;
            IsEnabled = isEnabled;
            Person = person;
            PersonID = person.Id;
            AccountType = accountType;
        }

        public Account(long id, string username, string password, bool isEnabled, Person person, AccountType accountType)
        {
            Username = username;
            Password = password;
            IsEnabled = isEnabled;
            Id = id;
            PersonID = person.Id;
            AccountType = accountType;
            Person = person;
        }
        public Account(long id, string username, string password, bool isEnabled, long personId, AccountType accountType)
        {
            Username = username;
            Password = password;
            IsEnabled = isEnabled;
            Id = id;
            PersonID = personId;
            AccountType = accountType;
        }
        public Account(string username, string password, bool isEnabled, long personId, AccountType accountType)
        {
            Username = username;
            Password = password;
            IsEnabled = isEnabled;
            PersonID = personId;
            AccountType = accountType;
        }
        public Account(string username, string password, AccountType accountType)//za guest nalog
        {
            Username = username;
            Password = password;
            AccountType = accountType;
        }

    }
}