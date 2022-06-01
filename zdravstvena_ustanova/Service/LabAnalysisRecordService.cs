using zdravstvena_ustanova.Model;
using System;
using zdravstvena_ustanova.Repository;
using System.Collections.Generic;
using System.Linq;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class LabAnalysisRecordService : ILabAnalysisRecordService
    {
        private readonly ILabAnalysisRecordRepository _labAnalysisRecordRepository;
        private readonly ILabAnalysisComponentRepository _labAnalysisComponentRepository;

        public LabAnalysisRecordService(ILabAnalysisRecordRepository labAnalysisRecordRepository,
            ILabAnalysisComponentRepository labAnalysisComponentRepository)
        {
            _labAnalysisRecordRepository = labAnalysisRecordRepository;
            _labAnalysisComponentRepository = labAnalysisComponentRepository;
        }

        public IEnumerable<LabAnalysisRecord> GetAll()
        {
            var labAnalysisRecords = _labAnalysisRecordRepository.GetAll();
            var labAnalysisComponents = _labAnalysisComponentRepository.GetAll();
            BindLabAnalysisRecordsWithLabAnalysisComponents(labAnalysisRecords, labAnalysisComponents);
            return labAnalysisRecords;
        }

        private void BindLabAnalysisRecordsWithLabAnalysisComponents(IEnumerable<LabAnalysisRecord> labAnalysisRecords, IEnumerable<LabAnalysisComponent> labAnalysisComponents)
        {
            foreach (LabAnalysisRecord lar in labAnalysisRecords)
            {
                BindLabAnalysisRecordWithLabAnalysisComponents(lar, labAnalysisComponents);
            }
        }

        public LabAnalysisRecord Get(long id)
        {
            var labAnalysisRecord = _labAnalysisRecordRepository.Get(id);
            var labAnalysisComponents = _labAnalysisComponentRepository.GetAll();
            BindLabAnalysisRecordWithLabAnalysisComponents(labAnalysisRecord, labAnalysisComponents);
            return labAnalysisRecord;
        }

        private void BindLabAnalysisRecordWithLabAnalysisComponents(LabAnalysisRecord labAnalysisRecord, IEnumerable<LabAnalysisComponent> labAnalysisComponents)
        {
            List<LabAnalysisComponent> labAnalysisComponentsBinded = new List<LabAnalysisComponent>();
            foreach (LabAnalysisComponent lac1 in labAnalysisRecord.LabAnalysisComponent)
            {
                foreach (LabAnalysisComponent lac2 in labAnalysisComponents)
                {
                    if (lac2.Id == lac1.Id)
                    {
                        labAnalysisComponentsBinded.Add(lac2);
                        break;
                    }
                }
            }
            labAnalysisRecord.LabAnalysisComponent = labAnalysisComponentsBinded;
        }

        public LabAnalysisRecord Create(LabAnalysisRecord labAnalysisRecord)
        {
            return _labAnalysisRecordRepository.Create(labAnalysisRecord);
        }
        public bool Update(LabAnalysisRecord labAnalysisRecord)
        {
            return _labAnalysisRecordRepository.Update(labAnalysisRecord);
        }
        public bool Delete(long labAnalysisRecordId)
        {
            return _labAnalysisRecordRepository.Delete(labAnalysisRecordId);
        }

    }
}