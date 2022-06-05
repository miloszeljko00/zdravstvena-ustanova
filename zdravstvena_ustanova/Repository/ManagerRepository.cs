using zdravstvena_ustanova.Exception;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using zdravstvena_ustanova.Repository.RepositoryInterface;

namespace zdravstvena_ustanova.Repository
{
    public class ManagerRepository : IManagerRepository
    {
        private const string NOT_FOUND_ERROR = "MANAGER NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _managerMaxId;

        public ManagerRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _managerMaxId = GetMaxId(GetAll());
        }

        private long GetMaxId(IEnumerable<Manager> managers)
        {
            return managers.Count() == 0 ? 0 : managers.Max(managers => managers.Id);
        }

        public IEnumerable<Manager> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToManager)
                .ToList();
        }

        public Manager Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(manager => manager.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public Manager Create(Manager manager)
        {
            manager.Id = ++_managerMaxId;
            AppendLineToFile(_path, ManagerToCSVFormat(manager));
            return manager;
        }

        public bool Update(Manager manager)
        {
            var managers = GetAll();

            foreach (Manager m in managers)
            {
                if(m.Id == manager.Id)
                {
                    m.Name = manager.Name;
                    m.Surname = manager.Surname;
                    m.PhoneNumber = manager.PhoneNumber;
                    m.Email = manager.Email;
                    m.DateOfBirth = manager.DateOfBirth;
                    m.DateOfEmployment = manager.DateOfEmployment;
                    m.Experience = manager.Experience;
                    m.Address = manager.Address;
                    m.Account = manager.Account;
                    m.Account.Id = manager.Account.Id;
                    m.Shift = manager.Shift;
                }
            }


            WriteLinesToFile(_path, ManagersToCSVFormat((List<Manager>)managers));
            return true;
        }
        public bool Delete(long managerId)
        {
            var managers = (List<Manager>)GetAll();

            foreach (Manager m in managers)
            {
                if (m.Id == managerId)
                {
                    managers.Remove(m);
                   break;
                }
            }


            WriteLinesToFile(_path, ManagersToCSVFormat((List<Manager>)managers));
            return true;
        }

        private string ManagerToCSVFormat(Manager manager)
        {
            return string.Join(_delimiter,
                manager.Name,
                manager.Surname,
                manager.Id,
                manager.PhoneNumber,
                manager.Email,
                manager.DateOfBirth.ToString("dd.MM.yyyy."),
                manager.Address.Street,
                manager.Address.Number,
                manager.Address.City,
                manager.Address.Country,
                manager.Account.Id,
                manager.DateOfEmployment.ToString("dd.MM.yyyy."),
                manager.Experience,
                (int)manager.Shift
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

        private Manager CSVFormatToManager(string managerCSVFormat)
        {
            var tokens = managerCSVFormat.Split(_delimiter.ToCharArray());
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
            DateTime dateOfEmployment;
            DateTime.TryParseExact(tokens[11], timeFormat, CultureInfo.InvariantCulture
                                               , DateTimeStyles.None
                                               , out dateOfEmployment);

            return new Manager(
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

        private List<string> ManagersToCSVFormat(List<Manager> managers)
        {
            List<string> lines = new List<string>();

            foreach (Manager manager in managers)
            {
                lines.Add(ManagerToCSVFormat(manager));
            }
            return lines;
        }
    }
}