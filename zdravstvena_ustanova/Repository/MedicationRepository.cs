using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Exception;

namespace zdravstvena_ustanova.Repository
{
    public class MedicationRepository
    {
        private const string NOT_FOUND_ERROR = "MEDICATION NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _medicationMaxId;

        public MedicationRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _medicationMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<Medication> medications)
        {
            return medications.Count() == 0 ? 0 : medications.Max(medication => medication.Id);
        }

        public IEnumerable<Medication> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToMedication)
                .ToList();
        }

        public Medication Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(medication => medication.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public Medication Create(Medication medication)
        {
            medication.Id = ++_medicationMaxId;
            AppendLineToFile(_path, MedicationToCSVFormat(medication));
            return medication;
        }

        public bool Update(Medication medication)
        {
            var medications = GetAll();

            foreach (Medication m in medications)
            {
                if (m.Id == medication.Id)
                {
                    m.Name = medication.Name;
                    m.MedicationType = medication.MedicationType;
                    m.Quantity = medication.Quantity;
                    m.IsApproved = medication.IsApproved;
                    m.Ingredients = medication.Ingredients;
                    WriteLinesToFile(_path, MedicationsToCSVFormat((List<Medication>)medications));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long medicationId)
        {
            var medications = (List<Medication>)GetAll();

            foreach (Medication m in medications)
            {
                if (m.Id == medicationId)
                {
                    medications.Remove(m);
                    WriteLinesToFile(_path, MedicationsToCSVFormat((List<Medication>)medications));
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

        private string MedicationToCSVFormat(Medication medication)
        {
            int count = medication.Ingredients.Count;
            string ingredients = count.ToString();
            for(int i=0; i<count;i++)
            {
                ingredients = string.Join(_delimiter, ingredients, medication.Ingredients[i].Id);
            }



            return string.Join(_delimiter,
                medication.Id,
                medication.Name,
                medication.MedicationType.Id,
                medication.Quantity,
                medication.IsApproved,
                count,
                ingredients
                );
        }
        private List<string> MedicationsToCSVFormat(List<Medication> medications)
        {
            List<string> lines = new List<string>();

            foreach (Medication medication in medications)
            {
                lines.Add(MedicationToCSVFormat(medication));
            }
            return lines;
        }

        private Medication CSVFormatToMedication(string medicationCSVFormat)
        {
            var tokens = medicationCSVFormat.Split(_delimiter.ToCharArray());

            List<Ingredient> ingredients = new List<Ingredient>();
            for(int i = 6; i < 6 + int.Parse(tokens[5]); i++)
            {
                var ingredient = new Ingredient(int.Parse(tokens[i]));
                ingredients.Add(ingredient);
            }

            return new Medication(
                long.Parse(tokens[0]),
                tokens[1],
                long.Parse(tokens[2]),
                int.Parse(tokens[3]),
                bool.Parse(tokens[4]),
                ingredients
                );
        }

    }
}
