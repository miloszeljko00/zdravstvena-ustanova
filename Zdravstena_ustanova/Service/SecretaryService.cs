using Model;
using System;
using System.Collections.Generic;
using Repository;
using System.Linq;

namespace Service
{
    public class SecretaryService
    {
        private readonly SecretaryRepository _secretaryRepository;

        public SecretaryService(SecretaryRepository secretaryRepository)
        {
            _secretaryRepository = secretaryRepository;
        }

        internal IEnumerable<Secretary> GetAll()
        {
            var secretaries = _secretaryRepository.GetAll();
            return secretaries;
        }

        private Secretary FindSecretaryById(IEnumerable<Secretary> secretaries, long secretaryId)
        {
            return secretaries.SingleOrDefault(secretary => secretary.Id == secretaryId);
        }

        public Secretary Create(Secretary secretary)
        {
            return _secretaryRepository.Create(secretary);
        }
        public void Update(Secretary secretary)
        {
            _secretaryRepository.Update(secretary);
        }
        public void Delete(long secretaryId)
        {
            _secretaryRepository.Delete(secretaryId);
        }
    }
}