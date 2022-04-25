using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using zdravstvena_ustanova.Service;

namespace zdravstvena_ustanova.Controller
{
    public class AnamnesisController
    {
        private readonly AnamnesisService _anamnesisService;

        public AnamnesisController(AnamnesisService anamnesisService)
        {
            _anamnesisService = anamnesisService;
        }

        public IEnumerable<Anamnesis> GetAll()
        {
            return _anamnesisService.GetAll();
        }
       public Anamnesis GetById(long id)
        {
            return _anamnesisService.GetById(id);
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
