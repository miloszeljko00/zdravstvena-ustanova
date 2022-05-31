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

namespace zdravstvena_ustanova.View
{
    /// <summary>
    /// Interaction logic for Surveys.xaml
    /// </summary>
    public partial class Surveys : UserControl
    {
        public ObservableCollection<SurveyQuestions> sq { get; set; }
        public Surveys()
        {
            InitializeComponent();
            this.refreshSurveyList();
        }

        private void entered(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && surveyList.SelectedIndex != -1)
            {
                SurveyDetails sd = new SurveyDetails((SurveyQuestions)surveyList.SelectedItem);
                sd.ShowDialog();
                this.refreshSurveyList();
            }
        }

        public void refreshSurveyList()
        {
            sq = new ObservableCollection<SurveyQuestions>();
            var app = Application.Current as App;
            List<SurveyQuestions> surQ = new List<SurveyQuestions>(app.SurveyQuestionsController.GetAll());
            List<ScheduledAppointment> sa = new List<ScheduledAppointment>(app.ScheduledAppointmentController.GetAll());
            List<SurveyAnswers> surA = new List<SurveyAnswers>(app.SurveyAnswersController.GetAll());
            foreach (SurveyQuestions survey in surQ)
            {
                if (survey.ScheduledAppointment == null)
                {
                    sq.Add(survey);
                }
                else
                {
                    foreach (ScheduledAppointment scApp in sa)
                    {
                        if (app.LoggedInUser.Id == scApp.Patient.Id && survey.ScheduledAppointment.Id == scApp.Id)
                        {
                            sq.Add(survey);
                        }
                    }
                }
                foreach (SurveyAnswers answer in surA)
                {
                    if (survey.Id == answer.SurveyQuestions.Id && app.LoggedInUser.Id == answer.Patient.Id)
                    {
                        sq.Remove(survey);
                    }
                }
            }
            surveyList.ItemsSource = sq;
        }
    }
}
