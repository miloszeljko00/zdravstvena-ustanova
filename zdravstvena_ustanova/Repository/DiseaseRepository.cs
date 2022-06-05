using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using zdravstvena_ustanova.Exception;
using zdravstvena_ustanova.Repository.RepositoryInterface;

namespace zdravstvena_ustanova.Repository
{
    public class DiseaseRepository : IDiseaseRepository
    {
        private const string NOT_FOUND_ERROR = "INGREDIENT NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _diseaseMaxId;

        public DiseaseRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _diseaseMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<Disease> diseases)
        {
            return diseases.Count() == 0 ? 0 : diseases.Max(disease => disease.Id);
        }

        public IEnumerable<Disease> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToDisease)
                .ToList();
        }

        public Disease Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(disease => disease.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public Disease Create(Disease disease)
        {
            disease.Id = ++_diseaseMaxId;
            AppendLineToFile(_path, DiseaseToCSVFormat(disease));
            return disease;
        }
        public bool Update(Disease disease)
        {
            var diseases = GetAll();

            foreach (Disease d in diseases)
            {
                if (d.Id == disease.Id)
                {
                    d.Name = disease.Name;
                    WriteLinesToFile(_path, DiseasesToCSVFormat((List<Disease>)diseases));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long diseaseId)
        {
            var diseases = (List<Disease>)GetAll();

            foreach (Disease d in diseases)
            {
                if (d.Id == diseaseId)
                {
                    diseases.Remove(d);
                    WriteLinesToFile(_path, DiseasesToCSVFormat((List<Disease>)diseases));
                    return true;
                }
            }
            return false;
        }
        private string DiseaseToCSVFormat(Disease disease)
        {
            return string.Join(_delimiter,
                disease.Id,
                disease.Name
                );
        }

        private Disease CSVFormatToDisease(string diseaseCSVFormat)
        {
            var tokens = diseaseCSVFormat.Split(_delimiter.ToCharArray());
            return new Disease(
                long.Parse(tokens[0]),
                tokens[1]
                );
        }
        private List<string> DiseasesToCSVFormat(List<Disease> diseases)
        {
            List<string> lines = new List<string>();

            foreach (Disease disease in diseases)
            {
                lines.Add(DiseaseToCSVFormat(disease));
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
    }
}