using System;

namespace Model
{
    public class Secretary : Staff
    {
        public Secretary(string name, string surname, long id, string phoneNumber, string email, DateTime dateOfBirth, Address address, long accountId, DateTime dateOfEmployment, int weeklyHours, int experience) : base(name, surname, id, phoneNumber, email, dateOfBirth, address, accountId, dateOfEmployment, weeklyHours, experience)
        {
        }

        public Secretary(string name, string surname, long id, string phoneNumber, string email, DateTime dateOfBirth, Address address, Account account, long accountId, DateTime dateOfEmployment, int weeklyHours, int experience) : base(name, surname, id, phoneNumber, email, dateOfBirth, address, account, accountId, dateOfEmployment, weeklyHours, experience)
        {
        }

    }

}