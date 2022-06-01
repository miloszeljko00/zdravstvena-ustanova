using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using zdravstvena_ustanova.Repository;
using System.Linq;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IAccountRepository _accountRepository;

        public ManagerService(IManagerRepository managerRepository, IAccountRepository accountRepository)
        {
            _managerRepository = managerRepository;
            _accountRepository = accountRepository;
        }

        public IEnumerable<Manager> GetAll()
        {
            var accounts = _accountRepository.GetAll();
            var managers = _managerRepository.GetAll();
            BindManagersWithAccounts(accounts, managers);
            return managers;
        }
        public Manager Get(long id)
        {
            var accounts = _accountRepository.GetAll();
            var manager = _managerRepository.Get(id);
            BindManagerWithAccount(accounts, manager);
            return manager;
        }
        private void BindManagersWithAccounts(IEnumerable<Account> accounts,IEnumerable<Manager> managers)
        {
            managers.ToList().ForEach(manager =>
            {
                BindManagerWithAccount(accounts, manager);
            });
        }
        private void BindManagerWithAccount(IEnumerable<Account> accounts, Manager manager)
        {
            accounts.ToList().ForEach(account =>
            {
                if (account.Id == manager.Account.Id)
                {
                    account.Person = manager;
                    manager.Account = account;
                }
            });
        }
        private Manager FindManagerById(IEnumerable<Manager> managers, long managerId)
        {
            return managers.SingleOrDefault(manager => manager.Id == managerId);
        }

        public Manager Create(Manager manager)
        {
            return _managerRepository.Create(manager);
        }
        public bool Update(Manager manager)
        {
            return _managerRepository.Update(manager);
        }
        public bool Delete(long managerId)
        {
            return _managerRepository.Delete(managerId);
        }
    }
}