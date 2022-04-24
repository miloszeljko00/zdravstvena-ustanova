using Model;
using System;
using System.Collections.Generic;
using Repository;
using System.Linq;
using zdravstvena_ustanova.Repository;
using zdravstvena_ustanova.Model;

namespace Service
{
    public class VaccineService
    {
        private readonly VaccineRepository _vaccineRepository;

        public VaccineService(VaccineRepository vaccineRepository)
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
    }
}