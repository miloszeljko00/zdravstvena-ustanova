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
    /// Interaction logic for PollsOverviewPage.xaml
    /// </summary>
    public partial class PollsOverviewPage : Page
    {
        public ObservableCollection<SurveyQuestions> Surveys { get; set; }
        public PollsOverviewPage()
        {
            InitializeComponent();
            DataContext = this;
            Surveys = new ObservableCollection<SurveyQuestions>();
            var app = Application.Current as App;

            var surveyQuestions =  app.SurveyQuestionsController.GetAllUnique();
            foreach (var surveyQuestion in surveyQuestions)
            {
                Surveys.Add(surveyQuestion);
            }
        }

        private void ListOfQuestionsButton_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedSurvey = SurveysDataGrid.SelectedItem;
            if(selectedSurvey == null) return;
            NavigationService.Navigate(new PollsQuestionsOverviewPage((SurveyQuestions) selectedSurvey));
        }

        private void SurveyResultsButton_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedSurvey = SurveysDataGrid.SelectedItem;
            if (selectedSurvey == null) return;
            NavigationService.Navigate(new PollResultsPage((SurveyQuestions)selectedSurvey));
        }

        private void SearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            return;
        }

        private void DeleteSurveyIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            return;
        }

        private void EditSurveyIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            return;
        }

        private void AddSurveyIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            return;
        }

        private void SurveysDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SurveysDataGrid.SelectedItem == null)
            {
                DeleteIcon.IsEnabled = false;
                EditIcon.IsEnabled = false;
                SurveyResultsButton.IsEnabled = false;
                ListOfQuestionsButton.IsEnabled = false;
            }
            else
            {
                DeleteIcon.IsEnabled = true;
                EditIcon.IsEnabled = true;
                SurveyResultsButton.IsEnabled = true;
                ListOfQuestionsButton.IsEnabled = true;
            }
        }
    }
}
