using System;

namespace Model
{
    [Serializable]
    public class Patient : Person
    {
        public int InsuranceNumber { get; set; }
        public Enums.BloodType BloodType { get; set; }
        public Enums.EmploymentStatus EmploymentStatus { get; set; }

        public Patient(string name, string surname, double id, string phoneNumber, string email, DateTime dateOfBirth, Address address, Account account, int insuranceNumber, Enums.BloodType bloodType, Enums.EmploymentStatus employmentStatus) : base(name, surname, id, phoneNumber, email, dateOfBirth, address, account)
        {
            InsuranceNumber = insuranceNumber;
            BloodType = bloodType;
            EmploymentStatus = employmentStatus;
        }
    }
}