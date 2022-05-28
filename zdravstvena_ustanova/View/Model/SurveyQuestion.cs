using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdravstvena_ustanova.View.Model
{
    public class SurveyQuestion
    {
        public string Question { get; set; }

        public SurveyQuestion(string question)
        {
            Question = question;
        }
    }
}
