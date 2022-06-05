using System.Collections.Generic;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class RenovationTypeService : IRenovationTypeService
    {
        private readonly IRenovationTypeRepository _renovationTypeRepository;

        public RenovationTypeService(IRenovationTypeRepository renovationTypeRepository)
        {
            _renovationTypeRepository = renovationTypeRepository;
        }

        public IEnumerable<RenovationType> GetAll()
        {
            return _renovationTypeRepository.GetAll();
        }

        public RenovationType Get(long id)
        {
            return _renovationTypeRepository.Get(id);
        }

        public RenovationType Create(RenovationType renovationType)
        {
            return _renovationTypeRepository.Create(renovationType);
        }
        public bool Update(RenovationType renovationType)
        {
            return _renovationTypeRepository.Update(renovationType);
        }
        public bool Delete(long renovationTypeId)
        {
            return _renovationTypeRepository.Delete(renovationTypeId);
        }
    }
}
