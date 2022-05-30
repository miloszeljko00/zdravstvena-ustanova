using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Repository;

namespace zdravstvena_ustanova.Service
{
    public class RenovationTypeService
    {
        private readonly RenovationTypeRepository _renovationTypeRepository;

        public RenovationTypeService(RenovationTypeRepository renovationTypeRepository)
        {
            _renovationTypeRepository = renovationTypeRepository;
        }

        public IEnumerable<RenovationType> GetAll()
        {
            return _renovationTypeRepository.GetAll();
        }

        public RenovationType GetById(long id)
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
