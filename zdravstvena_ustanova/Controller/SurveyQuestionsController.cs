using System;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;
using System.Collections.Generic;
using System.Linq;

namespace zdravstvena_ustanova.Controller
{
   public class SurveyQuestionsController
    {
        private readonly SurveyQuestionsService _surveyQuestionsService;

        public SurveyQuestionsController(SurveyQuestionsService surveyQuestionsService)
        {
            _surveyQuestionsService = surveyQuestionsService;
        }

        public IEnumerable<SurveyQuestions> GetAll()
        {
            return _surveyQuestionsService.GetAll();
        }

        public SurveyQuestions GetById(long Id)
        {
            return _surveyQuestionsService.GetById(Id);
        }
        
    }
}