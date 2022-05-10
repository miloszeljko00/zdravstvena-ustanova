using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Exception;
using zdravstvena_ustanova.Model;

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
            return medicationApprovalRequests.Count() == 0 ? 0 : medicationApprovalRequests.Max(storedItem => storedItem.Id);
        }

        public IEnumerable<MedicationApprovalRequest> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToStoredItem)
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
            var medicationApprovalRequests = GetAll();

            foreach (MedicationApprovalRequest mar in medicationApprovalRequests)
            {
                if (mar.Id == medicationApprovalRequest.Id)
                {
                    mar.Item = medicationApprovalRequest.Item;
                    mar.Quantity = medicationApprovalRequest.Quantity;
                    mar.StorageType = medicationApprovalRequest.StorageType;
                    mar.Room = medicationApprovalRequest.Room;
                    mar.Warehouse = medicationApprovalRequest.Warehouse;
                    WriteLinesToFile(_path, StoredItemsToCSVFormat((List<StoredItem>)medicationApprovalRequests));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long storedItemId)
        {
            var storedItems = (List<StoredItem>)GetAll();

            foreach (StoredItem si in storedItems)
            {
                if (si.Id == storedItemId)
                {
                    storedItems.Remove(si);
                    WriteLinesToFile(_path, StoredItemsToCSVFormat((List<StoredItem>)storedItems));
                    return true;
                }
            }
            return false;
        }

        private string StoredItemToCSVFormat(StoredItem storedItem)
        {
            if (storedItem.StorageType == StorageType.ROOM)
            {
                return string.Join(_delimiter,
                                   storedItem.Id,
                                   storedItem.Item.Id,
                                   storedItem.Quantity,
                                   (int)storedItem.StorageType,
                                   storedItem.Room.Id);
            }
            return string.Join(_delimiter,
                                   storedItem.Id,
                                   storedItem.Item.Id,
                                   storedItem.Quantity,
                                   (int)storedItem.StorageType,
                                   storedItem.Warehouse.Id);
        }
        private List<string> StoredItemsToCSVFormat(List<StoredItem> storedItems)
        {
            List<string> lines = new List<string>();

            foreach (StoredItem storedItem in storedItems)
            {
                lines.Add(StoredItemToCSVFormat(storedItem));
            }
            return lines;
        }

        private StoredItem CSVFormatToStoredItem(string itemCSVFormat)
        {
            var tokens = itemCSVFormat.Split(_delimiter.ToCharArray());

            return new StoredItem(
                long.Parse(tokens[0]),
                long.Parse(tokens[1]),
                int.Parse(tokens[2]),
                (StorageType)int.Parse(tokens[3]),
                long.Parse(tokens[4]));
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
