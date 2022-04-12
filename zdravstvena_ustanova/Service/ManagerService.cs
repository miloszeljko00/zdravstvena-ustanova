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
        private readonly AccountRepository _accountRepository;

        public ManagerService(ManagerRepository managerRepository, AccountRepository accountRepository)
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
        public Manager GetById(long id)
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