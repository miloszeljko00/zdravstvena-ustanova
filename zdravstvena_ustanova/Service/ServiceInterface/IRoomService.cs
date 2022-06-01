using System.Collections.Generic;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Service.ServiceInterface;

public interface IRoomService : IService<Room>
{
    IEnumerable<StoredItem> FilterStoredItemsByName(long roomId, string searchText);
    IEnumerable<StoredItem> FilterStoredItemsByType(long roomId, ItemType itemType);
}