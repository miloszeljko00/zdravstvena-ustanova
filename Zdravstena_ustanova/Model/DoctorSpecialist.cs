using System;

namespace Model
{
    public class DoctorSpecialist : Doctor
    {

        public Specialty Specialty { get; set; }
        public long SpecialtyId { get; set; }

        public DoctorSpecialist(string name, string surname, long id, string phoneNumber, string email, DateTime dateOfBirth, Address address, long accountId, DateTime dateOfEmployment, int weeklyHours, int experience, string licenceNumber, long specialtyId) : base(name, surname, id, phoneNumber, email, dateOfBirth, address, accountId, dateOfEmployment, weeklyHours, experience, licenceNumber)
        {
            SpecialtyId = specialtyId;
        }

    }
}