using System;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Controller
{
   public class SurveyQuestionsController
    {
        private readonly ISurveyQuestionsService _surveyQuestionsService;

        public SurveyQuestionsController(ISurveyQuestionsService surveyQuestionsService)
        {
            _surveyQuestionsService = surveyQuestionsService;
        }

        public IEnumerable<SurveyQuestions> GetAll()
        {
            return _surveyQuestionsService.GetAll();
        }

        public SurveyQuestions GetById(long Id)
        {
            return _surveyQuestionsService.Get(Id);
        }

        public IEnumerable<SurveyQuestions> GetAllUnique()
        {
            return _surveyQuestionsService.GetAllUnique();
        }
    }
}