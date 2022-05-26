using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.View.Model;

namespace zdravstvena_ustanova.View.Pages.ManagerPages
{
    /// <summary>
    /// Interaction logic for PollResultsPage.xaml
    /// </summary>
    public partial class PollResultsPage : Page, INotifyPropertyChanged
    {
        public ObservableCollection<QuestionScore> QuestionScores { get; set; }
        #region NotifyProperties
        private QuestionScore _selectedQuestionScore;
        public QuestionScore SelectedQuestionScore
        {
            get
            {
                return _selectedQuestionScore;
            }
            set
            {
                if (value != _selectedQuestionScore)
                {
                    _selectedQuestionScore = value;
                    OnPropertyChanged("SelectedQuestionScore");
                }
            }
        }
        private double _totalScore;
        public double TotalScore
        {
            get
            {
                return _totalScore;
            }
            set
            {
                if (value != _totalScore)
                {
                    _totalScore = value;
                    OnPropertyChanged("TotalScore");
                }
            }
        }
        private string _surveyName;
        public string SurveyName
        {
            get
            {
                return _surveyName;
            }
            set
            {
                if (value != _surveyName)
                {
                    _surveyName = value;
                    OnPropertyChanged("SurveyName");
                }
            }
        }
        private int _totalScoreCount;
        public int TotalScoreCount
        {
            get
            {
                return _totalScoreCount;
            }
            set
            {
                if (value != _totalScoreCount)
                {
                    _totalScoreCount = value;
                    OnPropertyChanged("TotalScoreCount");
                }
            }
        }

        #endregion
        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public PollResultsPage(SurveyQuestions selectedSurvey)
        {
            InitializeComponent();
            DataContext = this;
            QuestionScores = new ObservableCollection<QuestionScore>();

            var app = Application.Current as App;

            var surveyAnswers = app.SurveyAnswersController.GetBySurveyQuestionsName(selectedSurvey.Name);

            var questionScore1 = new QuestionScore(selectedSurvey.QuestionOne, 0, 0, 0, 0, 0);
            var questionScore2 = new QuestionScore(selectedSurvey.QuestionTwo, 0, 0, 0, 0, 0);
            var questionScore3 = new QuestionScore(selectedSurvey.QuestionThree, 0, 0, 0, 0, 0);
            var questionScore4 = new QuestionScore(selectedSurvey.QuestionFour, 0, 0, 0, 0, 0);
            var questionScore5 = new QuestionScore(selectedSurvey.QuestionFive, 0, 0, 0, 0, 0);
           
            foreach (var surveyAnswer in surveyAnswers)
            {
                switch (surveyAnswer.AnswerOne)
                {
                    case 1:
                        questionScore1.AnswersOneCount++;
                        break;
                    case 2:
                        questionScore1.AnswersTwoCount++;
                        break;
                    case 3:
                        questionScore1.AnswersThreeCount++;
                        break;
                    case 4:
                        questionScore1.AnswersFourCount++;
                        break;
                    case 5:
                        questionScore1.AnswersFiveCount++;
                        break;
                }
                switch (surveyAnswer.AnswerTwo)
                {
                    case 1:
                        questionScore2.AnswersOneCount++;
                        break;
                    case 2:
                        questionScore2.AnswersTwoCount++;
                        break;
                    case 3:
                        questionScore2.AnswersThreeCount++;
                        break;
                    case 4:
                        questionScore2.AnswersFourCount++;
                        break;
                    case 5:
                        questionScore2.AnswersFiveCount++;
                        break;
                }
                switch (surveyAnswer.AnswerThree)
                {
                    case 1:
                        questionScore3.AnswersOneCount++;
                        break;
                    case 2:
                        questionScore3.AnswersTwoCount++;
                        break;
                    case 3:
                        questionScore3.AnswersThreeCount++;
                        break;
                    case 4:
                        questionScore3.AnswersFourCount++;
                        break;
                    case 5:
                        questionScore3.AnswersFiveCount++;
                        break;
                }
                switch (surveyAnswer.AnswerFour)
                {
                    case 1:
                        questionScore4.AnswersOneCount++;
                        break;
                    case 2:
                        questionScore4.AnswersTwoCount++;
                        break;
                    case 3:
                        questionScore4.AnswersThreeCount++;
                        break;
                    case 4:
                        questionScore4.AnswersFourCount++;
                        break;
                    case 5:
                        questionScore4.AnswersFiveCount++;
                        break;
                }
                switch (surveyAnswer.AnswerFive)
                {
                    case 1:
                        questionScore5.AnswersOneCount++;
                        break;
                    case 2:
                        questionScore5.AnswersTwoCount++;
                        break;
                    case 3:
                        questionScore5.AnswersThreeCount++;
                        break;
                    case 4:
                        questionScore5.AnswersFourCount++;
                        break;
                    case 5:
                        questionScore5.AnswersFiveCount++;
                        break;
                }
            }


            questionScore1.RecalculateAvgAndTotal();
            questionScore2.RecalculateAvgAndTotal();
            questionScore3.RecalculateAvgAndTotal();
            questionScore4.RecalculateAvgAndTotal();
            questionScore5.RecalculateAvgAndTotal();
            QuestionScores.Add(questionScore1);
            QuestionScores.Add(questionScore2);
            QuestionScores.Add(questionScore3);
            QuestionScores.Add(questionScore4);
            QuestionScores.Add(questionScore5);

            TotalScoreCount = 5;
            TotalScore = (questionScore1.AvgScore +
                         questionScore2.AvgScore +
                         questionScore3.AvgScore +
                         questionScore4.AvgScore +
                         questionScore5.AvgScore) / TotalScoreCount;
            SurveyName = selectedSurvey.Name;
        }

        private void SurveysDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedQuestionScore = (QuestionScore)SurveysDataGrid.SelectedItem;
        }
    }
}
