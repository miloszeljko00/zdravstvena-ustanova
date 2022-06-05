using System.Collections.Generic;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class AnamnesisService : IAnamnesisService
    {
        private readonly IAnamnesisRepository _anamnesisRepository;

        public AnamnesisService(IAnamnesisRepository anamnesisRepository)
        {
            _anamnesisRepository = anamnesisRepository;
        }

        public IEnumerable<Anamnesis> GetAll()
        {
            return _anamnesisRepository.GetAll();
        }

        public Anamnesis Get(long id)
        {
            return _anamnesisRepository.Get(id);
        }

        public Anamnesis Create(Anamnesis anamnesis)
        {
            return _anamnesisRepository.Create(anamnesis);
        }

        public bool Update(Anamnesis anamnesis)
        {
            return _anamnesisRepository.Update(anamnesis);
        }
        public bool Delete(long id)
        {
            return _anamnesisRepository.Delete(id);
        }
    }
}
