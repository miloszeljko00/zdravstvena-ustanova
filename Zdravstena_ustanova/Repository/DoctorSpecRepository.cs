using Zdravstena_ustanova.Exception;
using Model;
using Model.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repository
{
    public class DoctorSpecRepository
    {
        private const string NOT_FOUND_ERROR = "ROOM NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _doctorSpecMaxId;

        public DoctorSpecRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _doctorSpecMaxId = GetMaxId(GetAll());
        }

        private long GetMaxId(IEnumerable<DoctorSpecialist> doctors)
        {
            return doctors.Count() == 0 ? 0 : doctors.Max(doctors => doctors.Id);
        }

        public IEnumerable<DoctorSpecialist> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToDoctorSpec)
                .ToList();
        }

        public DoctorSpecialist Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(doctor => doctor.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public DoctorSpecialist Create(DoctorSpecialist doctor)
        {
            doctor.Id = ++_doctorSpecMaxId;
            AppendLineToFile(_path, DoctorSpecToCSVFormat(doctor));
            return doctor;
        }

        public void Update(DoctorSpecialist doctor)
        {
            var doctors = GetAll();

            foreach (DoctorSpecialist d in doctors)
            {
                if(d.Id == doctor.Id)
                {
                    d.Name = doctor.Name;
                    d.Surname = doctor.Surname;
                    d.PhoneNumber = doctor.PhoneNumber;
                    d.Email = doctor.Email;
                    d.DateOfBirth = d.DateOfBirth;
                    d.DateOfEmployment = doctor.DateOfEmployment;
                    d.WeeklyHours = doctor.WeeklyHours;
                    d.Experience = doctor.Experience;
                    d.LicenseNumber = doctor.LicenseNumber;
                    d.Address = doctor.Address;
                    d.Account = doctor.Account;
                    d.AccountId = doctor.AccountId;
                    d.SpecialtyId = doctor.SpecialtyId;
                }
            }


            WriteLinesToFile(_path, DoctorsSpecToCSVFormat((List<DoctorSpecialist>)doctors));
        }
        public void Delete(long doctorId)
        {
            var doctors = (List<DoctorSpecialist>)GetAll();

            foreach (DoctorSpecialist d in doctors)
            {
                if (d.Id == doctorId)
                {
                   doctors.Remove(d);
                   break;
                }
            }


            WriteLinesToFile(_path, DoctorsSpecToCSVFormat((List<DoctorSpecialist>)doctors));
        }

        private string DoctorSpecToCSVFormat(DoctorSpecialist doctor)
        {
            return string.Join(_delimiter,
                doctor.Name,
                doctor.Surname,
                doctor.Id,
                doctor.PhoneNumber,
                doctor.Email,
                doctor.DateOfBirth.ToString("dd.MM.yyyy"),
                doctor.Address.Street,
                doctor.Address.Number,
                doctor.Address.City,
                doctor.Address.Country,
                doctor.AccountId,
                doctor.DateOfEmployment.ToString("dd.MM.yyyy"),
                doctor.WeeklyHours,
                doctor.Experience,
                doctor.LicenseNumber,
                doctor.SpecialtyId
               );
        }

        private void AppendLineToFile(string path, string line)
        {
            File.AppendAllText(path, line + Environment.NewLine);
        }

        private void WriteLinesToFile(string path, List<string> lines)
        {
            File.WriteAllLines(path, lines);
        }

        private DoctorSpecialist CSVFormatToDoctorSpec(string doctorCSVFormat)
        {
            var tokens = doctorCSVFormat.Split(_delimiter.ToCharArray());
            var address = new Address(
                    tokens[6],
                    tokens[7],
                    tokens[8],
                    tokens[9]);

            return new DoctorSpecialist(
                tokens[0],
                tokens[1],
                long.Parse(tokens[2]),
                tokens[3],
                tokens[4],
                Convert.ToDateTime(tokens[5]),
                address,
                long.Parse(tokens[10]),
                Convert.ToDateTime(tokens[11]),
                int.Parse(tokens[12]),
                int.Parse(tokens[13]),
                tokens[14],
                long.Parse(tokens[15]));
        }

        private List<string> DoctorsSpecToCSVFormat(List<DoctorSpecialist> doctors)
        {
            List<string> lines = new List<string>();

            foreach (DoctorSpecialist doctor in doctors)
            {
                lines.Add(DoctorSpecToCSVFormat(doctor));
            }
            return lines;
        }
    }
}