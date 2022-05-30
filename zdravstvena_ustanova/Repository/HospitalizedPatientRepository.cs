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
    public class HospitalizedPatientRepository : IHospitalizedPatientRepository
    {
        private const string NOT_FOUND_ERROR = "HOSPITALIZED PATIENT NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _hospitalizedPatientMaxId;

        public HospitalizedPatientRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _hospitalizedPatientMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<HospitalizedPatient> hospitalizedPatients)
        {
            return hospitalizedPatients.Count() == 0 ? 0 : hospitalizedPatients.Max(hospitalizedPatient => hospitalizedPatient.Id);
        }

        public IEnumerable<HospitalizedPatient> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToHospitalizedPatient)
                .ToList();
        }

        public HospitalizedPatient Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(hospitalizedPatient => hospitalizedPatient.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public HospitalizedPatient Create(HospitalizedPatient hospitalizedPatient)
        {
            hospitalizedPatient.Id = ++_hospitalizedPatientMaxId;
            AppendLineToFile(_path, HospitalizedPatientToCSVFormat(hospitalizedPatient));
            return hospitalizedPatient;
        }

        public bool Update(HospitalizedPatient hospitalizedPatient)
        {
            var hospitalizedPatients = GetAll();

            foreach (HospitalizedPatient hp in hospitalizedPatients)
            {
                if (hp.Id == hospitalizedPatient.Id)
                {
                    hp.AdmissionDate = hospitalizedPatient.AdmissionDate;
                    hp.Cause = hospitalizedPatient.Cause;
                    hp.Patient = hospitalizedPatient.Patient;
                    hp.Room = hospitalizedPatient.Room;
                    WriteLinesToFile(_path, HospitalizedPatientsToCSVFormat((List<HospitalizedPatient>)hospitalizedPatients));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long hospitalizedPatientsId)
        {
            var hospitalizedPatients = (List<HospitalizedPatient>)GetAll();

            foreach (HospitalizedPatient hp in hospitalizedPatients)
            {
                if (hp.Id == hospitalizedPatientsId)
                {
                    hospitalizedPatients.Remove(hp);
                    WriteLinesToFile(_path, HospitalizedPatientsToCSVFormat((List<HospitalizedPatient>)hospitalizedPatients));
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

        private string HospitalizedPatientToCSVFormat(HospitalizedPatient hospitalizedPatient)
        {
            return string.Join(_delimiter,
                hospitalizedPatient.Id,
                hospitalizedPatient.AdmissionDate.ToString("dd.MM.yyyy. HH:mm"),
                hospitalizedPatient.Cause,
                hospitalizedPatient.Patient.Id,
                hospitalizedPatient.Room.Id
                );
        }
        private List<string> HospitalizedPatientsToCSVFormat(List<HospitalizedPatient> hospitalizedPatients)
        {
            List<string> lines = new List<string>();

            foreach (HospitalizedPatient hospitalizedPatient in hospitalizedPatients)
            {
                lines.Add(HospitalizedPatientToCSVFormat(hospitalizedPatient));
            }
            return lines;
        }

        private HospitalizedPatient CSVFormatToHospitalizedPatient(string hospitalizedPatientCSVFormat)
        {
            var tokens = hospitalizedPatientCSVFormat.Split(_delimiter.ToCharArray());
            var timeFormat = "dd.MM.yyyy. HH:mm";
            DateTime admissionDate;
            DateTime.TryParseExact(tokens[1], timeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out admissionDate);
            return new HospitalizedPatient(
                long.Parse(tokens[0]),
                admissionDate,
                tokens[2],
                long.Parse(tokens[3]),
                long.Parse(tokens[4])
                );
        }

    }
}
