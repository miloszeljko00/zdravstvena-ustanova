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

namespace zdravstvena_ustanova.Repository
{
    public class HospitalizationRecordRepository
    {
        private const string NOT_FOUND_ERROR = "HOSPITALIZATION RECORD NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _hospitalizationRecordMaxId;

        public HospitalizationRecordRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _hospitalizationRecordMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<HospitalizationRecord> hospitalizationRecords)
        {
            return hospitalizationRecords.Count() == 0 ? 0 : hospitalizationRecords.Max(hospitalizationRecord => hospitalizationRecord.Id);
        }

        public IEnumerable<HospitalizationRecord> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToHospitalizationRecord)
                .ToList();
        }

        public HospitalizationRecord Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(hospitalizationRecord => hospitalizationRecord.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public HospitalizationRecord Create(HospitalizationRecord hospitalizationRecord)
        {
            hospitalizationRecord.Id = ++_hospitalizationRecordMaxId;
            AppendLineToFile(_path, HospitalizationRecordToCSVFormat(hospitalizationRecord));
            return hospitalizationRecord;
        }

        public bool Update(HospitalizationRecord hospitalizationRecord)
        {
            var hospitalizationRecords = GetAll();

            foreach (HospitalizationRecord hr in hospitalizationRecords)
            {
                if (hr.Id == hospitalizationRecord.Id)
                {
                    hr.Cause = hospitalizationRecord.Cause;
                    hr.Admission = hospitalizationRecord.Admission;
                    hr.Release = hospitalizationRecord.Release;
                    hr.ReleaseKind = hospitalizationRecord.ReleaseKind;
                    WriteLinesToFile(_path, HospitalizationRecordsToCSVFormat((List<HospitalizationRecord>)hospitalizationRecords));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long hospitalizationRecordId)
        {
            var hospitalizationRecords = (List<HospitalizationRecord>)GetAll();

            foreach (HospitalizationRecord hr in hospitalizationRecords)
            {
                if (hr.Id == hospitalizationRecordId)
                {
                    hospitalizationRecords.Remove(hr);
                    WriteLinesToFile(_path, HospitalizationRecordsToCSVFormat((List<HospitalizationRecord>)hospitalizationRecords));
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

        private string HospitalizationRecordToCSVFormat(HospitalizationRecord hospitalizationRecord)
        {
            return string.Join(_delimiter,
                hospitalizationRecord.Id,
                hospitalizationRecord.Cause,
                hospitalizationRecord.Admission.ToString("dd.MM.yyyy. HH:mm"),
                hospitalizationRecord.Release.ToString("dd.MM.yyyy. HH:mm"),
                (int)hospitalizationRecord.ReleaseKind,
                hospitalizationRecord.Room.Id
                );
        }
        private List<string> HospitalizationRecordsToCSVFormat(List<HospitalizationRecord> hospitalizationRecords)
        {
            List<string> lines = new List<string>();

            foreach (HospitalizationRecord hospitalizationRecord in hospitalizationRecords)
            {
                lines.Add(HospitalizationRecordToCSVFormat(hospitalizationRecord));
            }
            return lines;
        }

        private HospitalizationRecord CSVFormatToHospitalizationRecord(string hospitalizationRecordCSVFormat)
        {
            var tokens = hospitalizationRecordCSVFormat.Split(_delimiter.ToCharArray());
            var timeFormat = "dd.MM.yyyy. HH:mm";
            DateTime admission;
            DateTime release;
            DateTime.TryParseExact(tokens[2], timeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out admission);
            DateTime.TryParseExact(tokens[3], timeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out release);
            return new HospitalizationRecord(
                long.Parse(tokens[0]),
                tokens[1],
                admission,
                release,
                (ReleaseKind)int.Parse(tokens[4]),
                long.Parse(tokens[5])
                );
        }

    }
}
