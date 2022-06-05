using zdravstvena_ustanova.Model;
using System.Collections.Generic;
using System.Linq;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class SecretaryService : ISecretaryService
    {
        private readonly ISecretaryRepository _secretaryRepository;
        private readonly IAccountRepository _accountRepository;

        public SecretaryService(ISecretaryRepository secretaryRepository, IAccountRepository accountRepository)
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

        public Secretary Get(long id)
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

        public Secretary Create(Secretary secretary)
        {
            return _secretaryRepository.Create(secretary);
        }
        public bool Update(Secretary secretary)
        {
            return _secretaryRepository.Update(secretary);
        }
        public bool Delete(long secretaryId)
        {
            return _secretaryRepository.Delete(secretaryId);
        }
    }
}