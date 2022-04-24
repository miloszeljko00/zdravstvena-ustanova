using Model;
using System;
using System.Collections.Generic;
using Repository;
using System.Linq;
using zdravstvena_ustanova.Model;

namespace Service
{
    public class LabAnalysisComponentService
    {
        private readonly LabAnalysisComponentRepository _labAnalysisComponentRepository;

        public LabAnalysisComponentService(LabAnalysisComponentRepository labAnalysisComponentRepository)
        {
            _labAnalysisComponentRepository = labAnalysisComponentRepository;
        }

        public IEnumerable<LabAnalysisComponent> GetAll()
        {
            return _labAnalysisComponentRepository.GetAll();
        }

        public LabAnalysisComponent FindLabAnalysisComponentById(IEnumerable<LabAnalysisComponent> components, long id)
        {
            return components.SingleOrDefault(component => component.Id == id);
        }

        public LabAnalysisComponent Create(LabAnalysisComponent labAnalysisComponent)
        {
            return _labAnalysisComponentRepository.Create(labAnalysisComponent);
        }
        public bool Update(LabAnalysisComponent labAnalysisComponent)
        {
            return _labAnalysisComponentRepository.Update(labAnalysisComponent);
        }
        public bool Delete(long labAnalysisComponentId)
        {
            return _labAnalysisComponentRepository.Delete(labAnalysisComponentId);
        }
    }
}