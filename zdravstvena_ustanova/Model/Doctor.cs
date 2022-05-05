using zdravstvena_ustanova.Model.Enums;
using System;
using System.Collections.Generic;

namespace zdravstvena_ustanova.Model
{
    public class Doctor : Staff
    {
        public string LicenseNumber { get; set; }
        public Room Room { get; set; }
        public Specialty Specialty { get; set; }

        public Doctor(string licenceNumber, Room room, Specialty specialty, DateTime dateOfEmployment, int experience,
            string name, string surname, long id, string phoneNumber, string email, DateTime dateOfBirth,
            Address address, Account account, Shift shift) : base(dateOfEmployment, experience, name, surname, id,
                phoneNumber, email, dateOfBirth, address, account, shift)
        {
            LicenseNumber = licenceNumber;
            Room = room;
            Specialty = specialty;
        }

        public Doctor(string licenceNumber, long roomId, long specialityId, DateTime dateOfEmployment, int experience,
            string name, string surname, long id, string phoneNumber, string email, DateTime dateOfBirth,
            Address address, long accountId, Shift shift) : base(dateOfEmployment, experience, name, surname, id,
                phoneNumber, email, dateOfBirth, address, accountId, shift)
        {
            LicenseNumber = licenceNumber;
            Room = new Room(roomId);
            Specialty = new Specialty(specialityId);

        }

        public Doctor(long id):base(id)
        {
        }

    }
}