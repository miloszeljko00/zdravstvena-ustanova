using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class SpecialistRequestService : ISpecialistRequestService
    {
        private readonly ISpecialistRequestRepository _specialistRequestRepository;
        private readonly ISpecialtyRepository _specialtyRepository;

        public SpecialistRequestService(ISpecialistRequestRepository specialistRequestRepository, ISpecialtyRepository specialtyRepository)
        {
            _specialistRequestRepository = specialistRequestRepository;
            _specialtyRepository = specialtyRepository;
        }

        public IEnumerable<SpecialistRequest> GetAll()
        {
            var specialistRequests = _specialistRequestRepository.GetAll();
            var specialties = _specialtyRepository.GetAll();
            BindSpecialistRequestsWithSpecialties(specialistRequests, specialties);
            return specialistRequests;
        }

        private void BindSpecialistRequestsWithSpecialties(IEnumerable<SpecialistRequest> specialistRequests, IEnumerable<Specialty> specialties)
        {
            foreach (SpecialistRequest sr in specialistRequests)
            {
                BindSpecialistRequestWithSpecialty(sr,specialties);
            }
        }

        public SpecialistRequest Get(long id)
        {
            var specialistRequest = _specialistRequestRepository.Get(id);
            var specialties = _specialtyRepository.GetAll();
            BindSpecialistRequestWithSpecialty(specialistRequest, specialties);
            return specialistRequest;
        }

        private void BindSpecialistRequestWithSpecialty(SpecialistRequest specialistRequest, IEnumerable<Specialty> specialties)
        {
            foreach(Specialty s in specialties)
            {
                if(specialistRequest.Specialty.Id==s.Id)
                {
                    specialistRequest.Specialty = s;
                    break;
                }
            }
        }

        public SpecialistRequest Create(SpecialistRequest specialistRequest)
        {
            return _specialistRequestRepository.Create(specialistRequest);
        }
        public bool Update(SpecialistRequest specialistRequest)
        {
            return _specialistRequestRepository.Update(specialistRequest);
        }
        public bool Delete(long specialistRequestId)
        {
            return _specialistRequestRepository.Delete(specialistRequestId);
        }
    }
}
