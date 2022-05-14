using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdravstvena_ustanova.Model
{
    public class SurveyAnswers
    {
        public long Id { get; set; }
        public SurveyQuestions SurveyQuestions { get; set; }
        public Patient Patient { get; set; }
        public int AnswerOne { get; set; }
        public int AnswerTwo { get; set; }
        public int AnswerThree { get; set; }
        public int AnswerFour { get; set; }
        public int AnswerFive { get; set; }

        public SurveyAnswers(long id, SurveyQuestions surveyQuestions, Patient patient, int answerOne, int answerTwo, int answerThree, int answerFour, int answerFive)
        {
            Id = id;
            SurveyQuestions = surveyQuestions;
            Patient = patient;
            AnswerOne = answerOne;
            AnswerTwo = answerTwo;
            AnswerThree = answerThree;
            AnswerFour = answerFour;
            AnswerFive = answerFive;
        }
        public SurveyAnswers(long id, long surveyQuestionsId, long patientId, int answerOne, int answerTwo, int answerThree, int answerFour, int answerFive)
        {
            Id = id;
            SurveyQuestions = new SurveyQuestions(surveyQuestionsId);
            Patient = new Patient(patientId);
            AnswerOne = answerOne;
            AnswerTwo = answerTwo;
            AnswerThree = answerThree;
            AnswerFour = answerFour;
            AnswerFive = answerFive;
        }
    }
}
