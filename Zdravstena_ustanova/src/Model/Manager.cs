using System;

namespace Model
{
    [Serializable]
    public class Manager : Staff
    {
        public Manager(string name, string surname, double id, string phoneNumber, string email, DateTime dateOfBirth, Address address, Account account, DateTime dateOfEmployment, int weeklyHours, int experience) : base(name, surname, id, phoneNumber, email, dateOfBirth, address, account, dateOfEmployment, weeklyHours, experience)
        {
        }
    }
}