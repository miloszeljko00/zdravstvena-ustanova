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


        public AccountType AccountType {get; set;}

        public Account(long id, string username, string password, bool isEnabled, Person person, AccountType accountType)
        {
            Id = id;
            Username = username;
            Password = password;
            IsEnabled = isEnabled;
            Person = person;
            AccountType = accountType;
        }
        public Account(string username, string password, bool isEnabled, Person person, AccountType accountType)
        {
            Username = username;
            Password = password;
            IsEnabled = isEnabled;
            Person = person;
            AccountType = accountType;
        }
        public Account(long id, string username, string password, bool isEnabled, long personId, AccountType accountType)
        {
            Id = id;
            Username = username;
            Password = password;
            IsEnabled = isEnabled;
            AccountType = accountType;

            switch (AccountType)
            {
                case AccountType.GUEST:
                case AccountType.PATIENT:
                    Person = new Patient(personId);
                    break;
                case AccountType.DOCTOR:
                    Person = new Doctor(personId);
                    break;
                case AccountType.MANAGER:
                    Person = new Manager(personId);
                    break;
                case AccountType.SECRETARY:
                    Person = new Secretary(personId);
                    break;
                default:
                    break;
            }
        }

        public Account(string username, string password, bool isEnabled, AccountType accountType)
        {
            Username = username;
            Password = password;
            IsEnabled = isEnabled;
            AccountType = accountType;
        }
        public Account(long id, string username, string password, bool isEnabled, AccountType accountType)
        {
            Id = id;
            Username = username;
            Password = password;
            IsEnabled = isEnabled;
            AccountType = accountType;
        }

        public Account(long id)
        {
            Id = id;
        }
    }
}