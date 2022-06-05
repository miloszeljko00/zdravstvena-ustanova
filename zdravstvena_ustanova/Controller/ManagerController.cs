using System;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;
using System.Collections.Generic;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Controller
{
   public class ManagerController
   {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
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