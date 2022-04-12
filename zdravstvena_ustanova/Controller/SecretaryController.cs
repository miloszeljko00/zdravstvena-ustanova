using System;
using Service;
using Model;
using System.Collections.Generic;

namespace Controller
{
   public class SecretaryController
   {
        private readonly SecretaryService _secretaryService;

        public SecretaryController(SecretaryService secretaryService)
        {
            _secretaryService = secretaryService;
        }

        public IEnumerable<Secretary> GetAll()
        {
            return _secretaryService.GetAll();
        }

        public Secretary Create(Secretary secretary)
        {
            return _secretaryService.Create(secretary);
        }
        public void Update(Secretary secretary)
        {
            _secretaryService.Update(secretary);
        }
        public void Delete(long secretayId)
        {
            _secretaryService.Delete(secretayId);
        }
    }
}