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

namespace zdravstvena_ustanova.Repository
{
    public class PatientVaccinationRepository
    {
        private const string NOT_FOUND_ERROR = "PATIENT VACCINATION NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _patientVaccinationMaxId;

        public PatientVaccinationRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _patientVaccinationMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<PatientVaccination> patientVaccinations)
        {
            return patientVaccinations.Count() == 0 ? 0 : patientVaccinations.Max(patientVaccination => patientVaccination.Id);
        }

        public IEnumerable<PatientVaccination> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToPatientVaccination)
                .ToList();
        }

        public PatientVaccination Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(patientVaccination => patientVaccination.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public PatientVaccination Create(PatientVaccination patientVaccination)
        {
            patientVaccination.Id = ++_patientVaccinationMaxId;
            AppendLineToFile(_path, PatientVaccinationToCSVFormat(patientVaccination));
            return patientVaccination;
        }

        public bool Update(PatientVaccination patientVaccination)
        {
            var patientVaccinations = GetAll();

            foreach (PatientVaccination pv in patientVaccinations)
            {
                if (pv.Id == patientVaccination.Id)
                {
                    pv.DateOfVaccination = patientVaccination.DateOfVaccination;
                    pv.Vaccine = patientVaccination.Vaccine;
                    WriteLinesToFile(_path, PatientVaccinationsToCSVFormat((List<PatientVaccination>)patientVaccinations));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long patientVaccinationId)
        {
            var patientVaccinations = (List<PatientVaccination>)GetAll();

            foreach (PatientVaccination pv in patientVaccinations)
            {
                if (pv.Id == patientVaccinationId)
                {
                    patientVaccinations.Remove(pv);
                    WriteLinesToFile(_path, PatientVaccinationsToCSVFormat((List<PatientVaccination>)patientVaccinations));
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

        private string PatientVaccinationToCSVFormat(PatientVaccination patientVaccination)
        {
            return string.Join(_delimiter,
                patientVaccination.Id,
                patientVaccination.DateOfVaccination.ToString("dd.MM.yyyy. HH:mm"),
                patientVaccination.Vaccine.Id
                );
        }
        private List<string> PatientVaccinationsToCSVFormat(List<PatientVaccination> patientVaccinations)
        {
            List<string> lines = new List<string>();

            foreach (PatientVaccination patientVaccination in patientVaccinations)
            {
                lines.Add(PatientVaccinationToCSVFormat(patientVaccination));
            }
            return lines;
        }

        private PatientVaccination CSVFormatToPatientVaccination(string patientVaccinationCSVFormat)
        {
            var tokens = patientVaccinationCSVFormat.Split(_delimiter.ToCharArray());
            var timeFormat = "dd.MM.yyyy. HH:mm";
            DateTime dateOfVaccination;
            DateTime.TryParseExact(tokens[1], timeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfVaccination);
            return new PatientVaccination(
                long.Parse(tokens[0]),
                dateOfVaccination,
                long.Parse(tokens[2])
                );
        }

    }
}
