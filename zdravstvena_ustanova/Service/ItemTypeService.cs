using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Repository;

namespace zdravstvena_ustanova.Service
{
    public class ItemTypeService
    {
        private readonly ItemTypeRepository _itemTypeRepository;

        public ItemTypeService(ItemTypeRepository itemTypeRepository)
        {
            _itemTypeRepository = itemTypeRepository;
        }

        public IEnumerable<ItemType> GetAll()
        {
            return _itemTypeRepository.GetAll();
        }

        public ItemType GetById(long id)
        {
            return _itemTypeRepository.Get(id);
        }

        public ItemType Create(ItemType itemType)
        {
            return _itemTypeRepository.Create(itemType);
        }
        public bool Update(ItemType itemType)
        {
            return _itemTypeRepository.Update(itemType);
        }
        public bool Delete(long itemTypeId)
        {
            return _itemTypeRepository.Delete(itemTypeId);
        }
    }
}
