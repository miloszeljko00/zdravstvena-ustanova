using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Controller
{
    public class WarehouseController
    {
        private readonly WarehouseService _warehouseService;

        public WarehouseController(WarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        public IEnumerable<Warehouse> GetAll()
        {
            return _warehouseService.GetAll();
        }

        public Warehouse Create(Warehouse warehouse)
        {
            return _warehouseService.Create(warehouse);
        }
        public bool Update(Warehouse warehouse)
        {
            return _warehouseService.Update(warehouse);
        }
        public bool Delete(long roomId)
        {
            return _warehouseService.Delete(roomId);
        }
    }
}
