using zdravstvena_ustanova.Exception;
using Model;
using Model.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;

namespace Repository
{
    public class PatientRepository
    {
        private const string NOT_FOUND_ERROR = "PATIENT NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _patientMaxId;

        public PatientRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _patientMaxId = GetMaxId(GetAll());
        }

        private long GetMaxId(IEnumerable<Patient> patients)
        {
            return patients.Count() == 0 ? 0 : patients.Max(patients => patients.Id);
        }

        public IEnumerable<Patient> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToPatient)
                .ToList();
        }

        public Patient Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(patient => patient.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public Patient Create(Patient patient)
        {
            patient.Id = ++_patientMaxId;
            AppendLineToFile(_path, PatientToCSVFormat(patient));
            return patient;
        }

        public void Update(Patient patient)
        {
            var patients = GetAll();

            foreach (Patient p in patients)
            {
                if(p.Id == patient.Id)
                {
                    p.Name = patient.Name;
                    p.Surname = patient.Surname;
                    p.PhoneNumber = patient.PhoneNumber;
                    p.Email = patient.Email;
                    p.DateOfBirth = patient.DateOfBirth;
                    p.Address = patient.Address;
                    p.Account = patient.Account;
                    p.Account.Id = patient.Account.Id;
                }
            }


            WriteLinesToFile(_path, PatientsToCSVFormat((List<Patient>)patients));
        }
        public void Delete(long pacientId)
        {
            var patients = (List<Patient>)GetAll();

            foreach (Patient p in patients)
            {
                if (p.Id == pacientId)
                {
                    patients.Remove(p);
                   break;
                }
            }


            WriteLinesToFile(_path, PatientsToCSVFormat((List<Patient>)patients));
        }

        private string PatientToCSVFormat(Patient patient)
        {
            return string.Join(_delimiter,
                patient.Name,
                patient.Surname,
                patient.Id,
                patient.PhoneNumber,
                patient.Email,
                patient.DateOfBirth.ToString("dd.MM.yyyy."),
                patient.Address.Street,
                patient.Address.Number,
                patient.Address.City,
                patient.Address.Country,
                patient.Account.Id  
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

        private Patient CSVFormatToPatient(string patientCSVFormat)
        {
            var tokens = patientCSVFormat.Split(_delimiter.ToCharArray());
            var address = new Address(
                    tokens[6],
                    tokens[7],
                    tokens[8],
                    tokens[9]);
            var timeFormat = "dd.MM.yyyy.";
            DateTime dateOfBirth;
            DateTime.TryParseExact(tokens[5], timeFormat, CultureInfo.InvariantCulture
                                               , DateTimeStyles.None
                                               , out dateOfBirth);

            return new Patient(
                tokens[0],
                tokens[1],
                long.Parse(tokens[2]),
                tokens[3],
                tokens[4],
                dateOfBirth,
            address,
                long.Parse(tokens[10]));
        }

        private List<string> PatientsToCSVFormat(List<Patient> patients)
        {
            List<string> lines = new List<string>();

            foreach (Patient patient in patients)
            {
                lines.Add(PatientToCSVFormat(patient));
            }
            return lines;
        }
    }
}