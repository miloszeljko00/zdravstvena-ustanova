using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;

namespace zdravstvena_ustanova.Controller
{
    public class MedicalExaminationController
    {
        private readonly MedicalExaminationService _medicalExaminationService;

        public MedicalExaminationController(MedicalExaminationService medicalExaminationService)
        {
            _medicalExaminationService = medicalExaminationService;
        }

        public IEnumerable<MedicalExamination> GetAll()
        {
            return _medicalExaminationService.GetAll();
        }
        public MedicalExamination GetById(long id)
        {
            return _medicalExaminationService.GetById(id);
        }
        public MedicalExamination Create(MedicalExamination medicalExamination)
        {
            return _medicalExaminationService.Create(medicalExamination);
        }
        public bool Update(MedicalExamination medicalExamination)
        {
            return _medicalExaminationService.Update(medicalExamination);
        }
        public bool Delete(long id)
        {
            return _medicalExaminationService.Delete(id);
        }
        public MedicalExamination FindByScheduledAppointmentId(long id)
        {
            return _medicalExaminationService.FindByScheduledAppointmentId(id);
        }
    }
}
