using zdravstvena_ustanova.Model.Enums;
using System;

namespace zdravstvena_ustanova.Model
{
    public abstract class Staff : Person
    {
        

        public DateTime DateOfEmployment { get; set; }
        public int Experience { get; set; }

        public Shift Shift{ get; set; }

        public Staff(DateTime dateOfEmployment, int experience, string name,
            string surname, long id, string phoneNumber, string email, DateTime dateOfBirth,
            Address address, Account account, Shift shift) : base(name, surname, id, phoneNumber, email, dateOfBirth,
                address, account)
        {
            DateOfEmployment = dateOfEmployment;
            Experience = experience;
            Shift = shift;
        }

        public Staff(DateTime dateOfEmployment, int experience, string name, string surname, long id,
            string phoneNumber, string email, DateTime dateOfBirth, Address address, long accountId, Shift shift)
            : base(name, surname, id, phoneNumber, email, dateOfBirth, address, accountId)
        {
            DateOfEmployment = dateOfEmployment;
            Experience = experience;
            Shift= shift;
        }

        public Staff(long id):base(id)
        {

        }
    }
}