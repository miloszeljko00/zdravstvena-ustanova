using Model;
using Model.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Exception;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Repository
{
    public class VaccineRepository
    {
        private const string NOT_FOUND_ERROR = "PRESCRIBED MEDICINE NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _vaccineMaxId;

        public VaccineRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _vaccineMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<Vaccine> vaccines)
        {
            return vaccines.Count() == 0 ? 0 : vaccines.Max(vaccine => vaccine.Id);
        }

        public IEnumerable<Vaccine> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToVaccine)
                .ToList();
        }

        public Vaccine Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(vaccine => vaccine.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public Vaccine Create(Vaccine vaccine)
        {
            vaccine.Id = ++_vaccineMaxId;
            AppendLineToFile(_path, VaccineToCSVFormat(vaccine));
            return vaccine;
        }

        public bool Update(Vaccine vaccine)
        {
            var vaccines = GetAll();

            foreach (Vaccine v in vaccines)
            {
                if (v.Id == vaccine.Id)
                {
                    v.Name=vaccine.Name;
                    WriteLinesToFile(_path, VaccinesToCSVFormat((List<Vaccine>)vaccines));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long vaccineId)
        {
            var vaccines = (List<Vaccine>)GetAll();

            foreach (Vaccine v in vaccines)
            {
                if (v.Id == vaccineId)
                {
                    vaccines.Remove(v);
                    WriteLinesToFile(_path, VaccinesToCSVFormat((List<Vaccine>)vaccines));
                    return true;
                }
            }
            return false;
        }
        private void WriteLinesToFile(string path, List<string> lines)
        {
            File.WriteAllLines(path, lines);
        }

        private void AppendLineToFile(string path, string line)
        {
            File.AppendAllText(path, line + Environment.NewLine);
        }

        private string VaccineToCSVFormat(Vaccine vaccine)
        {
            return string.Join(_delimiter,
                vaccine.Id,
                vaccine.Name
                );
        }
        private List<string> VaccinesToCSVFormat(List<Vaccine> vaccines)
        {
            List<string> lines = new List<string>();

            foreach (Vaccine v in vaccines)
            {
                lines.Add(VaccineToCSVFormat(v));
            }
            return lines;
        }

        private Vaccine CSVFormatToVaccine(string vaccineCSVFormat)
        {
            var tokens = vaccineCSVFormat.Split(_delimiter.ToCharArray());
            return new Vaccine(
                long.Parse(tokens[0]),
                tokens[1]
                );
        }

    }
}
