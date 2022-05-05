using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using zdravstvena_ustanova.Repository;
using System.Linq;

namespace zdravstvena_ustanova.Service
{
    public class SecretaryService
    {
        private readonly SecretaryRepository _secretaryRepository;
        private readonly AccountRepository _accountRepository;

        public SecretaryService(SecretaryRepository secretaryRepository, AccountRepository accountRepository)
        {
            _secretaryRepository = secretaryRepository;
            _accountRepository = accountRepository;
        }

        public IEnumerable<Secretary> GetAll()
        {
            var secretaries = _secretaryRepository.GetAll();
            var accounts = _accountRepository.GetAll();

            BindSecretariesWithAccounts(secretaries, accounts);

            return secretaries;

        }

        private void BindSecretariesWithAccounts(IEnumerable<Secretary> secretaries, IEnumerable<Account> accounts)
        {
            foreach (Secretary s in secretaries)
            {
                BindSecretaryWithAccount(accounts, s);
            }
        }

        public Secretary GetById(long id)
        {
            var secretary = _secretaryRepository.Get(id);
            var accounts = _accountRepository.GetAll();
            BindSecretaryWithAccount(accounts, secretary);
            return secretary;
        }

        private void BindSecretaryWithAccount(IEnumerable<Account> accounts, Secretary secretary)
        {
            foreach(Account acc in accounts)
            {
                if(acc.Person.Id == secretary.Id)
                {
                    secretary.Account = acc;
                    acc.Person = secretary;
                }
            }
        }

        private Secretary FindSecretaryById(IEnumerable<Secretary> secretaries, long secretaryId)
        {
            return secretaries.SingleOrDefault(secretary => secretary.Id == secretaryId);
        }

        public Secretary Create(Secretary secretary)
        {
            return _secretaryRepository.Create(secretary);
        }
        public void Update(Secretary secretary)
        {
            _secretaryRepository.Update(secretary);
        }
        public void Delete(long secretaryId)
        {
            _secretaryRepository.Delete(secretaryId);
        }
    }
}