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
    public class LabAnalysisComponentController
    {
        private readonly ILabAnalysisComponentService _labAnalysisComponentService;

        public LabAnalysisComponentController(ILabAnalysisComponentService labAnalysisComponentService)
        {
            _labAnalysisComponentService = labAnalysisComponentService;
        }

        public IEnumerable<LabAnalysisComponent> GetAll()
        {
            return _labAnalysisComponentService.GetAll();
        }
        public LabAnalysisComponent GetById(IEnumerable<LabAnalysisComponent> labAnalysisComponents, long id)
        {
            return _labAnalysisComponentService.FindLabAnalysisComponentById(labAnalysisComponents, id);
        }
        public LabAnalysisComponent Create(LabAnalysisComponent labAnalysisComponent)
        {
            return _labAnalysisComponentService.Create(labAnalysisComponent);
        }
        public bool Update(LabAnalysisComponent labAnalysisComponent)
        {
            return _labAnalysisComponentService.Update(labAnalysisComponent);
        }
        public bool Delete(long labAnalysisComponentId)
        {
            return _labAnalysisComponentService.Delete(labAnalysisComponentId);
        }
    }
}
