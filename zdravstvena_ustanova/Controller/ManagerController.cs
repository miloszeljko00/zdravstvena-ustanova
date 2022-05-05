using System;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;
using System.Collections.Generic;

namespace zdravstvena_ustanova.Controller
{
   public class ManagerController
   {
        private readonly ManagerService _managerService;

        public ManagerController(ManagerService managerService)
        {
            _managerService = managerService;
        }

        public IEnumerable<Manager> GetAll()
        {
            return _managerService.GetAll();
        }

        public Manager Create(Manager manager)
        {
            return _managerService.Create(manager);
        }
        public void Update(Manager manager)
        {
            _managerService.Update(manager);
        }
        public void Delete(long managerId)
        {
            _managerService.Delete(managerId);
        }
    }
}