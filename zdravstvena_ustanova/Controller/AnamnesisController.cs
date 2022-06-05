using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Controller
{
    public class AnamnesisController
    {
        private readonly IAnamnesisService _anamnesisService;

        public AnamnesisController(IAnamnesisService anamnesisService)
        {
            _anamnesisService = anamnesisService;
        }

        public IEnumerable<Anamnesis> GetAll()
        {
            return _anamnesisService.GetAll();
        }
       public Anamnesis GetById(long id)
        {
            return _anamnesisService.Get(id);
        }
        public Anamnesis Create(Anamnesis anamnesis)
        {
            return _anamnesisService.Create(anamnesis);
        }
        public bool Update(Anamnesis anamnesis)
        {
            return _anamnesisService.Update(anamnesis);
        }
        public bool Delete(long id)
        {
            return _anamnesisService.Delete(id);
        }
    }
}
