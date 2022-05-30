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
    public class ScheduledItemTransferRepository : IScheduledItemTransferRepository
    {
        private const string NOT_FOUND_ERROR = "SCHEDULED ITEM TRANSFER NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _scheduledItemTransferMaxId;

        public ScheduledItemTransferRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _scheduledItemTransferMaxId = GetMaxId(GetAll());
        }

        private long GetMaxId(IEnumerable<ScheduledItemTransfer> scheduledItemTransfers)
        {
            return scheduledItemTransfers.Count() == 0 ? 0 : scheduledItemTransfers.Max(scheduledItemTransfer => scheduledItemTransfer.Id);
        }

        public IEnumerable<ScheduledItemTransfer> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToScheduledItemTransfer)
                .ToList();
        }

        public ScheduledItemTransfer Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(scheduledItemTransfer => scheduledItemTransfer.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public ScheduledItemTransfer Create(ScheduledItemTransfer scheduledItemTransfer)
        {
            scheduledItemTransfer.Id = ++_scheduledItemTransferMaxId;
            AppendLineToFile(_path, ScheduledItemTransferToCSVFormat(scheduledItemTransfer));
            return scheduledItemTransfer;
        }

        public bool Update(ScheduledItemTransfer scheduledItemTransfer)
        {
            var scheduledItemTransfers = GetAll();

            foreach (ScheduledItemTransfer sit in scheduledItemTransfers)
            {
                if (sit.Id == scheduledItemTransfer.Id)
                {
                    sit.Item = scheduledItemTransfer.Item;
                    sit.ItemsForTransfer = scheduledItemTransfer.ItemsForTransfer;
                    sit.SourceStorageType = scheduledItemTransfer.SourceStorageType;
                    sit.SourceRoom = scheduledItemTransfer.SourceRoom;
                    sit.SourceWarehouse = scheduledItemTransfer.SourceWarehouse;
                    sit.DestinationStorageType = scheduledItemTransfer.DestinationStorageType;
                    sit.DestinationRoom = scheduledItemTransfer.DestinationRoom;
                    sit.DestinationWarehouse = scheduledItemTransfer.DestinationWarehouse;
                    sit.TransferDate = scheduledItemTransfer.TransferDate;
                    
                }
            }
            WriteLinesToFile(_path, ScheduledItemTransfersToCSVFormat((List<ScheduledItemTransfer>)scheduledItemTransfers));
            return true;
        }
        public bool Delete(long scheduledItemTransferId)
        {
            var scheduledItemTransfers = (List<ScheduledItemTransfer>)GetAll();

            foreach (ScheduledItemTransfer sit in scheduledItemTransfers)
            {
                if (sit.Id == scheduledItemTransferId)
                {
                    scheduledItemTransfers.Remove(sit);
                    break;
                }
            }


            WriteLinesToFile(_path, ScheduledItemTransfersToCSVFormat((List<ScheduledItemTransfer>)scheduledItemTransfers));
            return true;
        }

        private string ScheduledItemTransferToCSVFormat(ScheduledItemTransfer scheduledItemTransfer)
        {
            StorageType sourceStorageType = scheduledItemTransfer.SourceStorageType;
            StorageType destinationStorageType = scheduledItemTransfer.DestinationStorageType;

            long sourceStorageId = -1;
            if (sourceStorageType == StorageType.ROOM) sourceStorageId = scheduledItemTransfer.SourceRoom.Id;
            else if(sourceStorageType == StorageType.WAREHOUSE) sourceStorageId = scheduledItemTransfer.SourceWarehouse.Id;

            long destinationStorageId = -1;
            if (destinationStorageType == StorageType.ROOM) destinationStorageId = scheduledItemTransfer.DestinationRoom.Id;
            else if(destinationStorageType == StorageType.WAREHOUSE) destinationStorageId = scheduledItemTransfer.DestinationWarehouse.Id;

            return string.Join(_delimiter, 
                scheduledItemTransfer.Id,
                scheduledItemTransfer.Item.Id,
                scheduledItemTransfer.ItemsForTransfer,
                (int)sourceStorageType,
                sourceStorageId,
                (int)destinationStorageType,
                destinationStorageId,
                scheduledItemTransfer.TransferDate.ToString("dd.MM.yyyy.")
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

        private ScheduledItemTransfer CSVFormatToScheduledItemTransfer(string scheduledItemTransferCSVFormat)
        {
            var tokens = scheduledItemTransferCSVFormat.Split(_delimiter.ToCharArray());

            var timeFormat = "dd.MM.yyyy.";
            DateTime transferDate;

            DateTime.TryParseExact(tokens[7], timeFormat, CultureInfo.InvariantCulture
                                                , DateTimeStyles.None
                                                , out transferDate);

            return new ScheduledItemTransfer(
               long.Parse(tokens[0]),
               long.Parse(tokens[1]),
               int.Parse(tokens[2]),
               (StorageType)int.Parse(tokens[3]),
               long.Parse(tokens[4]),
               (StorageType)int.Parse(tokens[5]),
               long.Parse(tokens[6]),
               transferDate
               );
        }

        private List<string> ScheduledItemTransfersToCSVFormat(List<ScheduledItemTransfer> scheduledItemTransfers)
        {
            List<string> lines = new List<string>();

            foreach (ScheduledItemTransfer scheduledItemTransfer in scheduledItemTransfers)
            {
                lines.Add(ScheduledItemTransferToCSVFormat(scheduledItemTransfer));
            }
            return lines;
        }
    }
}
