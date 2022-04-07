using System;

namespace Model
{
    public class DoctorSpecialist : Doctor
    {
        public Specialty Specialty { get; set; }

        public DoctorSpecialist(string name, string surname, long id, string phoneNumber, string email,
            DateTime dateOfBirth, Address address, Account account, DateTime dateOfEmployment, int weeklyHours,
            int experience, string licenceNumber, Specialty specialty) : base(name, surname, id, phoneNumber,
                email, dateOfBirth, address, account, account.Id, dateOfEmployment, weeklyHours, experience, licenceNumber)
        {
                Specialty = specialty;
        }
    }
}