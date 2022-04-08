using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Repository
{
    public class AccountsRepository
    {
        public bool Save(List<Account> accounts)
        {
            
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("..\\..\\..\\data\\accounts.txt", FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, accounts);
            stream.Close();
           
            return true;
        }

        public List<Account> Read()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("..\\..\\..\\data\\accounts.txt", FileMode.Open, FileAccess.Read);

            List<Account> accounts = (List<Account>)formatter.Deserialize(stream);
            return accounts;
        }
    }
}