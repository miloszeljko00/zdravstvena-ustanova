using System;
using System.Collections.Generic;

namespace Model
{
    public class Patient : Person
    {
        

        public Patient(string name, string surname, long id, string phoneNumber,
            string email, DateTime dateOfBirth, Address address, Account account)
            : base(name, surname, id, phoneNumber, email, dateOfBirth, address, account)
        {
            Account = account;
        }

        public Patient(string name, string surname, long id, string phoneNumber, string email, DateTime dateOfBirth,
            Address address, long accountId)
            : base(name, surname, id, phoneNumber, email, dateOfBirth, address, accountId)
        {
            
        }
        public Patient(string name, string surname, string phoneNumber, string email, DateTime dateOfBirth,
            Address address, long accountId)
            : base(name, surname, phoneNumber, email, dateOfBirth, address, accountId)
        {
        }
        public Patient(long id) : base(id)
        {

        }
        public Patient()
        {

        }

    }
}