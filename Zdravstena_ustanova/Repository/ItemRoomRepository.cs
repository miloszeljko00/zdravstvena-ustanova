using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Zdravstena_ustanova.Exception;
using Model;

namespace Repository
{
   public class ItemRoomRepository
   {
        private const string NOT_FOUND_ERROR = "ItemRoom NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _itemRoomMaxId;

        public ItemRoomRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _itemRoomMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<ItemRoom> itemRooms)
        {
            return itemRooms.Count() == 0 ? 0 : itemRooms.Max(itemRoom => itemRoom.Id);
        }

        public IEnumerable<ItemRoom> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToItem)
                .ToList();
        }

        public ItemRoom GetById(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(itemRoom => itemRoom.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public ItemRoom Create(ItemRoom itemRoom)
        {
            itemRoom.Id = ++_itemRoomMaxId;
            AppendLineToFile(_path, ItemRoomToCSVFormat(itemRoom));
            return itemRoom;
        }
        private string ItemRoomToCSVFormat(ItemRoom itemRoom)
        {
            return string.Join(_delimiter,
                itemRoom.Id,
                itemRoom.RoomId,
                itemRoom.ItemId,
                itemRoom.Quantity);
        }

        private ItemRoom CSVFormatToItem(string itemCSVFormat)
        {
            var tokens = itemCSVFormat.Split(_delimiter.ToCharArray());
            return new ItemRoom(
                long.Parse(tokens[0]),
                long.Parse(tokens[1]),
                long.Parse(tokens[2]),
                int.Parse(tokens[3]));
        }
        private void AppendLineToFile(string path, string line)
        {
            File.AppendAllText(path, line + Environment.NewLine);
        }
    }
}