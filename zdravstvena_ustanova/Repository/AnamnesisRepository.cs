using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Exception;

namespace zdravstvena_ustanova.Repository
{
    public class AnamnesisRepository
    {
        private const string NOT_FOUND_ERROR = "ANAMNESIS NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _anamnesisMaxId;

        public AnamnesisRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _anamnesisMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<Anamnesis> anamnesiss)
        {
            return anamnesiss.Count() == 0 ? 0 : anamnesiss.Max(anamnesis => anamnesis.Id);
        }

        public IEnumerable<Anamnesis> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToAnamnesis)
                .ToList();
        }

        public Anamnesis Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(anamnesis => anamnesis.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public Anamnesis Create(Anamnesis anamnesis)
        {
            anamnesis.Id = ++_anamnesisMaxId;
            AppendLineToFile(_path, AnamnesisToCSVFormat(anamnesis));
            return anamnesis;
        }

        public bool Update(Anamnesis anamnesis)
        {
            var anamnesiss = GetAll();

            foreach (Anamnesis a in anamnesiss)
            {
                if (a.Id == anamnesis.Id)
                {
                    a.Diagnosis = anamnesis.Diagnosis;
                    a.Conclusion = anamnesis.Conclusion;
                    WriteLinesToFile(_path, AnamnesissToCSVFormat((List<Anamnesis>)anamnesiss));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long anamnesisId)
        {
            var anamnesiss = (List<Anamnesis>)GetAll();

            foreach (Anamnesis a in anamnesiss)
            {
                if (a.Id == anamnesisId)
                {
                    anamnesiss.Remove(a);
                    WriteLinesToFile(_path, AnamnesissToCSVFormat((List<Anamnesis>)anamnesiss));
                    return true;
                }
            }
            return false;
        }
        private void WriteLinesToFile(string path, List<string> lines)
        {
            File.WriteAllLines(path, lines);
        }

        private void AppendLineToFile(string path, string line)
        {
            File.AppendAllText(path, line + Environment.NewLine);
        }

        private string AnamnesisToCSVFormat(Anamnesis anamnesis)
        {
            return string.Join(_delimiter,
                anamnesis.Id,
                anamnesis.Diagnosis,
                anamnesis.Conclusion
                );
        }
        private List<string> AnamnesissToCSVFormat(List<Anamnesis> anamnesiss)
        {
            List<string> lines = new List<string>();

            foreach (Anamnesis anamnesis in anamnesiss)
            {
                lines.Add(AnamnesisToCSVFormat(anamnesis));
            }
            return lines;
        }

        private Anamnesis CSVFormatToAnamnesis(string anamnesisCSVFormat)
        {
            var tokens = anamnesisCSVFormat.Split(_delimiter.ToCharArray());
            return new Anamnesis(
                long.Parse(tokens[0]),
                tokens[1],
                tokens[2]
                );
        }

    }
}
