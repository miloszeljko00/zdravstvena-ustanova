using System;

namespace Model
{
    [Serializable]
    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsEnabled { get; set; } = true;

        public Person Person { get; set; }

        public Account(string username, string password, bool isEnabled, Person person)
        {
            Username = username;
            Password = password;
            IsEnabled = isEnabled;
            Person = person;
        }
    }
}