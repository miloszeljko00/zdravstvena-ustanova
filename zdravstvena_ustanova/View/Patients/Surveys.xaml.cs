using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.View
{
    public partial class Surveys : UserControl
    {
        public ObservableCollection<SurveyQuestions> SurveyQuestions { get; set; }
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
            SurveyQuestions = new ObservableCollection<SurveyQuestions>();
            var app = Application.Current as App;
            List<SurveyQuestions> surveyQuestions = new List<SurveyQuestions>(app.SurveyQuestionsController.GetAll());
            List<ScheduledAppointment> scheduledAppointments = new List<ScheduledAppointment>(app.ScheduledAppointmentController.GetScheduledAppointmentsForPatient(app.LoggedInUser.Id));
            List<SurveyAnswers> surveyAnswers = new List<SurveyAnswers>(app.SurveyAnswersController.GetAnswersByPatient(app.LoggedInUser.Id));
            foreach (SurveyQuestions sq in surveyQuestions)
            {
                if (sq.ScheduledAppointment == null)
                {
                    SurveyQuestions.Add(sq);
                }
                else
                {
                    foreach (ScheduledAppointment sa in scheduledAppointments)
                    {
                        if (sq.ScheduledAppointment.Id == sa.Id)
                        {
                            SurveyQuestions.Add(sq);
                        }
                    }
                }
                foreach (SurveyAnswers answer in surveyAnswers)
                {
                    if (sq.Id == answer.SurveyQuestions.Id)
                    {
                        SurveyQuestions.Remove(sq);
                    }
                }
            }
            surveyList.ItemsSource = SurveyQuestions;
        }
    }
}
