using Model;
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
    public class LabAnalysisRecordRepository
    {
        private const string NOT_FOUND_ERROR = "LabAnalysisRecord NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _labAnalysisRecordMaxId;

        public LabAnalysisRecordRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _labAnalysisRecordMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<LabAnalysisRecord> labAnalysisRecords)
        {
            return labAnalysisRecords.Count() == 0 ? 0 : labAnalysisRecords.Max(labAnalysisRecord => labAnalysisRecord.Id);
        }

        public IEnumerable<LabAnalysisRecord> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToLabAnalysisRecord)
                .ToList();
        }

        public LabAnalysisRecord Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(labAnalysisRecord => labAnalysisRecord.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public LabAnalysisRecord Create(LabAnalysisRecord labAnalysisRecord)
        {
            labAnalysisRecord.Id = ++_labAnalysisRecordMaxId;
            AppendLineToFile(_path, LabAnalysisRecordToCSVFormat(labAnalysisRecord));
            return labAnalysisRecord;
        }

        public bool Update(LabAnalysisRecord labAnalysisRecord)
        {
            var labAnalysisRecords = GetAll();

            foreach (LabAnalysisRecord lar in labAnalysisRecords)
            {
                if (lar.Id == labAnalysisRecord.Id)
                {
                    lar.Date = labAnalysisRecord.Date;
                    WriteLinesToFile(_path, LabAnalysisRecordsToCSVFormat((List<LabAnalysisRecord>)labAnalysisRecords));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long labAnalysisRecordId)
        {
            var labAnalysisRecords = (List<LabAnalysisRecord>)GetAll();

            foreach (LabAnalysisRecord lar in labAnalysisRecords)
            {
                if (lar.Id == labAnalysisRecordId)
                {
                    labAnalysisRecords.Remove(lar);
                    WriteLinesToFile(_path, LabAnalysisRecordsToCSVFormat((List<LabAnalysisRecord>)labAnalysisRecords));
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

        private string LabAnalysisRecordToCSVFormat(LabAnalysisRecord labAnalysisRecord)
        {
            return string.Join(_delimiter,
                labAnalysisRecord.Id,
                labAnalysisRecord.Date.ToString("dd.MM.yyyy. HH:mm")
                );
        }
        private List<string> LabAnalysisRecordsToCSVFormat(List<LabAnalysisRecord> labAnalysisRecords)
        {
            List<string> lines = new List<string>();

            foreach (LabAnalysisRecord labAnalysisRecord in labAnalysisRecords)
            {
                lines.Add(LabAnalysisRecordToCSVFormat(labAnalysisRecord));
            }
            return lines;
        }

        private LabAnalysisRecord CSVFormatToLabAnalysisRecord(string labAnalysisRecordCSVFormat)
        {
            var tokens = labAnalysisRecordCSVFormat.Split(_delimiter.ToCharArray());
            var timeFormat = "dd.MM.yyyy. HH:mm";
            DateTime date;
            DateTime.TryParseExact(tokens[1], timeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
            return new LabAnalysisRecord(
                long.Parse(tokens[0]),
                date
                );
        }

    }
}
