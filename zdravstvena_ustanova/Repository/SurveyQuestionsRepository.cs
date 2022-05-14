using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using zdravstvena_ustanova.Exception;
using System.Globalization;

namespace zdravstvena_ustanova.Repository
{
    public class SurveyQuestionsRepository
    {
   
        private const string NOT_FOUND_ERROR = "SURVEY NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;

        public SurveyQuestionsRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
        }

        public IEnumerable<SurveyQuestions> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToSurvey)
                .ToList();
        }

        public SurveyQuestions GetById(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(surveyQuestions => surveyQuestions.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        private SurveyQuestions CSVFormatToSurvey(string surveyCSVFormat)
        {
            var tokens = surveyCSVFormat.Split(_delimiter.ToCharArray());
            var timeFormat = "dd.MM.yyyy. HH:mm";
            DateTime dateOfPublication;
            if (!DateTime.TryParseExact(tokens[3], timeFormat, CultureInfo.InvariantCulture
                                                , DateTimeStyles.None
                                                , out dateOfPublication))
            {
                return new SurveyQuestions(
                long.Parse(tokens[0]),
                long.Parse(tokens[1]),
                (SurveyType)int.Parse(tokens[2]),
                tokens[4],
                tokens[5],
                tokens[6],
                tokens[7],
                tokens[8],
                tokens[9]);
            }

            return new SurveyQuestions(
                long.Parse(tokens[0]),
                long.Parse(tokens[1]),
                (SurveyType)int.Parse(tokens[2]),
                dateOfPublication,
                tokens[4],
                tokens[5],
                tokens[6],
                tokens[7],
                tokens[8],
                tokens[9]);

        }
    }
}