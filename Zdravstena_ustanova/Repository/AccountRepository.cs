using Model;
using Model.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Zdravstena_ustanova.Exception;

namespace Repository
{
    public class AccountRepository
    {
   
        private const string NOT_FOUND_ERROR = "ACCOUNT NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _accountMaxId;

        public AccountRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _accountMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<Account> accounts)
        {
            return accounts.Count() == 0 ? 0 : accounts.Max(account => account.Id);
        }

        public IEnumerable<Account> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToAccount)
                .ToList();
        }

        public Account GetById(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(account => account.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public Account Create(Account account)
        {
            account.Id = ++_accountMaxId;
            AppendLineToFile(_path, AccountToCSVFormat(account));
            return account;
        }
        public bool Update(Account account)
        {
            var accounts = GetAll();

            foreach (Account acc in accounts)
            {
                if (acc.Id == account.Id)
                {
                    acc.Username = account.Username;
                    acc.Password = account.Password;
                    acc.IsEnabled = account.IsEnabled;
                    acc.AccountType = account.AccountType;
                    WriteLinesToFile(_path, AccountsToCSVFormat((List<Account>)accounts));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long id)
        {
            var accounts = (List<Account>)GetAll();

            foreach (Account acc in accounts)
            {
                if (acc.Id == id)
                {
                    accounts.Remove(acc);
                    WriteLinesToFile(_path, AccountsToCSVFormat((List<Account>)accounts));
                    return true;
                }
            }
            return false;
        }
        private string AccountToCSVFormat(Account account)
        {   
            if(account.Person != null)
            {
                return string.Join(_delimiter,
                account.Id,
                account.Username,
                account.Password,
                account.IsEnabled,
                account.Person.Id,
                (int)account.AccountType);
            }
            return string.Join(_delimiter,
                account.Id,
                account.Username,
                account.Password,
                account.IsEnabled,
                -1,
                (int)account.AccountType);
        }

        private Account CSVFormatToAccount(string accountCSVFormat)
        {
            var tokens = accountCSVFormat.Split(_delimiter.ToCharArray());
            if(long.Parse(tokens[4]) != -1)
            {
                return new Account(
                long.Parse(tokens[0]),
                tokens[1],
                tokens[2],
                bool.Parse(tokens[3]),
                long.Parse(tokens[4]),
                (AccountType)int.Parse(tokens[5]));
            }
            return new Account(
                long.Parse(tokens[0]),
                tokens[1],
                tokens[2],
                bool.Parse(tokens[3]),
                (AccountType)int.Parse(tokens[5]));

        }
        private List<string> AccountsToCSVFormat(List<Account> accounts)
        {
            List<string> lines = new List<string>();

            foreach (Account account in accounts)
            {
                lines.Add(AccountToCSVFormat(account));
            }
            return lines;
        }

        private void AppendLineToFile(string path, string line)
        {
            File.AppendAllText(path, line + Environment.NewLine);
        }

        private void WriteLinesToFile(string path, List<string> lines)
        {
            File.WriteAllLines(path, lines);
        }
    }
}