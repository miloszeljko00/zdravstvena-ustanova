using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using zdravstvena_ustanova.Repository;
using System.Linq;
using zdravstvena_ustanova.Model.Enums;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class SurveyQuestionsService : ISurveyQuestionsService
    {
        private readonly ISurveyQuestionsRepository _surveyQuestionsRepository;
        private readonly IScheduledAppointmentRepository _scheduledAppointmentRepository;

        public SurveyQuestionsService(ISurveyQuestionsRepository surveyQuestionsRepository,
            IScheduledAppointmentRepository scheduledAppointmentRepository)
        {
            _surveyQuestionsRepository = surveyQuestionsRepository;
            _scheduledAppointmentRepository = scheduledAppointmentRepository;
        }

        public IEnumerable<SurveyQuestions> GetAll()
        {
            var scheduledAppointments = _scheduledAppointmentRepository.GetAll();
            var surveysQuestions = _surveyQuestionsRepository.GetAll();
            BindScheduledAppointmentsWithSurveysQuestions(scheduledAppointments, surveysQuestions);
            return surveysQuestions;
        }

        public SurveyQuestions Get(long Id)
        {
            var scheduledAppointments = _scheduledAppointmentRepository.GetAll();
            var surveyQuestions = _surveyQuestionsRepository.Get(Id);
            BindScheduledAppointmentsWithSurveyQuestions(scheduledAppointments, surveyQuestions);
            return surveyQuestions;
        }
        
        private void BindScheduledAppointmentsWithSurveyQuestions(IEnumerable<ScheduledAppointment> scheduledAppointments, SurveyQuestions surveyQuestions)
        {
            if (surveyQuestions.ScheduledAppointment.Id == -1)
            {
                surveyQuestions.ScheduledAppointment = null;
            }
            else
            {
                surveyQuestions.ScheduledAppointment = FindScheduledAppointmentById(scheduledAppointments, surveyQuestions.ScheduledAppointment.Id);
                surveyQuestions.DateOfPublication = surveyQuestions.ScheduledAppointment.End;
            }
        }

        private void BindScheduledAppointmentsWithSurveysQuestions(IEnumerable<ScheduledAppointment> scheduledAppointments, IEnumerable<SurveyQuestions> surveysQuestions)
        {
            surveysQuestions.ToList().ForEach(surveyQuestions =>
            {
                BindScheduledAppointmentsWithSurveyQuestions(scheduledAppointments, surveyQuestions);
            });
        }

        private ScheduledAppointment FindScheduledAppointmentById(IEnumerable<ScheduledAppointment> scheduledAppointments, long scheduledAppointmentId)
        {
            return scheduledAppointments.SingleOrDefault(scheduledAppointment => scheduledAppointment.Id == scheduledAppointmentId);
        }

        public IEnumerable<SurveyQuestions> GetAllUnique()
        {
            var surveyQuestions = GetAll();

            var surveys = new List<SurveyQuestions>();

            foreach (var surveyQuestion in surveyQuestions)
            {
                bool isAdded = false;
                foreach (var survey in surveys)
                {
                    if (survey.Name == surveyQuestion.Name)
                    {
                        isAdded = true;
                        break;
                    }
                }

                if (!isAdded)
                {
                    surveys.Add(surveyQuestion);
                }
            }

            return surveys;
        }

        public SurveyQuestions Create(SurveyQuestions t)
        {
            throw new NotImplementedException();
        }

        public bool Update(SurveyQuestions t)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}