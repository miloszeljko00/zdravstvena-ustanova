using System;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;
using System.Collections.Generic;
using System.Linq;

namespace zdravstvena_ustanova.Controller
{
   public class SurveyAnswersController
   {
        private readonly SurveyAnswersService _surveyAnswersService;

        public SurveyAnswersController(SurveyAnswersService surveyAnswersService)
        {
            _surveyAnswersService = surveyAnswersService;
        }

        public IEnumerable<SurveyAnswers> GetAll()
        {
            return _surveyAnswersService.GetAll();
        }

        public SurveyAnswers GetById(long Id)
        {
            return _surveyAnswersService.GetById(Id);
        }

        public SurveyAnswers Create(SurveyAnswers surveyAnswers)
        {
            return _surveyAnswersService.Create(surveyAnswers);
        }
        
    }
}