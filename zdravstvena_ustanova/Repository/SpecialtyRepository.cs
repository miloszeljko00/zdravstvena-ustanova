using zdravstvena_ustanova.Exception;
using Model;
using Model.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repository
{
    public class SpecialtyRepository
    {
        private const string NOT_FOUND_ERROR = "SPECIALITY NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _specialtyMaxId;

        public SpecialtyRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _specialtyMaxId = GetMaxId(GetAll());
        }

        private long GetMaxId(IEnumerable<Specialty> specialties)
        {
            return specialties.Count() == 0 ? 0 : specialties.Max(specialties => specialties.Id);
        }

        public IEnumerable<Specialty> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToSpecialty)
                .ToList();
        }

        public Specialty Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(specialty => specialty.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public Specialty Create(Specialty specialty)
        {
            specialty.Id = ++_specialtyMaxId;
            AppendLineToFile(_path, SpecialtyToCSVFormat(specialty));
            return specialty;
        }

        public void Update(Specialty specialty)
        {
            var specialties = GetAll();

            foreach (Specialty s in specialties)
            {
                if(s.Id == specialty.Id)
                {
                    s.Name = specialty.Name;
                }
            }


            WriteLinesToFile(_path, SpecialtiesToCSVFormat((List<Specialty>)specialties));
        }
        public void Delete(long specialtyId)
        {
            var specialties = (List<Specialty>)GetAll();

            foreach (Specialty s in specialties)
            {
                if (s.Id == specialtyId)
                {
                   specialties.Remove(s);
                   break;
                }
            }


            WriteLinesToFile(_path, SpecialtiesToCSVFormat((List<Specialty>)specialties));
        }

        private string SpecialtyToCSVFormat(Specialty specialty)
        {
            return string.Join(_delimiter,
                specialty.Id,
                specialty.Name
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

        private Specialty CSVFormatToSpecialty(string specialtyCSVFormat)
        {
            var tokens = specialtyCSVFormat.Split(_delimiter.ToCharArray());

            return new Specialty(
                long.Parse(tokens[0]),
                tokens[1]);
        }

        private List<string> SpecialtiesToCSVFormat(List<Specialty> specialties)
        {
            List<string> lines = new List<string>();

            foreach (Specialty specialty in specialties)
            {
                lines.Add(SpecialtyToCSVFormat(specialty));
            }
            return lines;
        }
    }
}