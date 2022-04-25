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
    public class AllergensRepository
    {
        private const string NOT_FOUND_ERROR = "ALLERGENS NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _allergensMaxId;

        public AllergensRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _allergensMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<Allergens> allergens)
        {
            return allergens.Count() == 0 ? 0 : allergens.Max(allergen => allergen.Id);
        }

        public IEnumerable<Allergens> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToAllergen)
                .ToList();
        }

        public Allergens Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(allergen => allergen.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public Allergens Create(Allergens allergen)
        {
            allergen.Id = ++_allergensMaxId;
            AppendLineToFile(_path, AllergenToCSVFormat(allergen));
            return allergen;
        }

        public bool Update(Allergens allergen)
        {
            var allergens = GetAll();

            foreach (Allergens a in allergens)
            {
                if (a.Id == allergen.Id)
                {
                    a.Name = allergen.Name;
                    a.Ingredients = allergen.Ingredients;
                    WriteLinesToFile(_path, AllergensToCSVFormat((List<Allergens>)allergens));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long allergenId)
        {
            var allergens = (List<Allergens>)GetAll();

            foreach (Allergens a in allergens)
            {
                if (a.Id == allergenId)
                {
                    allergens.Remove(a);
                    WriteLinesToFile(_path, AllergensToCSVFormat((List<Allergens>)allergens));
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

        private string AllergenToCSVFormat(Allergens allergen)
        {
            int count = allergen.Ingredients.Count;
            string ingredients = "";
            for (int i = 0; i < count; i++)
            {
                ingredients = string.Join(_delimiter, ingredients, allergen.Ingredients[i].Id);
            }

            return string.Join(_delimiter,
                allergen.Id,
                allergen.Name,
                count,
                ingredients
                );
        }
        private List<string> AllergensToCSVFormat(List<Allergens> allergens)
        {
            List<string> lines = new List<string>();

            foreach (Allergens allergen in allergens)
            {
                lines.Add(AllergenToCSVFormat(allergen));
            }
            return lines;
        }

        private Allergens CSVFormatToAllergen(string allergenCSVFormat)
        {
            var tokens = allergenCSVFormat.Split(_delimiter.ToCharArray());

            List<Ingredient> ingredients = new List<Ingredient>();
            for (int i = 2 + 1; i < 2 + 1 + int.Parse(tokens[2]); i++)
            {
                var ingredient = new Ingredient(int.Parse(tokens[i]));
                ingredients.Add(ingredient);
            }

            return new Allergens(
                long.Parse(tokens[0]),
                tokens[1],
                ingredients
                );
        }

    }
}
