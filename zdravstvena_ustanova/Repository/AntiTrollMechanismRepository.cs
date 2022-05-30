using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using zdravstvena_ustanova.Exception;
using zdravstvena_ustanova.Repository.RepositoryInterface;

namespace zdravstvena_ustanova.Repository
{
    public class AntiTrollMechanismRepository : IAntiTrollMechanismRepository
    {
   
        private const string NOT_FOUND_ERROR = "ID NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _maxId;

        public AntiTrollMechanismRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _maxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<AntiTrollMechanism> antiTrollMechanisms)
        {
            return antiTrollMechanisms.Count() == 0 ? 0 : antiTrollMechanisms.Max(antiTrollMechanism => antiTrollMechanism.Id);
        }

        public IEnumerable<AntiTrollMechanism> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToAntiTrollMechanism)
                .ToList();
        }

        public AntiTrollMechanism Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(antiTrollMechanism => antiTrollMechanism.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public AntiTrollMechanism Create(AntiTrollMechanism antiTrollMechanism)
        {
            antiTrollMechanism.Id = ++_maxId;
            AppendLineToFile(_path, AntiTrollMechanismToCSVFormat(antiTrollMechanism));
            return antiTrollMechanism;
        }
        public bool Update(AntiTrollMechanism antiTrollMechanism)
        {
            var antiTrollMechanisms = GetAll();

            foreach (AntiTrollMechanism atm in antiTrollMechanisms)
            {
                if (atm.Id == antiTrollMechanism.Id)
                {
                    atm.Patient = antiTrollMechanism.Patient;
                    atm.NumberOfDates = antiTrollMechanism.NumberOfDates;
                    atm.DatesOfCanceledAppointments = antiTrollMechanism.DatesOfCanceledAppointments;
                    WriteLinesToFile(_path, AntiTrollMechanismsToCSVFormat((List<AntiTrollMechanism>)antiTrollMechanisms));
                    return true;
                }
            }
            return false;
        }
        private string AntiTrollMechanismToCSVFormat(AntiTrollMechanism antiTrollMechanism)
        {
            string dates = antiTrollMechanism.DatesOfCanceledAppointments[0].ToString();
            for (int i = 1; i < antiTrollMechanism.NumberOfDates; i++)
            {
                dates = string.Join(_delimiter, dates, antiTrollMechanism.DatesOfCanceledAppointments[i]);
            }

            return string.Join(_delimiter,
                antiTrollMechanism.Id,
                antiTrollMechanism.Patient.Id,
                antiTrollMechanism.NumberOfDates,
                dates
                );
        }

        private AntiTrollMechanism CSVFormatToAntiTrollMechanism(string antiTrollMechanismCSVFormat)
        {
            var tokens = antiTrollMechanismCSVFormat.Split(_delimiter.ToCharArray());
            List<DateTime> dates = new List<DateTime>();
            for (int i = 3; i < 3 + int.Parse(tokens[2]); i++)
            {
                var date = Convert.ToDateTime(tokens[i]);
                dates.Add(date);
            }

            return new AntiTrollMechanism(
                long.Parse(tokens[0]),
                long.Parse(tokens[1]),
                int.Parse(tokens[2]),
                dates);

        }
        private List<string> AntiTrollMechanismsToCSVFormat(List<AntiTrollMechanism> antiTrollMechanisms)
        {
            List<string> lines = new List<string>();

            foreach (AntiTrollMechanism antiTrollMechanism in antiTrollMechanisms)
            {
                lines.Add(AntiTrollMechanismToCSVFormat(antiTrollMechanism));
            }
            return lines;
        }

        private void AppendLineToFile(string path, string line)
        {
            File.AppendAllText(path, line + Environment.NewLine);
        }

        private void WriteLinesToFile(string path, List<string> lines)
        {
            File.WriteAllLines(path, lines);
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}