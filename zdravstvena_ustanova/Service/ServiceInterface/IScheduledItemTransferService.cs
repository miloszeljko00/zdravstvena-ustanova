using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Service.ServiceInterface;

public interface IScheduledItemTransferService : IService<ScheduledItemTransfer>
{
    ScheduledItemTransfer ScheduleItemTransfer(ScheduledItemTransfer scheduledItemTransfer);
    int GetItemUnderTransferCountForSourceStorage(ScheduledItemTransfer scheduledItemTransfer);
}