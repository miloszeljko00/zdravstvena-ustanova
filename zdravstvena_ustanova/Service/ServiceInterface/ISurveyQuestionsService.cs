using System.Collections.Generic;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Service.ServiceInterface;

public interface ISurveyQuestionsService : IService<SurveyQuestions>
{
    IEnumerable<SurveyQuestions> GetAllUnique();
}