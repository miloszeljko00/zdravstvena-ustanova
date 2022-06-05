using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Service.ServiceInterface;

public interface IStoredItemService : IService<StoredItem>
{
    int GetTotalItemCount(Item item);
    bool MoveItemFromTo(Room fromRoom, Warehouse toWarehouse, Item item, int quantity);
    bool MoveItemFromTo(Room fromRoom, Room toRoom, Item item, int quantity);
    bool MoveItemFromTo(Warehouse fromWarehouse, Room toRoom, Item item, int quantity);
    StoredItem GetByWarehouseItemId(long id);
}