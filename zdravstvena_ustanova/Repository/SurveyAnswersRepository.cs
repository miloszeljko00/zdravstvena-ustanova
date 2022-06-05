using zdravstvena_ustanova.Exception;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using zdravstvena_ustanova.Repository.RepositoryInterface;

namespace zdravstvena_ustanova.Repository
{
    public class SurveyAnswersRepository : ISurveyAnswersRepository
    {
        private const string NOT_FOUND_ERROR = "SURVEY ANSWERS NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _surveyAnswersMaxId;

        public SurveyAnswersRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _surveyAnswersMaxId = GetMaxId(GetAll());
        }

        private long GetMaxId(IEnumerable<SurveyAnswers> surveysAnswers)
        {
            return surveysAnswers.Count() == 0 ? 0 : surveysAnswers.Max(surveyAnswers => surveyAnswers.Id);
        }

        public IEnumerable<SurveyAnswers> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToSurveyAnswers)
                .ToList();
        }

        public SurveyAnswers Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(surveyAnswers => surveyAnswers.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public SurveyAnswers Create(SurveyAnswers surveyAnswers)
        {
            surveyAnswers.Id = ++_surveyAnswersMaxId;
            AppendLineToFile(_path, SurveyAnswersToCSVFormat(surveyAnswers));
            return surveyAnswers;
        }

        private string SurveyAnswersToCSVFormat(SurveyAnswers surveyAnswers)
        {
            return string.Join(_delimiter,
                surveyAnswers.Id,
                surveyAnswers.SurveyQuestions.Id,
                surveyAnswers.Patient.Id,
                surveyAnswers.AnswerOne,
                surveyAnswers.AnswerTwo,
                surveyAnswers.AnswerThree,
                surveyAnswers.AnswerFour,
                surveyAnswers.AnswerFive
                );
        }

        private void AppendLineToFile(string path, string line)
        {
            File.AppendAllText(path, line + Environment.NewLine);
        }

        private SurveyAnswers CSVFormatToSurveyAnswers(string surveyAnswersCSVFormat)
        {
            var tokens = surveyAnswersCSVFormat.Split(_delimiter.ToCharArray());

            return new SurveyAnswers(
               long.Parse(tokens[0]),
               long.Parse(tokens[1]),
               long.Parse(tokens[2]),
               int.Parse(tokens[3]),
               int.Parse(tokens[4]),
               int.Parse(tokens[5]),
               int.Parse(tokens[6]),
               int.Parse(tokens[7])
                );
        }

        public bool Update(SurveyAnswers t)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}