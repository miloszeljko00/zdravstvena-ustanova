using zdravstvena_ustanova.Exception;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using zdravstvena_ustanova.Repository.RepositoryInterface;

namespace zdravstvena_ustanova.Repository
{
    public class RoomRepository : IRoomRepository

    {
    private const string NOT_FOUND_ERROR = "ROOM NOT FOUND: {0} = {1}";
    private readonly string _path;
    private readonly string _delimiter;
    private long _roomMaxId;

    public RoomRepository(string path, string delimiter)
    {
        _path = path;
        _delimiter = delimiter;
        _roomMaxId = GetMaxId(GetAll());
    }

    private long GetMaxId(IEnumerable<Room> rooms)
    {
        return rooms.Count() == 0 ? 0 : rooms.Max(room => room.Id);
    }

    public IEnumerable<Room> GetAll()
    {
        return File.ReadAllLines(_path)
            .Select(CSVFormatToRoom)
            .ToList();
    }

    public Room Get(long id)
    {
        try
        {
            return GetAll().SingleOrDefault(room => room.Id == id);
        }
        catch (ArgumentException)
        {
            throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
        }
    }

    public Room Create(Room room)
    {
        room.Id = ++_roomMaxId;
        AppendLineToFile(_path, RoomToCSVFormat(room));
        return room;
    }

    public bool Update(Room room)
    {
        var rooms = GetAll();

        foreach (Room r in rooms)
        {
            if (r.Id == room.Id)
            {
                r.Name = room.Name;
                r.Floor = room.Floor;
                r.RoomType = room.RoomType;
                WriteLinesToFile(_path, RoomsToCSVFormat((List<Room>)rooms));
                return true;
            }
        }

        return false;
    }

    public bool Delete(long roomId)
    {
        var rooms = (List<Room>)GetAll();

        foreach (Room r in rooms)
        {
            if (r.Id == roomId)
            {
                rooms.Remove(r);
                WriteLinesToFile(_path, RoomsToCSVFormat((List<Room>)rooms));
                return true;
            }
        }

        return false;
    }

    private string RoomToCSVFormat(Room room)
    {
        return string.Join(_delimiter,
            room.Id,
            room.Name,
            room.Floor,
            (int)room.RoomType);
    }

    private void AppendLineToFile(string path, string line)
    {
        File.AppendAllText(path, line + Environment.NewLine);
    }

    private void WriteLinesToFile(string path, List<string> lines)
    {
        File.WriteAllLines(path, lines);
    }

    private Room CSVFormatToRoom(string roomCSVFormat)
    {
        var tokens = roomCSVFormat.Split(_delimiter.ToCharArray());
        return new Room(
            long.Parse(tokens[0]),
            tokens[1],
            int.Parse(tokens[2]),
            (RoomType)int.Parse(tokens[3]));
    }

    private List<string> RoomsToCSVFormat(List<Room> rooms)
    {
        List<string> lines = new List<string>();

        foreach (Room room in rooms)
        {
            lines.Add(RoomToCSVFormat(room));
        }

        return lines;
    }
    }
}