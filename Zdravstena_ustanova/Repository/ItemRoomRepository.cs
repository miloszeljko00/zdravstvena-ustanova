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
        private const string NOT_FOUND_ERROR = "ITEMROOM NOT FOUND: {0} = {1}";
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

        public bool Update(ItemRoom itemRoom)
        {
            var itemRooms = GetAll();

            foreach (ItemRoom ir in itemRooms)
            {
                if (ir.Id == itemRoom.Id)
                {
                    ir.RoomId = itemRoom.RoomId;
                    ir.Item.Id = itemRoom.Item.Id;
                    ir.Quantity = itemRoom.Quantity;
                    ir.Item = itemRoom.Item;
                    WriteLinesToFile(_path, ItemRoomsToCSVFormat((List<ItemRoom>)itemRooms));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long itemRoomId)
        {
            var itemRooms = (List<ItemRoom>)GetAll();

            foreach (ItemRoom ir in itemRooms)
            {
                if (ir.Id == itemRoomId)
                {
                    itemRooms.Remove(ir);
                    WriteLinesToFile(_path, ItemRoomsToCSVFormat((List<ItemRoom>)itemRooms));
                    return true;
                }
            }
            return false;
        }

        private string ItemRoomToCSVFormat(ItemRoom itemRoom)
        {
            return string.Join(_delimiter,
                itemRoom.Id,
                itemRoom.RoomId,
                itemRoom.Item.Id,
                itemRoom.Quantity);
        }
        private List<string> ItemRoomsToCSVFormat(List<ItemRoom> itemRooms)
        {
            List<string> lines = new List<string>();

            foreach (ItemRoom itemRoom in itemRooms)
            {
                lines.Add(ItemRoomToCSVFormat(itemRoom));
            }
            return lines;
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

        private void WriteLinesToFile(string path, List<string> lines)
        {
            File.WriteAllLines(path, lines);
        }
    }
}