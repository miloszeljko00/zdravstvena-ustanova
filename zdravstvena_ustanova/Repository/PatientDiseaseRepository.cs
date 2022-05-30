using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Exception;
using zdravstvena_ustanova.Repository.RepositoryInterface;

namespace zdravstvena_ustanova.Repository
{
    public class PatientDiseaseRepository : IPatientDiseaseRepository
    {
        private const string NOT_FOUND_ERROR = "PATIENT DISEASE NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _patientDiseaseMaxId;

        public PatientDiseaseRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _patientDiseaseMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<PatientDisease> patientDiseases)
        {
            return patientDiseases.Count() == 0 ? 0 : patientDiseases.Max(patientDisease => patientDisease.Id);
        }

        public IEnumerable<PatientDisease> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToPatientDisease)
                .ToList();
        }

        public PatientDisease Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(patientDisease => patientDisease.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public PatientDisease Create(PatientDisease patientDisease)
        {
            patientDisease.Id = ++_patientDiseaseMaxId;
            AppendLineToFile(_path, PatientDiseaseToCSVFormat(patientDisease));
            return patientDisease;
        }

        public bool Update(PatientDisease patientDisease)
        {
            var patientDiseases = GetAll();

            foreach (PatientDisease pd in patientDiseases)
            {
                if (pd.Id == patientDisease.Id)
                {
                    pd.StartDate = patientDisease.StartDate;
                    pd.EndDate = patientDisease.EndDate;
                    pd.Disease = patientDisease.Disease;
                    WriteLinesToFile(_path, PatientDiseasesToCSVFormat((List<PatientDisease>)patientDiseases));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long patientDiseaseId)
        {
            var patientDiseases = (List<PatientDisease>)GetAll();

            foreach (PatientDisease pd in patientDiseases)
            {
                if (pd.Id == patientDiseaseId)
                {
                    patientDiseases.Remove(pd);
                    WriteLinesToFile(_path, PatientDiseasesToCSVFormat((List<PatientDisease>)patientDiseases));
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

        private string PatientDiseaseToCSVFormat(PatientDisease patientDisease)
        {
            return string.Join(_delimiter,
                patientDisease.Id,
                patientDisease.StartDate.ToString("dd.MM.yyyy. HH:mm"),
                patientDisease.EndDate.ToString("dd.MM.yyyy. HH:mm"),
                patientDisease.Disease.Id
                );
        }
        private List<string> PatientDiseasesToCSVFormat(List<PatientDisease> patientDiseases)
        {
            List<string> lines = new List<string>();

            foreach (PatientDisease patientDisease in patientDiseases)
            {
                lines.Add(PatientDiseaseToCSVFormat(patientDisease));
            }
            return lines;
        }

        private PatientDisease CSVFormatToPatientDisease(string patientDiseaseCSVFormat)
        {
            var tokens = patientDiseaseCSVFormat.Split(_delimiter.ToCharArray());
            var timeFormat = "dd.MM.yyyy. HH:mm";
            DateTime startDate;
            DateTime.TryParseExact(tokens[1], timeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
            DateTime endDate;
            DateTime.TryParseExact(tokens[2], timeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
            return new PatientDisease(
                long.Parse(tokens[0]),
                startDate,
                endDate,
                long.Parse(tokens[3])
                );
        }

    }
}
