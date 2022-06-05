using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Exception;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Repository.RepositoryInterface;

namespace zdravstvena_ustanova.Repository
{
    public class ItemTypeRepository : IItemTypeRepository
    {
        private const string NOT_FOUND_ERROR = "ITEM TYPE NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _itemTypeMaxId;

        public ItemTypeRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _itemTypeMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<ItemType> itemTypes)
        {
            return itemTypes.Count() == 0 ? 0 : itemTypes.Max(itemType => itemType.Id);
        }

        public IEnumerable<ItemType> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToItemType)
                .ToList();
        }

        public ItemType Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(itemType => itemType.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public ItemType Create(ItemType itemType)
        {
            itemType.Id = ++_itemTypeMaxId;
            AppendLineToFile(_path, ItemTypeToCSVFormat(itemType));
            return itemType;
        }
        public bool Update(ItemType itemType)
        {
            var itemTypes = (List<ItemType>)GetAll();

            foreach (ItemType it in itemTypes)
            {
                if (it.Id == itemType.Id)
                {
                    it.Name = itemType.Name;
                    WriteLinesToFile(_path, ItemTypesToCSVFormat(itemTypes));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long itemTypeId)
        {
            var itemTypes = (List<ItemType>)GetAll();

            foreach (ItemType it in itemTypes)
            {
                if (it.Id == itemTypeId)
                {
                    itemTypes.Remove(it);
                    WriteLinesToFile(_path, ItemTypesToCSVFormat(itemTypes));
                    return true;
                }
            }
            return false;
        }
        private string ItemTypeToCSVFormat(ItemType itemType)
        {
            return string.Join(_delimiter,
                itemType.Id,
                itemType.Name);
        }

        private ItemType CSVFormatToItemType(string itemTypeCSVFormat)
        {
            var tokens = itemTypeCSVFormat.Split(_delimiter.ToCharArray());

            return new ItemType(
                long.Parse(tokens[0]),
                tokens[1]);
        }
        private List<string> ItemTypesToCSVFormat(List<ItemType> itemTypes)
        {
            List<string> lines = new List<string>();

            foreach (ItemType itemType in itemTypes)
            {
                lines.Add(ItemTypeToCSVFormat(itemType));
            }
            return lines;
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
