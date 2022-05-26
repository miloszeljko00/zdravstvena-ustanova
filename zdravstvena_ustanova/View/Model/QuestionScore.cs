using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdravstvena_ustanova.View.Model
{
    public class QuestionScore
    {
        public string Question { get; set; }
        public double AvgScore { get; set; }
        public int TotalAnswersCount { get; set; }
        public int AnswersOneCount { get; set; }
        public int AnswersTwoCount { get; set; }
        public int AnswersThreeCount { get; set; }
        public int AnswersFourCount { get; set; }
        public int AnswersFiveCount { get; set; }

        public QuestionScore(string question, int answersOneCount, int answersTwoCount, int answersThreeCount, int answersFourCount, int answersFiveCount)
        {
            Question = question;
            AnswersOneCount = answersOneCount;
            AnswersTwoCount = answersTwoCount;
            AnswersThreeCount = answersThreeCount;
            AnswersFourCount = answersFourCount;
            AnswersFiveCount = answersFiveCount;
            TotalAnswersCount = AnswersOneCount + AnswersTwoCount + AnswersThreeCount + AnswersFourCount + AnswersFiveCount;
            AvgScore = AnswersOneCount * 1 + AnswersTwoCount * 2 + AnswersThreeCount * 3 + AnswersFourCount * 4 + AnswersFiveCount * 5;
            AvgScore /= TotalAnswersCount;
        }

        public void RecalculateAvgAndTotal()
        {
            TotalAnswersCount = AnswersOneCount + AnswersTwoCount + AnswersThreeCount + AnswersFourCount + AnswersFiveCount;
            AvgScore = AnswersOneCount * 1 + AnswersTwoCount * 2 + AnswersThreeCount * 3 + AnswersFourCount * 4 + AnswersFiveCount * 5;
            AvgScore /= TotalAnswersCount;
        }
    }
}
