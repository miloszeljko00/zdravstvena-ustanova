using System;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;
using System.Collections.Generic;
using System.Linq;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Controller
{
   public class SurveyAnswersController
   {
        private readonly ISurveyAnswersService _surveyAnswersService;

        public SurveyAnswersController(ISurveyAnswersService surveyAnswersService)
        {
            _surveyAnswersService = surveyAnswersService;
        }

        public IEnumerable<SurveyAnswers> GetAll()
        {
            return _surveyAnswersService.GetAll();
        }

        public IEnumerable<SurveyAnswers> GetAnswersByPatient(long patientId)
        {
            return _surveyAnswersService.GetAnswersByPatient(patientId);
        }
        public SurveyAnswers GetById(long Id)
        {
            return _surveyAnswersService.Get(Id);
        }

        public SurveyAnswers Create(SurveyAnswers surveyAnswers)
        {
            return _surveyAnswersService.Create(surveyAnswers);
        }

        public IEnumerable<SurveyAnswers> GetBySurveyQuestionsName(string surveyName)
        {
            return _surveyAnswersService.GetBySurveyQuestionsName(surveyName);
        }
   }
}