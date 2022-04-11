using System;

namespace Model
{
    public class Secretary : Staff
    {
        public Secretary(string name, string surname, long id, string phoneNumber,
            string email, DateTime dateOfBirth, Address address, long accountId, DateTime dateOfEmployment,
            int experience) : base(dateOfEmployment, experience, name, surname, id,
                phoneNumber, email, dateOfBirth, address, accountId)
        {
        }

        public Secretary(string name, string surname, long id, string phoneNumber, string email,
            DateTime dateOfBirth, Address address, Account account, long accountId, DateTime dateOfEmployment,
            int experience) : base(dateOfEmployment, experience, name, surname, id,
                phoneNumber, email, dateOfBirth, address, account)
        {
        }

        public Secretary(long id) : base(id)
        {
        }

    }

}