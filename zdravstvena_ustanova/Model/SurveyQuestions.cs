using System;
using zdravstvena_ustanova.Model.Enums;

namespace zdravstvena_ustanova.Model
{
    public class SurveyQuestions
    {
        public long Id { get; set; }
        public ScheduledAppointment ScheduledAppointment { get; set; }
        public SurveyType SurveyType { get; set; }
        public DateTime DateOfPublication { get; set; }
        public string Name { get; set; }
        public string QuestionOne { get; set; }
        public string QuestionTwo { get; set; }
        public string QuestionThree { get; set; }
        public string QuestionFour { get; set; }
        public string QuestionFive { get; set; }

        public SurveyQuestions(long id, ScheduledAppointment scheduledAppointment, SurveyType surveyType, DateTime dateOfPublication, string name, string questionOne, string questionTwo, string questionThree, string questionFour, string questionFive)
        {
            Id = id;
            ScheduledAppointment = scheduledAppointment;
            SurveyType = surveyType;
            DateOfPublication = dateOfPublication;
            Name = name;
            QuestionOne = questionOne;
            QuestionTwo = questionTwo;
            QuestionThree = questionThree;
            QuestionFour = questionFour;
            QuestionFive = questionFive;
        }
        public SurveyQuestions(long id, long scheduledAppointmentId, SurveyType surveyType, DateTime dateOfPublication, string name, string questionOne, string questionTwo, string questionThree, string questionFour, string questionFive)
        {
            Id = id;
            ScheduledAppointment = new ScheduledAppointment(scheduledAppointmentId);
            SurveyType = surveyType;
            DateOfPublication = dateOfPublication;
            Name = name;
            QuestionOne = questionOne;
            QuestionTwo = questionTwo;
            QuestionThree = questionThree;
            QuestionFour = questionFour;
            QuestionFive = questionFive;
        }
        public SurveyQuestions(long id, long scheduledAppointmentId, SurveyType surveyType, string name, string questionOne, string questionTwo, string questionThree, string questionFour, string questionFive)
        {
            Id = id;
            ScheduledAppointment = new ScheduledAppointment(scheduledAppointmentId);
            SurveyType = surveyType;
            Name = name;
            QuestionOne = questionOne;
            QuestionTwo = questionTwo;
            QuestionThree = questionThree;
            QuestionFour = questionFour;
            QuestionFive = questionFive;
        }
        public SurveyQuestions(long id){
            Id = id;
        }

    }
}
