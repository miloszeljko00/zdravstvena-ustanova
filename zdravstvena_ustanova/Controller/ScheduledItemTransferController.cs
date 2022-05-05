using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Controller
{
    public class ScheduledItemTransferController
    {
        private readonly ScheduledItemTransferService _scheduledItemTransferService;

        public ScheduledItemTransferController(ScheduledItemTransferService scheduledItemTransferService)
        {
            _scheduledItemTransferService = scheduledItemTransferService;
        }
        public IEnumerable<ScheduledItemTransfer> GetAll()
        {
            return _scheduledItemTransferService.GetAll();
        }
        public ScheduledItemTransfer GetById(long id)
        {
            return _scheduledItemTransferService.GetById(id);
        }
        public ScheduledItemTransfer Create(ScheduledItemTransfer scheduledItemTransfer)
        {
            return _scheduledItemTransferService.Create(scheduledItemTransfer);
        }
        public void Update(ScheduledItemTransfer scheduledItemTransfer)
        {
            _scheduledItemTransferService.Update(scheduledItemTransfer);
        }
        public void Delete(long scheduledItemTransferId)
        {
            _scheduledItemTransferService.Delete(scheduledItemTransferId);
        }

        public ScheduledItemTransfer ScheduleItemTransferFromRoom(ScheduledItemTransfer scheduledItemTransfer)
        {
            return _scheduledItemTransferService.ScheduleItemTransfer(scheduledItemTransfer);
        }

        public int GetItemUnderTransferCountForRoom(ScheduledItemTransfer scheduledItemTransfer)
        {
            return _scheduledItemTransferService.GetItemUnderTransferCountForSourceStorage(scheduledItemTransfer);
        }
    }
}
