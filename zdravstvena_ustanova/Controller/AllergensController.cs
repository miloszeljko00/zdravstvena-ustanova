using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using Model;

namespace Controller
{
    public class AllergensController
    {
        private readonly AllergensService _allergenService;

        public AllergensController(AllergensService allergensService)
        {
            _allergenService = allergensService;
        }

        public IEnumerable<Allergens> GetAll()
        {
            return _allergenService.GetAll();
        }
        public Allergens GetById(long id)
        {
            return _allergenService.GetById(id);
        }
        public Allergens Create(Allergens allergen)
        {
            return _allergenService.Create(allergen);
        }
        public bool Update(Allergens allergen)
        {
            return _allergenService.Update(allergen);
        }
        public bool Delete(long allergenId)
        {
            return _allergenService.Delete(allergenId);
        }
    }
}
