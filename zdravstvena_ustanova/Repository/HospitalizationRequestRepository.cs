using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using zdravstvena_ustanova.Exception;

namespace zdravstvena_ustanova.Repository
{
    public class HospitalizationRequestRepository
    {
        private const string NOT_FOUND_ERROR = "INGREDIENT NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _hospitalizationRequestMaxId;

        public HospitalizationRequestRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _hospitalizationRequestMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<HospitalizationRequest> hospitalizationRequests)
        {
            return hospitalizationRequests.Count() == 0 ? 0 : hospitalizationRequests.Max(hospitalizationRequest => hospitalizationRequest.Id);
        }

        public IEnumerable<HospitalizationRequest> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToHospitalizationRequest)
                .ToList();
        }

        public HospitalizationRequest GetById(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(hospitalizationRequest => hospitalizationRequest.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public HospitalizationRequest Create(HospitalizationRequest hospitalizationRequest)
        {
            hospitalizationRequest.Id = ++_hospitalizationRequestMaxId;
            AppendLineToFile(_path, HospitalizationRequestToCSVFormat(hospitalizationRequest));
            return hospitalizationRequest;
        }
        public bool Update(HospitalizationRequest hospitalizationRequest)
        {
            var hospitalizationRequests = GetAll();

            foreach (HospitalizationRequest hr in hospitalizationRequests)
            {
                if (hr.Id == hospitalizationRequest.Id)
                {
                    hr.ReqDateOfAdmission = hospitalizationRequest.ReqDateOfAdmission;
                    hr.Cause = hospitalizationRequest.Cause;
                    WriteLinesToFile(_path, HospitalizationRequestsToCSVFormat((List<HospitalizationRequest>)hospitalizationRequests));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long hospitalizationRequestsId)
        {
            var hospitalizationRequests = (List<HospitalizationRequest>)GetAll();

            foreach (HospitalizationRequest hr in hospitalizationRequests)
            {
                if (hr.Id == hospitalizationRequestsId)
                {
                    hospitalizationRequests.Remove(hr);
                    WriteLinesToFile(_path, HospitalizationRequestsToCSVFormat((List<HospitalizationRequest>)hospitalizationRequests));
                    return true;
                }
            }
            return false;
        }
        private string HospitalizationRequestToCSVFormat(HospitalizationRequest hospitalizationRequest)
        {
            return string.Join(_delimiter,
                hospitalizationRequest.Id,
                hospitalizationRequest.ReqDateOfAdmission,
                hospitalizationRequest.Cause
                );
        }

        private HospitalizationRequest CSVFormatToHospitalizationRequest(string hospitalizationRequestCSVFormat)
        {
            var tokens = hospitalizationRequestCSVFormat.Split(_delimiter.ToCharArray());
            var timeFormat = "dd.MM.yyyy. HH:mm";
            DateTime reqDateOfAdmission;
            DateTime.TryParseExact(tokens[1], timeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out reqDateOfAdmission);
            return new HospitalizationRequest(
                long.Parse(tokens[0]),
                reqDateOfAdmission,
                tokens[2]
                );
        }
        private List<string> HospitalizationRequestsToCSVFormat(List<HospitalizationRequest> hospitalizationRequests)
        {
            List<string> lines = new List<string>();

            foreach (HospitalizationRequest hospitalizationRequest in hospitalizationRequests)
            {
                lines.Add(HospitalizationRequestToCSVFormat(hospitalizationRequest));
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