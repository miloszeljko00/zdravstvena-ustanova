using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Controller
{
    public class DiseaseController
    {
        private readonly IDiseaseService _diseaseService;

        public DiseaseController(IDiseaseService diseaseService)
        {
            _diseaseService = diseaseService;
        }

        public IEnumerable<Disease> GetAll()
        {
            return _diseaseService.GetAll();
        }
        public Disease GetById(IEnumerable<Disease> diseases, long id)
        {
            return _diseaseService.FindDiseaseById(diseases, id);
        }
        public Disease Create(Disease disease)
        {
            return _diseaseService.Create(disease);
        }
        public bool Update(Disease disease)
        {
            return _diseaseService.Update(disease);
        }
        public bool Delete(long diseaseId)
        {
            return _diseaseService.Delete(diseaseId);
        }
    }
}
