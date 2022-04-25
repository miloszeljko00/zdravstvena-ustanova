using System;

namespace Model
{
   public class Anamnesis
   {
      public long Id { get; set; }
      public string Diagnosis { get; set; }
      public string Conclusion { get; set; }

        public Anamnesis(long id, string diagnosis, string conclusion)
        {
            Id = id;
            Diagnosis = diagnosis;
            Conclusion = conclusion;
        }
        public Anamnesis(long id)
        {
            Id = id;
        }
    }
}