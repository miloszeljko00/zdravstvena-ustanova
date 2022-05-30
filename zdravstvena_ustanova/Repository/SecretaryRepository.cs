using zdravstvena_ustanova.Exception;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Windows.Navigation;
using zdravstvena_ustanova.Repository.RepositoryInterface;

namespace zdravstvena_ustanova.Repository
{
    public class SecretaryRepository : ISecretaryRepository
    {
        private const string NOT_FOUND_ERROR = "SECRETARY NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _secretaryMaxId;

        public SecretaryRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _secretaryMaxId = GetMaxId(GetAll());
        }

        private long GetMaxId(IEnumerable<Secretary> secretaries)
        {
            return secretaries.Count() == 0 ? 0 : secretaries.Max(secretaries => secretaries.Id);
        }

        public IEnumerable<Secretary> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToSecretary)
                .ToList();
        }

        public Secretary Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(secretary => secretary.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public Secretary Create(Secretary secretary)
        {
            secretary.Id = ++_secretaryMaxId;
            AppendLineToFile(_path, SecretaryToCSVFormat(secretary));
            return secretary;
        }

        public bool Update(Secretary secretary)
        {
            var secretaries = GetAll();

            foreach (Secretary s in secretaries)
            {
                if(s.Id == secretary.Id)
                {
                    s.Name = secretary.Name;
                    s.Surname = secretary.Surname;
                    s.PhoneNumber = secretary.PhoneNumber;
                    s.Email = secretary.Email;
                    s.DateOfBirth = secretary.DateOfBirth;
                    s.DateOfEmployment = secretary.DateOfEmployment;
                    s.Experience = secretary.Experience;
                    s.Address = secretary.Address;
                    s.Account = secretary.Account;
                    s.Account.Id = secretary.Account.Id;
                    s.Shift = secretary.Shift;
                }
            }


            WriteLinesToFile(_path, SecretariesToCSVFormat((List<Secretary>)secretaries));
            return true;
        }
        public bool Delete(long secretaryId)
        {
            var secretaries = (List<Secretary>)GetAll();

            foreach (Secretary s in secretaries)
            {
                if (s.Id == secretaryId)
                {
                    secretaries.Remove(s);
                   break;
                }
            }


            WriteLinesToFile(_path, SecretariesToCSVFormat((List<Secretary>)secretaries));
            return true;
        }

        private string SecretaryToCSVFormat(Secretary secretary)
        {
            return string.Join(_delimiter,
                secretary.Name,
                secretary.Surname,
                secretary.Id,
                secretary.PhoneNumber,
                secretary.Email,
                secretary.DateOfBirth.ToString("dd.MM.yyyy."),
                secretary.Address.Street,
                secretary.Address.Number,
                secretary.Address.City,
                secretary.Address.Country,
                secretary.Account.Id,
                secretary.DateOfEmployment.ToString("dd.MM.yyyy."),
                secretary.Experience,
                (int)secretary.Shift
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

        private Secretary CSVFormatToSecretary(string secretaryCSVFormat)
        {
            var tokens = secretaryCSVFormat.Split(_delimiter.ToCharArray());
            var timeFormat = "dd.MM.yyyy.";
            DateTime dateOfBirth;
            DateTime.TryParseExact(tokens[5], timeFormat, CultureInfo.InvariantCulture
                                               , DateTimeStyles.None
                                               , out dateOfBirth);
            DateTime dateOfEmployment;
            DateTime.TryParseExact(tokens[11], timeFormat, CultureInfo.InvariantCulture
                                               , DateTimeStyles.None
                                               , out dateOfEmployment);
            var address = new Address(
                    tokens[6],
                    tokens[7],
                    tokens[8],
                    tokens[9]);

            return new Secretary(
                tokens[0],
                tokens[1],
                long.Parse(tokens[2]),
                tokens[3],
                tokens[4],
                dateOfBirth,
                address,
                long.Parse(tokens[10]),
                dateOfEmployment,
                int.Parse(tokens[12]),
                (Shift)int.Parse(tokens[13]));
        }

        private List<string> SecretariesToCSVFormat(List<Secretary> secretaries)
        {
            List<string> lines = new List<string>();

            foreach (Secretary secretary in secretaries)
            {
                lines.Add(SecretaryToCSVFormat(secretary));
            }
            return lines;
        }
    }
}