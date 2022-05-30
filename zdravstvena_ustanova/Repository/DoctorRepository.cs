using zdravstvena_ustanova.Exception;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using zdravstvena_ustanova.Repository.RepositoryInterface;

namespace zdravstvena_ustanova.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private const string NOT_FOUND_ERROR = "DOCTOR NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _doctorMaxId;

        public DoctorRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _doctorMaxId = GetMaxId(GetAll());
        }

        private long GetMaxId(IEnumerable<Doctor> doctors)
        {
            return doctors.Count() == 0 ? 0 : doctors.Max(doctors => doctors.Id);
        }

        public IEnumerable<Doctor> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToDoctor)
                .ToList();
        }

        public Doctor Get(long id)
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

        public Doctor Create(Doctor doctor)
        {
            doctor.Id = ++_doctorMaxId;
            AppendLineToFile(_path, DoctorToCSVFormat(doctor));
            return doctor;
        }

        public bool Update(Doctor doctor)
        {
            var doctors = GetAll();

            foreach (Doctor d in doctors)
            {
                if(d.Id == doctor.Id)
                {
                    d.Name = doctor.Name;
                    d.Surname = doctor.Surname;
                    d.PhoneNumber = doctor.PhoneNumber;
                    d.Email = doctor.Email;
                    d.DateOfBirth = d.DateOfBirth;
                    d.DateOfEmployment = doctor.DateOfEmployment;
                    d.Experience = doctor.Experience;
                    d.LicenseNumber = doctor.LicenseNumber;
                    d.Address = doctor.Address;
                    d.Account = doctor.Account;
                    d.Specialty = doctor.Specialty;
                    d.Shift = doctor.Shift;
                }
            }


            WriteLinesToFile(_path, DoctorsToCSVFormat((List<Doctor>)doctors));
            return true;
        }
        public bool Delete(long doctorId)
        {
            var doctors = (List<Doctor>)GetAll();

            foreach (Doctor d in doctors)
            {
                if (d.Id == doctorId)
                {
                   doctors.Remove(d);
                   break;
                }
            }


            WriteLinesToFile(_path, DoctorsToCSVFormat((List<Doctor>)doctors));
            return true;
        }

        private string DoctorToCSVFormat(Doctor doctor)
        {
            return string.Join(_delimiter,
                doctor.LicenseNumber,
                doctor.Room.Id,
                doctor.Specialty.Id,
                doctor.DateOfEmployment.ToString("dd.MM.yyyy."),
                doctor.Experience,
                doctor.Name,
                doctor.Surname,
                doctor.Id,
                doctor.PhoneNumber,
                doctor.Email,
                doctor.DateOfBirth.ToString("dd.MM.yyyy."),
                doctor.Address.Street,
                doctor.Address.Number,
                doctor.Address.City,
                doctor.Address.Country,
                doctor.Account.Id,
                (int)doctor.Shift
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

        private Doctor CSVFormatToDoctor(string doctorCSVFormat)
        {
            var tokens = doctorCSVFormat.Split(_delimiter.ToCharArray());
            var address = new Address(
                    tokens[11],
                    tokens[12],
                    tokens[13],
                    tokens[14]);

            return new Doctor(
                tokens[0],
                long.Parse(tokens[1]),
                long.Parse(tokens[2]),
                Convert.ToDateTime(tokens[3]),
                int.Parse(tokens[4]),
                tokens[5],
                tokens[6],
                long.Parse(tokens[7]),
                tokens[8],
                tokens[9],
                Convert.ToDateTime(tokens[10]),
                address,
                long.Parse(tokens[15]),
                (Shift)int.Parse(tokens[16]));
                
        }

        private List<string> DoctorsToCSVFormat(List<Doctor> doctors)
        {
            List<string> lines = new List<string>();

            foreach (Doctor doctor in doctors)
            {
                lines.Add(DoctorToCSVFormat(doctor));
            }
            return lines;
        }
    }
}