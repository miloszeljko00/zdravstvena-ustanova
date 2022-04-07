using Model;
using System;
using Repository;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class WarehouseService
    {
        private readonly WarehouseRepository _warehouseRepository;

        public WarehouseService(WarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public IEnumerable<Warehouse> GetAll()
        {
            return _warehouseRepository.GetAll();
        }

        private Warehouse FindWarehouseById(IEnumerable<Warehouse> warehouses, long warehouseId)
        {
            return warehouses.SingleOrDefault(warehouse => warehouse.Id == warehouseId);
        }

        public Warehouse Create(Warehouse warehouse)
        {
            return _warehouseRepository.Create(warehouse);
        }
        public bool Update(Warehouse warehouse)
        {
            return _warehouseRepository.Update(warehouse);
        }
        public bool Delete(long warehouseId)
        {
            return _warehouseRepository.Delete(warehouseId);
        }

    }
}