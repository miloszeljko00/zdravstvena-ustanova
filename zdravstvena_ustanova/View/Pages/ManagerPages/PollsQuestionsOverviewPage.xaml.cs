using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Interaction logic for PollsQuestionsOverviewPage.xaml
    /// </summary>
    public partial class PollsQuestionsOverviewPage : Page
    {
        public string SurveyName { get; set; }
        public ObservableCollection<SurveyQuestion> SurveyQuestions { get; set; }
        public PollsQuestionsOverviewPage(SurveyQuestions survey)
        {
            InitializeComponent();
            DataContext = this;

            SurveyQuestions = new ObservableCollection<SurveyQuestion>();
            SurveyQuestions.Add(new SurveyQuestion(survey.QuestionOne));
            SurveyQuestions.Add(new SurveyQuestion(survey.QuestionTwo));
            SurveyQuestions.Add(new SurveyQuestion(survey.QuestionThree));
            SurveyQuestions.Add(new SurveyQuestion(survey.QuestionFour));
            SurveyQuestions.Add(new SurveyQuestion(survey.QuestionFive));
        }

        private void SearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DeleteSurveyQuestionIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EditSurveyQuestionIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddSurveyQuestionIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SurveyQuestionsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SurveyQuestionsDataGrid.SelectedItem == null)
            {
                DeleteIcon.IsEnabled = false;
                EditIcon.IsEnabled = false;
            }
            else
            {
                DeleteIcon.IsEnabled = true;
                EditIcon.IsEnabled = true;
            }
        }
    }
}
