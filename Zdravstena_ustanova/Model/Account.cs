using System;

namespace Model
{
    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsEnabled { get; set; } = true;

        public Person Person { get; set; }

        public long PersonId {get; set;}

        public Account(string username, string password, bool isEnabled, Person person, long personId)
        {
            Username = username;
            Password = password;
            IsEnabled = isEnabled;
            Person = person;
            PersonId = personId;
        }

        public Account(string username, string password, bool isEnabled, long personId)
        {
            Username = username;
            Password = password;
            IsEnabled = isEnabled;
            PersonId = personId;
        }
    }
}