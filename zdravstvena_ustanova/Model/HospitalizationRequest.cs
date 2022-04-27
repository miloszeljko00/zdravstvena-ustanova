using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdravstvena_ustanova.Model
{
    public class HospitalizationRequest
    {
        public long Id { get; set; }
        public DateTime ReqDateOfAdmission { get; set; }
        public string Cause { get; set; }

        public HospitalizationRequest(long id, DateTime reqDateOfAdmission, string cause)
        {
            Id = id;
            ReqDateOfAdmission = reqDateOfAdmission;
            Cause = cause;
        }
        public HospitalizationRequest(long id)
        {
            Id = id;
        }
    }
}
