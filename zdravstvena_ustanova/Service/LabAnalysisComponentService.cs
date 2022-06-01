using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using zdravstvena_ustanova.Repository;
using System.Linq;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class LabAnalysisComponentService : ILabAnalysisComponentService
    {
        private readonly ILabAnalysisComponentRepository _labAnalysisComponentRepository;

        public LabAnalysisComponentService(ILabAnalysisComponentRepository labAnalysisComponentRepository)
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

        public LabAnalysisComponent Get(long id)
        {
            throw new NotImplementedException();
        }
    }
}