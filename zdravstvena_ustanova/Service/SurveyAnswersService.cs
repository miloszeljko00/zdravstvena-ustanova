using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using zdravstvena_ustanova.Repository;
using System.Linq;
using zdravstvena_ustanova.Repository.RepositoryInterface;

namespace zdravstvena_ustanova.Service
{
    public class SurveyAnswersService
    {
        private readonly ISurveyAnswersRepository _surveyAnswersRepository;
        private IPatientRepository _patientRepository;
        private ISurveyQuestionsRepository _surveyQuestionsRepository;

        public SurveyAnswersService(ISurveyAnswersRepository surveyAnswersRepository, IPatientRepository patientRepository,
            ISurveyQuestionsRepository surveyQuestionsRepository)
        {
            _surveyAnswersRepository = surveyAnswersRepository;
            _patientRepository = patientRepository;
            _surveyQuestionsRepository = surveyQuestionsRepository;
        }

        public IEnumerable<SurveyAnswers> GetAll()
        {
            var patients = _patientRepository.GetAll();
            var surveyQuestions = _surveyQuestionsRepository.GetAll();
            var surveysAnswers = _surveyAnswersRepository.GetAll();
            BindPatientAndQuestionsWithSurveysAnswers(patients, surveyQuestions, surveysAnswers);
            return surveysAnswers;
        }

        public SurveyAnswers GetById(long Id)
        {
            var patients = _patientRepository.GetAll();
            var surveyQuestions = _surveyQuestionsRepository.GetAll();
            var surveyAnswers = _surveyAnswersRepository.Get(Id);
            BindPatientAndQuestionsWithSurveyAnswers(patients, surveyQuestions, surveyAnswers);
            return surveyAnswers;
        }
        private void BindPatientAndQuestionsWithSurveyAnswers(IEnumerable<Patient> patients, IEnumerable<SurveyQuestions> surveysQuestions,SurveyAnswers surveyAnswers)
        {
            surveyAnswers.Patient = FindPatientById(patients, surveyAnswers.Patient.Id);
            surveyAnswers.SurveyQuestions = FindSurveyById(surveysQuestions, surveyAnswers.SurveyQuestions.Id);

        }

        private void BindPatientAndQuestionsWithSurveysAnswers(IEnumerable<Patient> patients, IEnumerable<SurveyQuestions> surveysQuestions, IEnumerable<SurveyAnswers> surveysAnswers)
        {
            surveysAnswers.ToList().ForEach(surveyAnswers =>
            {
                BindPatientAndQuestionsWithSurveyAnswers(patients, surveysQuestions, surveyAnswers);
            });
        }

        private Patient FindPatientById(IEnumerable<Patient> patients, long patientId)
        {
            return patients.SingleOrDefault(patient => patient.Id == patientId);
        }
        private SurveyQuestions FindSurveyById(IEnumerable<SurveyQuestions> surveysQuestions, long surveyQuestionsId)
        {
            return surveysQuestions.SingleOrDefault(surveyQuestions => surveyQuestions.Id == surveyQuestionsId);
        }

        public SurveyAnswers Create(SurveyAnswers surveyAnswers)
        {
            return _surveyAnswersRepository.Create(surveyAnswers);
        }

        public IEnumerable<SurveyAnswers> GetBySurveyQuestionsName(string surveyName)
        {
            var surveyAnswers = GetAll();
            return surveyAnswers.Where(surveyAnswer => surveyAnswer.SurveyQuestions.Name == surveyName).ToList();
        }
    }
}