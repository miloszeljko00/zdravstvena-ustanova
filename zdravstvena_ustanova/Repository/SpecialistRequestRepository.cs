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
    public class SpecialistRequestRepository
    {
        private const string NOT_FOUND_ERROR = "SPECIALTY REQUEST NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _specialistRequestMaxId;

        public SpecialistRequestRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _specialistRequestMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<SpecialistRequest> specialistRequests)
        {
            return specialistRequests.Count() == 0 ? 0 : specialistRequests.Max(specialistRequest => specialistRequest.Id);
        }

        public IEnumerable<SpecialistRequest> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToSpecialistRequest)
                .ToList();
        }

        public SpecialistRequest Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(specialistRequest => specialistRequest.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public SpecialistRequest Create(SpecialistRequest specialistRequest)
        {
            specialistRequest.Id = ++_specialistRequestMaxId;
            AppendLineToFile(_path, SpecialistRequestToCSVFormat(specialistRequest));
            return specialistRequest;
        }

        public bool Update(SpecialistRequest specialistRequest)
        {
            var specialistRequests = GetAll();

            foreach (SpecialistRequest sr in specialistRequests)
            {
                if (sr.Id == specialistRequest.Id)
                {
                    sr.Cause = specialistRequest.Cause;
                    sr.IsUrgent = specialistRequest.IsUrgent;
                    sr.Specialty = specialistRequest.Specialty;
                    WriteLinesToFile(_path, SpecialistRequestsToCSVFormat((List<SpecialistRequest>)specialistRequests));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long specialistRequestId)
        {
            var specialistRequests = (List<SpecialistRequest>)GetAll();

            foreach (SpecialistRequest sr in specialistRequests)
            {
                if (sr.Id == specialistRequestId)
                {
                    specialistRequests.Remove(sr);
                    WriteLinesToFile(_path, SpecialistRequestsToCSVFormat((List<SpecialistRequest>)specialistRequests));
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

        private string SpecialistRequestToCSVFormat(SpecialistRequest specialistRequest)
        {
            return string.Join(_delimiter,
                specialistRequest.Id,
                specialistRequest.Cause,
                specialistRequest.IsUrgent,
                specialistRequest.Specialty.Id
                );
        }
        private List<string> SpecialistRequestsToCSVFormat(List<SpecialistRequest> specialistRequests)
        {
            List<string> lines = new List<string>();

            foreach (SpecialistRequest specialistRequest in specialistRequests)
            {
                lines.Add(SpecialistRequestToCSVFormat(specialistRequest));
            }
            return lines;
        }

        private SpecialistRequest CSVFormatToSpecialistRequest(string specialistRequestCSVFormat)
        {
            var tokens = specialistRequestCSVFormat.Split(_delimiter.ToCharArray());
            return new SpecialistRequest(
                long.Parse(tokens[0]),
                tokens[1],
                bool.Parse(tokens[2]),
                long.Parse(tokens[3])
                );
        }

    }
}
