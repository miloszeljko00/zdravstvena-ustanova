using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Repository;
using Model;

namespace zdravstvena_ustanova.Service
{
    public class AnamnesisService
    {
        private readonly AnamnesisRepository _anamnesisRepository;

        public AnamnesisService(AnamnesisRepository anamnesisRepository)
        {
            _anamnesisRepository = anamnesisRepository;
        }

        public IEnumerable<Anamnesis> GetAll()
        {
            return _anamnesisRepository.GetAll();
        }

        public Anamnesis GetById(long id)
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
