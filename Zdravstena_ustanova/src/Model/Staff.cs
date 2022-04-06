using System;

namespace Model
{
    [Serializable]
    public abstract class Staff : Person
    {
        public DateTime DateOfEmployment { get; set; }
        public int WeeklyHours { get; set; }
        public int Experience { get; set; }

        protected Staff(string name, string surname, double id, string phoneNumber, string email, DateTime dateOfBirth, Address address, Account account, DateTime dateOfEmployment, int weeklyHours, int experience) : base(name, surname, id, phoneNumber, email, dateOfBirth, address, account)
        {
            DateOfEmployment = dateOfEmployment;
            WeeklyHours = weeklyHours;
            Experience = experience;
        }
    }
}