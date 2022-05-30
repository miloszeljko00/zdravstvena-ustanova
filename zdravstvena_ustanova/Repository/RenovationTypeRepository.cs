using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Exception;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Repository.RepositoryInterface;

namespace zdravstvena_ustanova.Repository
{
    public class RenovationTypeRepository : IRenovationTypeRepository
    {
        private const string NOT_FOUND_ERROR = "RENOVATION TYPE NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _renovationTypeMaxId;

        public RenovationTypeRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _renovationTypeMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<RenovationType> renovationTypes)
        {
            return renovationTypes.Count() == 0 ? 0 : renovationTypes.Max(renovationType => renovationType.Id);
        }

        public IEnumerable<RenovationType> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToRenovationType)
                .ToList();
        }

        public RenovationType Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(renovationType => renovationType.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public RenovationType Create(RenovationType renovationType)
        {
            renovationType.Id = ++_renovationTypeMaxId;
            AppendLineToFile(_path, RenovationTypeToCSVFormat(renovationType));
            return renovationType;
        }
        public bool Update(RenovationType renovationType)
        {
            var renovationTypes = (List<RenovationType>)GetAll();

            foreach (RenovationType rt in renovationTypes)
            {
                if (rt.Id == renovationType.Id)
                {
                    rt.Name = renovationType.Name;
                }
            }
            WriteLinesToFile(_path, RenovationTypesToCSVFormat(renovationTypes));
            return true;
        }
        public bool Delete(long renovationTypeId)
        {
            var renovationTypes = (List<RenovationType>)GetAll();

            foreach (RenovationType rt in renovationTypes)
            {
                if (rt.Id == renovationTypeId)
                {
                    renovationTypes.Remove(rt);
                    WriteLinesToFile(_path, RenovationTypesToCSVFormat(renovationTypes));
                    return true;
                }
            }
            return false;
        }
        private string RenovationTypeToCSVFormat(RenovationType renovationType)
        {
            return string.Join(_delimiter,
                renovationType.Id,
                renovationType.Name);
        }

        private RenovationType CSVFormatToRenovationType(string renovationTypeCSVFormat)
        {
            var tokens = renovationTypeCSVFormat.Split(_delimiter.ToCharArray());

            return new RenovationType(
                long.Parse(tokens[0]),
                tokens[1]);
        }
        private List<string> RenovationTypesToCSVFormat(List<RenovationType> renovationTypes)
        {
            List<string> lines = new List<string>();

            foreach (RenovationType renovationType in renovationTypes)
            {
                lines.Add(RenovationTypeToCSVFormat(renovationType));
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
