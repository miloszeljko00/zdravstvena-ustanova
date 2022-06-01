using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using zdravstvena_ustanova.Repository;
using System.Linq;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class VaccineService : IVaccineService
    {
        private readonly IVaccineRepository _vaccineRepository;

        public VaccineService(IVaccineRepository vaccineRepository)
        {
            _vaccineRepository = vaccineRepository;
        }

        public IEnumerable<Vaccine> GetAll()
        {
            return _vaccineRepository.GetAll();
        }

        public Vaccine FindVaccineById(IEnumerable<Vaccine> vaccines, long id)
        {
            return vaccines.SingleOrDefault(vaccine => vaccine.Id == id);
        }

        public Vaccine Create(Vaccine vaccine)
        {
            return _vaccineRepository.Create(vaccine);
        }
        public bool Update(Vaccine vaccine)
        {
            return _vaccineRepository.Update(vaccine);
        }
        public bool Delete(long vaccineId)
        {
            return _vaccineRepository.Delete(vaccineId);
        }

        public Vaccine Get(long id)
        {
            return _vaccineRepository.Get(id);
        }
    }
}