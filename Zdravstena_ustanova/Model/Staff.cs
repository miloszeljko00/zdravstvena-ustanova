using System;

namespace Model
{
    public abstract class Staff : Person
    {
        public DateTime DateOfEmployment { get; set; }
        public int WeeklyHours { get; set; }
        public int Experience { get; set; }

        public Staff(string name, string surname, long id, string phoneNumber, string email, DateTime dateOfBirth, Address address, Account account, long accountId, DateTime dateOfEmployment, int weeklyHours, int experience) : base(name, surname, id, phoneNumber, email, dateOfBirth, address, account, accountId)
        {
            DateOfEmployment = dateOfEmployment;
            WeeklyHours = weeklyHours;
            Experience = experience;
        }

        protected Staff(string name, string surname, string phoneNumber, string email, DateTime dateOfBirth, Address address, Account account, long accountId, DateTime dateOfEmployment, int weeklyHours, int experience) : base(name, surname, phoneNumber, email, dateOfBirth, address, account, accountId)
        {
            DateOfEmployment = dateOfEmployment;
            WeeklyHours = weeklyHours;
            Experience = experience;
        }

        protected Staff(string name, string surname, long id, string phoneNumber, string email, DateTime dateOfBirth, Address address, long accountId, DateTime dateOfEmployment, int weeklyHours, int experience) : base(name, surname, id, phoneNumber, email, dateOfBirth, address, accountId)
        {
            DateOfEmployment = dateOfEmployment;
            WeeklyHours = weeklyHours;
            Experience = experience;
        }
    }
}