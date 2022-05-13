using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Exception;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;

namespace zdravstvena_ustanova.Repository
{
    public class MedicationApprovalRequestRepository
    {
        private const string NOT_FOUND_ERROR = "MEDICATION APPROVAL REQUEST NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _medicationApprovalRequestMaxId;

        public MedicationApprovalRequestRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _medicationApprovalRequestMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<MedicationApprovalRequest> medicationApprovalRequests)
        {
            return medicationApprovalRequests.Count() == 0 ? 0 : medicationApprovalRequests.Max(medicationApprovalRequest => medicationApprovalRequest.Id);
        }

        public IEnumerable<MedicationApprovalRequest> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToMedicationApprovalRequest)
                .ToList();
        }

        public MedicationApprovalRequest Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(medicationApprovalRequest => medicationApprovalRequest.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public MedicationApprovalRequest Create(MedicationApprovalRequest medicationApprovalRequest)
        {

            medicationApprovalRequest.Id = ++_medicationApprovalRequestMaxId;
            AppendLineToFile(_path, MedicationApprovalRequestToCSVFormat(medicationApprovalRequest));
            return medicationApprovalRequest;
        }

        public bool Update(MedicationApprovalRequest medicationApprovalRequest)
        {
            var medicationApprovalRequests = (List<MedicationApprovalRequest>)GetAll();

            foreach (MedicationApprovalRequest mar in medicationApprovalRequests)
            {
                if (mar.Id == medicationApprovalRequest.Id)
                {
                    mar.Medication = medicationApprovalRequest.Medication;
                    mar.ApprovingDoctor = medicationApprovalRequest.ApprovingDoctor;
                    mar.RequestMessage = medicationApprovalRequest.RequestMessage;
                    mar.ResponseMessage = medicationApprovalRequest.ResponseMessage;
                    mar.RequestStatus = medicationApprovalRequest.RequestStatus;
                    mar.IsSeenByDoctor = medicationApprovalRequest.IsSeenByDoctor;
                    mar.IsSeenByManager = medicationApprovalRequest.IsSeenByManager;

                    WriteLinesToFile(_path, MedicationApprovalRequestsToCSVFormat(medicationApprovalRequests));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long medicationApprovalRequestId)
        {
            var medicationApprovalRequests = (List<MedicationApprovalRequest>)GetAll();

            foreach (MedicationApprovalRequest mar in medicationApprovalRequests)
            {
                if (mar.Id == medicationApprovalRequestId)
                {
                    medicationApprovalRequests.Remove(mar);
                    WriteLinesToFile(_path, MedicationApprovalRequestsToCSVFormat(medicationApprovalRequests));
                    return true;
                }
            }
            return false;
        }

        private string MedicationApprovalRequestToCSVFormat(MedicationApprovalRequest medicationApprovalRequest)
        {
            return string.Join(_delimiter,
                                   medicationApprovalRequest.Id,
                                   medicationApprovalRequest.Medication.Id,
                                   medicationApprovalRequest.ApprovingDoctor.Id,
                                   medicationApprovalRequest.RequestMessage,
                                   medicationApprovalRequest.ResponseMessage,
                                   (int)medicationApprovalRequest.RequestStatus,
                                   medicationApprovalRequest.IsSeenByDoctor,
                                   medicationApprovalRequest.IsSeenByManager);
    }
        private List<string> MedicationApprovalRequestsToCSVFormat(List<MedicationApprovalRequest> medicationApprovalRequests)
        {
            List<string> lines = new List<string>();

            foreach (MedicationApprovalRequest medicationApprovalRequest in medicationApprovalRequests)
            {
                lines.Add(MedicationApprovalRequestToCSVFormat(medicationApprovalRequest));
            }
            return lines;
        }

        private MedicationApprovalRequest CSVFormatToMedicationApprovalRequest(string itemCSVFormat)
        {
            var tokens = itemCSVFormat.Split(_delimiter.ToCharArray());

            return new MedicationApprovalRequest(
                long.Parse(tokens[0]),
                long.Parse(tokens[1]),
                long.Parse(tokens[2]),
                tokens[3],
                tokens[4],
                (RequestStatus)int.Parse(tokens[5]),
                bool.Parse(tokens[6]),
                bool.Parse(tokens[7]));
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
