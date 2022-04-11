using System;

namespace Model
{
    public abstract class Staff : Person
    {
        

        public DateTime DateOfEmployment { get; set; }
        public int Experience { get; set; }

        public Staff(DateTime dateOfEmployment, int experience, string name,
            string surname, long id, string phoneNumber, string email, DateTime dateOfBirth,
            Address address, Account account) : base(name, surname, id, phoneNumber, email, dateOfBirth,
                address, account)
        {
            DateOfEmployment = dateOfEmployment;
            Experience = experience;
        }

        public Staff(DateTime dateOfEmployment, int experience, string name, string surname, long id,
            string phoneNumber, string email, DateTime dateOfBirth, Address address, long accountId)
            : base(name, surname, id, phoneNumber, email, dateOfBirth, address, accountId)
        {
            DateOfEmployment = dateOfEmployment;
            Experience = experience;
        }

        public Staff(long id):base(id)
        {

        }
    }
}