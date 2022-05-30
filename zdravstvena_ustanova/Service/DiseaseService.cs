using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using zdravstvena_ustanova.Repository;
using System.Linq;
using zdravstvena_ustanova.Repository.RepositoryInterface;

namespace zdravstvena_ustanova.Service
{
    public class DiseaseService
    {
        private readonly IDiseaseRepository _diseaseRepository;

        public DiseaseService(IDiseaseRepository diseaseRepository)
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