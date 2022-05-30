using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using zdravstvena_ustanova.Exception;
using zdravstvena_ustanova.Repository.RepositoryInterface;

namespace zdravstvena_ustanova.Repository
{
    public class IngredientRepository : IIngredientRepository
    {
        private const string NOT_FOUND_ERROR = "INGREDIENT NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _ingredientMaxId;

        public IngredientRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _ingredientMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<Ingredient> ingredients)
        {
            return ingredients.Count() == 0 ? 0 : ingredients.Max(ingredient => ingredient.Id);
        }

        public IEnumerable<Ingredient> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToIngredient)
                .ToList();
        }

        public Ingredient Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(ingredient => ingredient.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public Ingredient Create(Ingredient ingredient)
        {
            ingredient.Id = ++_ingredientMaxId;
            AppendLineToFile(_path, IngredientToCSVFormat(ingredient));
            return ingredient;
        }
        public bool Update(Ingredient ingredient)
        {
            var ingredients = GetAll();

            foreach (Ingredient i in ingredients)
            {
                if (i.Id == ingredient.Id)
                {
                    i.Name = ingredient.Name;
                    WriteLinesToFile(_path, IngredientsToCSVFormat((List<Ingredient>)ingredients));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long ingredientId)
        {
            var ingredients = (List<Ingredient>)GetAll();

            foreach (Ingredient i in ingredients)
            {
                if (i.Id == ingredientId)
                {
                    ingredients.Remove(i);
                    WriteLinesToFile(_path, IngredientsToCSVFormat((List<Ingredient>)ingredients));
                    return true;
                }
            }
            return false;
        }
        private string IngredientToCSVFormat(Ingredient ingredient)
        {
            return string.Join(_delimiter,
                ingredient.Id,
                ingredient.Name
                );
        }

        private Ingredient CSVFormatToIngredient(string ingredientCSVFormat)
        {
            var tokens = ingredientCSVFormat.Split(_delimiter.ToCharArray());
            return new Ingredient(
                long.Parse(tokens[0]),
                tokens[1]
                );
        }
        private List<string> IngredientsToCSVFormat(List<Ingredient> ingredients)
        {
            List<string> lines = new List<string>();

            foreach (Ingredient ingredient in ingredients)
            {
                lines.Add(IngredientToCSVFormat(ingredient));
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