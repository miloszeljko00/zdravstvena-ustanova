using Model;
using System;
using System.Collections.Generic;
using Repository;
using System.Linq;

namespace Service
{
    public class DiseaseService
    {
        private readonly DiseaseRepository _diseaseRepository;

        public DiseaseService(DiseaseRepository diseaseRepository)
        {
            _diseaseRepository = diseaseRepository;
        }

        public IEnumerable<Disease> GetAll()
        {
            return _diseaseRepository.GetAll();
        }

        public Disease FindDiseaseById(IEnumerable<Disease> diseases, long id)
        {
            return diseases.SingleOrDefault(disease => disease.Id == id);
        }

        public Disease Create(Disease disease)
        {
            return _diseaseRepository.Create(disease);
        }
        public bool Update(Disease disease)
        {
            return _diseaseRepository.Update(disease);
        }
        public bool Delete(long diseaseId)
        {
            return _diseaseRepository.Delete(diseaseId);
        }
    }
}