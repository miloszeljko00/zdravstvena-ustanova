using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using zdravstvena_ustanova.Exception;
using Model;
using Model.Enums;

namespace Repository
{
   public class StoredItemRepository
   {
        private const string NOT_FOUND_ERROR = "STOREDITEM NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _storedItemMaxId;

        public StoredItemRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _storedItemMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<StoredItem> storedItems)
        {
            return storedItems.Count() == 0 ? 0 : storedItems.Max(storedItem => storedItem.Id);
        }

        public IEnumerable<StoredItem> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToStoredItem)
                .ToList();
        }

        public StoredItem Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(storedItem => storedItem.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public StoredItem Create(StoredItem storedItem)
        {

            var storedItems = GetAll();

            foreach(StoredItem si in storedItems)
            {
                if(si.Item.Id == storedItem.Item.Id)
                {
                    if(si.StorageType == StorageType.WAREHOUSE && storedItem.StorageType == StorageType.WAREHOUSE)
                    {
                        if(si.Warehouse.Id == storedItem.Warehouse.Id)
                        {
                            si.Quantity += storedItem.Quantity;
                            Update(si);
                            storedItem = si;
                            return storedItem;
                        }
                    }
                    else if(si.StorageType == StorageType.ROOM && storedItem.StorageType == StorageType.ROOM)
                    {
                        if (si.Room.Id == storedItem.Room.Id)
                        {
                            si.Quantity += storedItem.Quantity;
                            Update(si);
                            storedItem = si;
                            return storedItem;
                        }
                    }
                }
            }

            storedItem.Id = ++_storedItemMaxId;
            AppendLineToFile(_path, StoredItemToCSVFormat(storedItem));
            return storedItem;
        }

        public bool Update(StoredItem storedItem)
        {
            var storedItems = GetAll();

            foreach (StoredItem si in storedItems)
            {
                if (si.Id == storedItem.Id)
                {
                    si.Item = storedItem.Item;
                    si.Quantity = storedItem.Quantity;
                    si.StorageType = storedItem.StorageType;
                    si.Room = storedItem.Room;
                    si.Warehouse = storedItem.Warehouse;
                    WriteLinesToFile(_path, StoredItemsToCSVFormat((List<StoredItem>)storedItems));
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
            if(storedItem.StorageType == StorageType.ROOM)
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