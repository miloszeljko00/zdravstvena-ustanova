﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
using Model;

namespace zdravstvena_ustanova.Controller
{
    public class PatientVaccinationController
    {
        private readonly PatientVaccinationService _patientVaccinationService;

        public PatientVaccinationController(PatientVaccinationService patientVaccinationService)
        {
            _patientVaccinationService = patientVaccinationService;
        }

        public IEnumerable<PatientVaccination> GetAll()
        {
            return _patientVaccinationService.GetAll();
        }

        public PatientVaccination GetById(long id)
        {
            return _patientVaccinationService.GetById(id);
        }

        public PatientVaccination Create(PatientVaccination patientVaccination)
        {
            return _patientVaccinationService.Create(patientVaccination);
        }
        public bool Update(PatientVaccination patientVaccination)
        {
            return _patientVaccinationService.Update(patientVaccination);
        }
        public bool Delete(long prescribedMedicineId)
        {
            return _patientVaccinationService.Delete(prescribedMedicineId);
        }
    }
}
