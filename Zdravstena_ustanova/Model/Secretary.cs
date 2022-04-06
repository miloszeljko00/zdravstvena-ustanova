using System;

namespace Model
{
    public class Secretary : Staff
    {
        public Secretary(string name, string surname, double id, string phoneNumber, string email, DateTime dateOfBirth, Address address, Account account, DateTime dateOfEmployment, int weeklyHours, int experience) : base(name, surname, id, phoneNumber, email, dateOfBirth, address, account, dateOfEmployment, weeklyHours, experience)
        {
        }
    }

}