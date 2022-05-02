using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Enums;

namespace zdravstvena_ustanova.View.Model
{
    public class ItemTransferViewModel
    {
        public ScheduledItemTransfer ScheduledItemTransfer { get; set; }
        public string SourceStorageName { get; set; }
        public string DestinationStorageName { get; set; }

        public ItemTransferViewModel(ScheduledItemTransfer scheduledItemTransfer)
        {
            ScheduledItemTransfer = scheduledItemTransfer;
            if (ScheduledItemTransfer.SourceStorageType == StorageType.ROOM) SourceStorageName = ScheduledItemTransfer.SourceRoom.Name;
            else if (ScheduledItemTransfer.SourceStorageType == StorageType.WAREHOUSE) SourceStorageName = ScheduledItemTransfer.SourceWarehouse.Name;
            else SourceStorageName = "OTPIS ROBE";

            if (ScheduledItemTransfer.DestinationStorageType == StorageType.ROOM) DestinationStorageName = ScheduledItemTransfer.DestinationRoom.Name;
            else if (ScheduledItemTransfer.SourceStorageType == StorageType.WAREHOUSE) DestinationStorageName = ScheduledItemTransfer.DestinationWarehouse.Name;
            else DestinationStorageName = "OTPIS ROBE";
        }
    }
}
