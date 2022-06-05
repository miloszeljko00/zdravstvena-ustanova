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
    public class MedicationTypeRepository : IMedicationTypeRepository
    {
        private const string NOT_FOUND_ERROR = "MEDICATION TYPE NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _medicationMaxId;

        public MedicationTypeRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _medicationMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<MedicationType> medicationTypes)
        {
            return medicationTypes.Count() == 0 ? 0 : medicationTypes.Max(medicationType => medicationType.Id);
        }

        public IEnumerable<MedicationType> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToMedicationType)
                .ToList();
        }

        public MedicationType Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(medicationType => medicationType.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public MedicationType Create(MedicationType medicationType)
        {
            medicationType.Id = ++_medicationMaxId;
            AppendLineToFile(_path, MedicationTypeToCSVFormat(medicationType));
            return medicationType;
        }
        public bool Update(MedicationType medicationType)
        {
            var medicationTypes = (List<MedicationType>)GetAll();

            foreach (MedicationType mt in medicationTypes)
            {
                if (mt.Id == medicationType.Id)
                {
                    mt.Name = medicationType.Name;
                    WriteLinesToFile(_path, MedicationTypesToCSVFormat(medicationTypes));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long medicationTypeId)
        {
            var medicationTypes = (List<MedicationType>)GetAll();

            foreach (MedicationType mt in medicationTypes)
            {
                if (mt.Id == medicationTypeId)
                {
                    medicationTypes.Remove(mt);
                    WriteLinesToFile(_path, MedicationTypesToCSVFormat(medicationTypes));
                    return true;
                }
            }
            return false;
        }
        private string MedicationTypeToCSVFormat(MedicationType medicationType)
        {
            return string.Join(_delimiter,
                medicationType.Id,
                medicationType.Name
                );
        }

        private MedicationType CSVFormatToMedicationType(string medicationTypeCSVFormat)
        {
            var tokens = medicationTypeCSVFormat.Split(_delimiter.ToCharArray());
            return new MedicationType(
                long.Parse(tokens[0]),
                tokens[1]
                );
        }
        private List<string> MedicationTypesToCSVFormat(List<MedicationType> medicationTypes)
        {
            List<string> lines = new List<string>();

            foreach (MedicationType medicationType in medicationTypes)
            {
                lines.Add(MedicationTypeToCSVFormat(medicationType));
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
