using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Zdravstena_ustanova.Exception;

namespace Repository
{
    public class WarehouseRepository
    {
        private const string NOT_FOUND_ERROR = "WAREHOUSE NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _warehouseMaxId;

        public WarehouseRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _warehouseMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<Warehouse> warehouses)
        {
            return warehouses.Count() == 0 ? 0 : warehouses.Max(warehouse => warehouse.Id);
        }

        public IEnumerable<Warehouse> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToWarehouse)
                .ToList();
        }

        public Warehouse GetById(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(warehouse => warehouse.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public Warehouse Create(Warehouse warehouse)
        {
            warehouse.Id = ++_warehouseMaxId;
            AppendLineToFile(_path, WarehouseToCSVFormat(warehouse));
            return warehouse;
        }

        public bool Update(Warehouse warehouse)
        {
            var warehouses = GetAll();

            foreach (Warehouse r in warehouses)
            {
                if (r.Id == warehouse.Id)
                {
                    r.Name = warehouse.Name;
                    WriteLinesToFile(_path, WarehousesToCSVFormat((List<Warehouse>)warehouses));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long roomId)
        {
            var warehouses = (List<Warehouse>)GetAll();

            foreach (Warehouse w in warehouses)
            {
                if (w.Id == roomId)
                {
                    warehouses.Remove(w);
                    WriteLinesToFile(_path, WarehousesToCSVFormat((List<Warehouse>)warehouses));
                    return true;
                }
            }
            return false;
        }
        private void WriteLinesToFile(string path, List<string> lines)
        {
            File.WriteAllLines(path, lines);
        }

        private void AppendLineToFile(string path, string line)
        {
            File.AppendAllText(path, line + Environment.NewLine);
        }
        
        private string WarehouseToCSVFormat(Warehouse warehouse)
        {
            return string.Join(_delimiter,
                warehouse.Id,
                warehouse.Name);
        }
        private List<string> WarehousesToCSVFormat(List<Warehouse> warehouses)
        {
            List<string> lines = new List<string>();

            foreach (Warehouse warehouse in warehouses)
            {
                lines.Add(WarehouseToCSVFormat(warehouse));
            }
            return lines;
        }

        private Warehouse CSVFormatToWarehouse(string warehouseCSVFormat)
        {
            var tokens = warehouseCSVFormat.Split(_delimiter.ToCharArray());
            return new Warehouse(
                long.Parse(tokens[0]),
                tokens[1]);
        }
       
    }
}