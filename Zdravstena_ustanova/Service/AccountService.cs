using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class AccountService
    {
        public  List<Account> Accounts { get; set; }

        public  Repository.AccountsRepository accountsRepository;

        public AccountService()
        {
            Accounts = new List<Account>();
            this.accountsRepository = new Repository.AccountsRepository();
        }

        public  bool CreateAccount(Account account)
        {
            try
            {
                    Accounts.Add(account);
                    return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }

        public bool DeleteAccount(string username)
        {
            foreach (Account account in Accounts)
            {
                if (account.Username == username)
                {
                    return Accounts.Remove(account);    
                    
                }
                    
            }
            return false;
        }

        public Account GetByUsername(string username)
        {
            foreach (Account account in Accounts)
            {
                if (account.Username == username)
                {
                    return account;
                }

            }
            return null;
        }

        public bool Save()
        {
            return accountsRepository.Save(Accounts);
        }

        public bool Read()
        {
            try
            {
                Accounts = accountsRepository.Read();

                if (Accounts == null)
                    return false;

                return true;

            }catch(Exception ex)
            {
                return false;
            }
            
         }

        public void DisableAccount(string username)
        {
            foreach (Account account in Accounts)
            {
                if (account.Username == username)
                {
                    account.IsEnabled = false;
                    break;
                }

            }
        }

        public void EnableAccount(string username)
        {
            foreach (Account account in Accounts)
            {
                if (account.Username == username)
                {
                    account.IsEnabled = true;
                    break;
                }

            }
        }

        

    }
}