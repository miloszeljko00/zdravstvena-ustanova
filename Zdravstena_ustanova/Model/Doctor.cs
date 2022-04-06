using System;

namespace Model
{
    public class Doctor : Staff
    {
        public string LicenseNumber { get; set; }

        public Doctor(string name, string surname, double id, string phoneNumber, string email, DateTime dateOfBirth, Address address, Account account, DateTime dateOfEmployment, int weeklyHours, int experience, string licenceNumber) : base(name, surname, id, phoneNumber, email, dateOfBirth, address, account, dateOfEmployment, weeklyHours, experience)
        {
            LicenseNumber = licenceNumber;
        }



    }
}