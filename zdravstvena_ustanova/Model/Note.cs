using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdravstvena_ustanova.Model
{
    public class Note
    {
        public long Id { get; set; } 
        public Patient Patient { get; set; }   
        public string Name { get; set; }
        public string Content { get; set; }
        public string Time { get; set; }

        public Note(long id, Patient patient, string name, string content, string time)
        {
            Id = id;
            Patient = patient;
            Name = name;
            Content = content;
            Time = time;
        }
        public Note(long id, long patientId, string name, string content, string time)
        {
            Id = id;
            Patient = new Patient(patientId);
            Name = name;
            Content = content;
            Time = time;
        }
    }
}
