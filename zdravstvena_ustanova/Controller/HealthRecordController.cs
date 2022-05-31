﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Controller
{
    public class HealthRecordController
    {
        private readonly HealthRecordService _healthRecordService;

        public HealthRecordController(HealthRecordService healthRecordService)
        {
            _healthRecordService = healthRecordService;
        }
        public IEnumerable<HealthRecord> GetAll()
        {
            return _healthRecordService.GetAll();
        }
        public IEnumerable<Anamnesis> GetAnamnesisForPatient(long patientId)
        {
            return _healthRecordService.GetAnamnesisForPatient(patientId);
        }
        public HealthRecord GetById(long id)
        {
            return _healthRecordService.GetById(id);
        }
        public HealthRecord Create(HealthRecord healthRecord)
        {
            return _healthRecordService.Create(healthRecord);
        }

        public bool Update(HealthRecord healthRecord)
        {
            return _healthRecordService.Update(healthRecord);
        }

        public bool Delete(long id)
        {
            return _healthRecordService.Delete(id);
        }

        public HealthRecord FindHealthRecordByPatient(long patientId)
        {
            return _healthRecordService.FindHealthRecordByPatient(patientId);
        }
    }
}
