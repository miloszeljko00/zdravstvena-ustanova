﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using Model;

namespace Controller
{
    public class AccountController
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        public IEnumerable<Account> GetAll()
        {
            return _accountService.GetAll();
        }
        public Account GetById(long Id)
        {
            return _accountService.GetById(Id);
        }

        public Account Create(Account account)
        {
            return _accountService.Create(account);
        }
        public void Update(Account account)
        {
            _accountService.Update(account);
        }
        public void Delete(long accountId)
        {
            _accountService.Delete(accountId);
        }

        public void EnableAccount(Account account)
        {
            account.IsEnabled = true;
            Update(account);
        }

        public void DisableAccount(Account account)
        {
            account.IsEnabled = false;
            Update(account);
        }
    }
}