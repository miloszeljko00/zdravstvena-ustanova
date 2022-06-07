using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdravstvena_ustanova.Model
{
    public class Notification
    {
        public long Id { get; set; }
        public Account Receiver { get; set; }
        public string Message { get; set; }

        public Notification(long id, Account receiver, string message)
        {
            Id = id;
            Receiver = receiver;
            Message = message;
        }


    }
}
