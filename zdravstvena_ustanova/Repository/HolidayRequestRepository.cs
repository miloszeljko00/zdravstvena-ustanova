using zdravstvena_ustanova.Exception;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;

namespace zdravstvena_ustanova.Repository
{
    public class HolidayRequestRepository
    {
        private const string NOT_FOUND_ERROR = "HOLIDAY REQUEST NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _holidayRequestRepositoryMaxId;

        public HolidayRequestRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _holidayRequestRepositoryMaxId = GetMaxId(GetAll());
        }

        private long GetMaxId(IEnumerable<HolidayRequest> holidayRequests)
        {
            return holidayRequests.Count() == 0 ? 0 : holidayRequests.Max(holidayRequest => holidayRequest.Id);
        }

        public IEnumerable<HolidayRequest> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToHolidayRequest)
                .ToList();
        }

        public HolidayRequest Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(holidayRequest => holidayRequest.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public HolidayRequest Create(HolidayRequest holidayRequest)
        {
            holidayRequest.Id = ++_holidayRequestRepositoryMaxId;
            AppendLineToFile(_path, HolidayRequestToCSVFormat(holidayRequest));
            return holidayRequest;
        }

        public bool Update(HolidayRequest holidayRequest)
        {
            var holidayRequests = GetAll();

            foreach (HolidayRequest hr in holidayRequests)
            {
                if(hr.Id == holidayRequest.Id)
                {
                    hr.Cause = holidayRequest.Cause;
                    hr.StartDate = holidayRequest.StartDate;
                    hr.EndDate = holidayRequest.EndDate;
                    hr.HolidayRequestStatus=holidayRequest.HolidayRequestStatus;
                    WriteLinesToFile(_path, HolidayRequestsToCSVFormat((List<HolidayRequest>)holidayRequests));
                    return true;
                }
            }

            return false;
        }
        public bool Delete(long holidayRequestId)
        {
            var holidayRequests = (List<HolidayRequest>)GetAll();

            foreach (HolidayRequest hr in holidayRequests)
            {
                if (hr.Id == holidayRequestId)
                {
                    holidayRequests.Remove(hr);
                    WriteLinesToFile(_path, HolidayRequestsToCSVFormat((List<HolidayRequest>)holidayRequests));
                    return true;
                }
            }
            return false;
           
        }

        private string HolidayRequestToCSVFormat(HolidayRequest holidayRequest)
        {
            return string.Join(_delimiter,
                holidayRequest.Id,
                holidayRequest.Cause,
                holidayRequest.StartDate.ToString("dd.MM.yyyy. HH:mm"),
                holidayRequest.EndDate.ToString("dd.MM.yyyy. HH:mm"),
                (int)holidayRequest.HolidayRequestStatus
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

        private HolidayRequest CSVFormatToHolidayRequest(string holidayRequestCSVFormat)
        {
            var tokens = holidayRequestCSVFormat.Split(_delimiter.ToCharArray());
            var timeFormat = "dd.MM.yyyy. HH:mm";
            DateTime startTime;
            DateTime endTime;

            DateTime.TryParseExact(tokens[2], timeFormat, CultureInfo.InvariantCulture
                                                , DateTimeStyles.None
                                                , out startTime);
            DateTime.TryParseExact(tokens[3], timeFormat, CultureInfo.InvariantCulture
                                                , DateTimeStyles.None
                                                , out endTime);

            return new HolidayRequest(
               long.Parse(tokens[0]),
               tokens[1],
               startTime,
               endTime,
               (HolidayRequestStatus)int.Parse(tokens[4])
                );
        }

        private List<string> HolidayRequestsToCSVFormat(List<HolidayRequest> holidayRequests)
        {
            List<string> lines = new List<string>();

            foreach (HolidayRequest holidayRequest in holidayRequests)
            {
                lines.Add(HolidayRequestToCSVFormat(holidayRequest));
            }
            return lines;
        }
    }
}