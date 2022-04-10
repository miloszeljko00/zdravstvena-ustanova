using Model;
using System;
using System.Collections.Generic;
using Repository;
using System.Linq;

namespace Service
{
    public class ManagerService
    {
        private readonly ManagerRepository _managerRepository;

        public ManagerService(ManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        internal IEnumerable<Manager> GetAll()
        {
            var managers = _managerRepository.GetAll();
            return managers;
        }

        private Manager FindManagerById(IEnumerable<Manager> managers, long managerId)
        {
            return managers.SingleOrDefault(manager => manager.Id == managerId);
        }

        public Manager Create(Manager manager)
        {
            return _managerRepository.Create(manager);
        }
        public void Update(Manager manager)
        {
            _managerRepository.Update(manager);
        }
        public void Delete(long managerId)
        {
            _managerRepository.Delete(managerId);
        }
    }
}