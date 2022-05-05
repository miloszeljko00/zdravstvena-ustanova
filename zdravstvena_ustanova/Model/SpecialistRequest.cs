using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdravstvena_ustanova.Model
{
    public class SpecialistRequest
    {
        public long Id { get; set; }
        public  string Cause { get; set; }
        public bool IsUrgent { get; set; }
        public Specialty Specialty { get; set; }

        public SpecialistRequest(long id, string cause, bool isUrgent, Specialty specialty)
        {
            Id = id;
            Cause = cause;
            IsUrgent = isUrgent;
            Specialty = specialty;
        }

        public SpecialistRequest(long id, string cause, bool isUrgent, long specialtyId)
        {
            Id = id;
            Cause = cause;
            IsUrgent = isUrgent;
            Specialty = new Specialty(specialtyId);
        }
        public SpecialistRequest(long id)
        {
            Id = id;
        }
    }
}
