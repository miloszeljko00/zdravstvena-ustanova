using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.View
{
    /// <summary>
    /// Interaction logic for SurveyDetails.xaml
    /// </summary>
    public partial class SurveyDetails : Window
    {
        public SurveyQuestions sq { get; set; }
        public SurveyAnswers sa { get; set; }
        public SurveyDetails(SurveyQuestions surveyQuestions)
        {
            InitializeComponent();
            sq = surveyQuestions;
            qOne.Content += sq.QuestionOne;
            qTwo.Content += sq.QuestionTwo;
            qThree.Content += sq.QuestionThree;
            qFour.Content += sq.QuestionFour;
            qFive.Content += sq.QuestionFive;
        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void submitSurvey(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            int checkedval1, checkedval2, checkedval3, checkedval4, checkedval5;
            if ((bool)opt1.IsChecked)
                checkedval1 = 1;
            else if ((bool)opt2.IsChecked)
                checkedval1 = 2;
            else if ((bool)opt3.IsChecked)
                checkedval1 = 3;
            else if ((bool)opt4.IsChecked)
                checkedval1 = 4;
            else
                checkedval1 = 5;
            if ((bool)opt6.IsChecked)
                checkedval2 = 1;
            else if ((bool)opt7.IsChecked)
                checkedval2 = 2;
            else if ((bool)opt8.IsChecked)
                checkedval2 = 3;
            else if ((bool)opt9.IsChecked)
                checkedval2 = 4;
            else
                checkedval2 = 5;
            if ((bool)opt11.IsChecked)
                checkedval3 = 1;
            else if ((bool)opt12.IsChecked)
                checkedval3 = 2;
            else if ((bool)opt13.IsChecked)
                checkedval3 = 3;
            else if ((bool)opt14.IsChecked)
                checkedval3 = 4;
            else
                checkedval3 = 5;
            if ((bool)opt16.IsChecked)
                checkedval4 = 1;
            else if ((bool)opt17.IsChecked)
                checkedval4 = 2;
            else if ((bool)opt18.IsChecked)
                checkedval4 = 3;
            else if ((bool)opt19.IsChecked)
                checkedval4 = 4;
            else
                checkedval4 = 5;
            if ((bool)opt21.IsChecked)
                checkedval5 = 1;
            else if ((bool)opt22.IsChecked)
                checkedval5 = 2;
            else if ((bool)opt23.IsChecked)
                checkedval5 = 3;
            else if ((bool)opt24.IsChecked)
                checkedval5 = 4;
            else
                checkedval5 = 5;
            SurveyAnswers surveyA = new SurveyAnswers(0, sq.Id, app.LoggedInUser.Id, checkedval1, checkedval2, checkedval3, checkedval4, checkedval5);
            sa = app.SurveyAnswersController.Create(surveyA);
            this.Close();
        }
    }
}
