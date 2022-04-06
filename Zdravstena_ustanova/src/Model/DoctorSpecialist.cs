using System;

namespace Model
{
    [Serializable]
    public class DoctorSpecialist : Doctor
    {
        public Specialty Specialty { get; set; }

        public DoctorSpecialist(string name, string surname, double id, string phoneNumber, string email, DateTime dateOfBirth, Address address, Account account, DateTime dateOfEmployment, int weeklyHours, int experience, string licenceNumber, Specialty specialty) : base(name, surname, id, phoneNumber, email, dateOfBirth, address, account, dateOfEmployment, weeklyHours, experience, licenceNumber)
        {
                Specialty = specialty;
        }
    }
}