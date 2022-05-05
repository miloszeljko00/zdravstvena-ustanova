using zdravstvena_ustanova.Model;
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
    public class LabAnalysisRequestRepository
    {
        private const string NOT_FOUND_ERROR = "LabAnalysisRequest NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _labAnalysisRequestMaxId;

        public LabAnalysisRequestRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _labAnalysisRequestMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<LabAnalysisRequest> labAnalysisRequests)
        {
            return labAnalysisRequests.Count() == 0 ? 0 : labAnalysisRequests.Max(labAnalysisRequest => labAnalysisRequest.Id);
        }

        public IEnumerable<LabAnalysisRequest> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToLabAnalysisRequest)
                .ToList();
        }

        public LabAnalysisRequest Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(labAnalysisRequest => labAnalysisRequest.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public LabAnalysisRequest Create(LabAnalysisRequest labAnalysisRequest)
        {
            labAnalysisRequest.Id = ++_labAnalysisRequestMaxId;
            AppendLineToFile(_path, LabAnalysisRequestToCSVFormat(labAnalysisRequest));
            return labAnalysisRequest;
        }

        public bool Update(LabAnalysisRequest labAnalysisRequest)
        {
            var labAnalysisRequests = GetAll();

            foreach (LabAnalysisRequest lar in labAnalysisRequests)
            {
                if (lar.Id == labAnalysisRequest.Id)
                {
                    lar.IsUrgent = labAnalysisRequest.IsUrgent;
                    lar.LabAnalysisComponent = labAnalysisRequest.LabAnalysisComponent;
                    WriteLinesToFile(_path, LabAnalysisRequestsToCSVFormat((List<LabAnalysisRequest>)labAnalysisRequests));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long labAnalysisRequestId)
        {
            var labAnalysisRequests = (List<LabAnalysisRequest>)GetAll();

            foreach (LabAnalysisRequest lar in labAnalysisRequests)
            {
                if (lar.Id == labAnalysisRequestId)
                {
                    labAnalysisRequests.Remove(lar);
                    WriteLinesToFile(_path, LabAnalysisRequestsToCSVFormat((List<LabAnalysisRequest>)labAnalysisRequests));
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

        private string LabAnalysisRequestToCSVFormat(LabAnalysisRequest labAnalysisRequest)
        {
            int count = labAnalysisRequest.LabAnalysisComponent.Count;
            string labAnalysisComponents = "";
            for (int i = 0; i < count; i++)
            {
                labAnalysisComponents = string.Join(_delimiter, labAnalysisComponents, labAnalysisRequest.LabAnalysisComponent[i].Id);
            }
            return string.Join(_delimiter,
                labAnalysisRequest.Id,
                labAnalysisRequest.IsUrgent,
                count,
                labAnalysisComponents
                );
        }
        private List<string> LabAnalysisRequestsToCSVFormat(List<LabAnalysisRequest> labAnalysisRequests)
        {
            List<string> lines = new List<string>();

            foreach (LabAnalysisRequest labAnalysisRequest in labAnalysisRequests)
            {
                lines.Add(LabAnalysisRequestToCSVFormat(labAnalysisRequest));
            }
            return lines;
        }

        private LabAnalysisRequest CSVFormatToLabAnalysisRequest(string labAnalysisRequestCSVFormat)
        {
            var tokens = labAnalysisRequestCSVFormat.Split(_delimiter.ToCharArray());

            List<LabAnalysisComponent> components = new List<LabAnalysisComponent>();
            for (int i = 2 + 1; i < 2 + 1 + int.Parse(tokens[2]); i++)
            {
                var component = new LabAnalysisComponent(int.Parse(tokens[i]));
                components.Add(component);
            }

            return new LabAnalysisRequest(
                long.Parse(tokens[0]),
                bool.Parse(tokens[1]),
                components
                );
        }

    }
}
