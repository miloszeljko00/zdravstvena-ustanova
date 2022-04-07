using System;

namespace Model
{
    public class Doctor : Staff
    {
        public string LicenseNumber { get; set; }

        public Doctor(string name, string surname, long id, string phoneNumber, string email, DateTime dateOfBirth, Address address, Account account, long accountId, DateTime dateOfEmployment, int weeklyHours, int experience, string licenceNumber) : base(name, surname, id, phoneNumber, email, dateOfBirth, address, account, accountId, dateOfEmployment, weeklyHours, experience)
        {
            LicenseNumber = licenceNumber;
        }

        public Doctor(string name, string surname, string phoneNumber, string email, DateTime dateOfBirth, Address address, Account account, long accountId, DateTime dateOfEmployment, int weeklyHours, int experience, string licenceNumber) : base(name, surname, phoneNumber, email, dateOfBirth, address, account, accountId, dateOfEmployment, weeklyHours, experience)
        {
            LicenseNumber = licenceNumber;
        }

        public Doctor(string name, string surname, long id, string phoneNumber, string email, DateTime dateOfBirth, Address address, long accountId, DateTime dateOfEmployment, int weeklyHours, int experience, string licenceNumber) : base(name, surname, id, phoneNumber, email, dateOfBirth, address, accountId, dateOfEmployment, weeklyHours, experience)
        {
            LicenseNumber = licenceNumber;
        }
    }
}