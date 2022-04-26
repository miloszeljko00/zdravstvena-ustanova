using Model;
using System;
using Repository;
using System.Collections.Generic;
using System.Linq;
using zdravstvena_ustanova.Repository;
using zdravstvena_ustanova.Model;

namespace Service
{
    public class LabAnalysisRequestService
    {
        private readonly LabAnalysisRequestRepository _labAnalysisRequestRepository;
        private readonly LabAnalysisComponentRepository _labAnalysisComponentRepository;

        public LabAnalysisRequestService(LabAnalysisRequestRepository labAnalysisRequestRepository, LabAnalysisComponentRepository labAnalysisComponentRepository)
        {
            _labAnalysisRequestRepository = labAnalysisRequestRepository;
            _labAnalysisComponentRepository = labAnalysisComponentRepository;
        }

        public IEnumerable<LabAnalysisRequest> GetAll()
        {
            var labAnalysisRequests = _labAnalysisRequestRepository.GetAll();
            var labAnalysisComponents = _labAnalysisComponentRepository.GetAll();
            BindLabAnalysisRequestsWithLabAnalysisComponents(labAnalysisRequests, labAnalysisComponents);
            return labAnalysisRequests;
        }

        private void BindLabAnalysisRequestsWithLabAnalysisComponents(IEnumerable<LabAnalysisRequest> labAnalysisRequests, IEnumerable<LabAnalysisComponent> labAnalysisComponents)
        {
            foreach (LabAnalysisRequest lar in labAnalysisRequests)
            {
                BindLabAnalysisRequestWithLabAnalysisComponents(lar, labAnalysisComponents);
            }
        }

        public LabAnalysisRequest GetById(long id)
        {
            var labAnalysisRequest = _labAnalysisRequestRepository.Get(id);
            var labAnalysisComponents = _labAnalysisComponentRepository.GetAll();
            BindLabAnalysisRequestWithLabAnalysisComponents(labAnalysisRequest, labAnalysisComponents);
            return labAnalysisRequest;
        }

        private void BindLabAnalysisRequestWithLabAnalysisComponents(LabAnalysisRequest labAnalysisRequest, IEnumerable<LabAnalysisComponent> labAnalysisComponents)
        {
            List<LabAnalysisComponent> labAnalysisComponentsBinded = new List<LabAnalysisComponent>();
            foreach (LabAnalysisComponent lac1 in labAnalysisRequest.LabAnalysisComponent)
            {
                foreach (LabAnalysisComponent lac2 in labAnalysisComponents)
                {
                    if (lac2.Id == lac1.Id)
                    {
                        labAnalysisComponentsBinded.Add(lac2);
                        break;
                    }
                }
            }
            labAnalysisRequest.LabAnalysisComponent = labAnalysisComponentsBinded;
        }

        public LabAnalysisRequest Create(LabAnalysisRequest labAnalysisRequest)
        {
            return _labAnalysisRequestRepository.Create(labAnalysisRequest);
        }
        public bool Update(LabAnalysisRequest labAnalysisRequest)
        {
            return _labAnalysisRequestRepository.Update(labAnalysisRequest);
        }
        public bool Delete(long labAnalysisRequestId)
        {
            return _labAnalysisRequestRepository.Delete(labAnalysisRequestId);
        }

    }
}