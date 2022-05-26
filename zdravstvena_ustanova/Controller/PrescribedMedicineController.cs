using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;
using System.Windows;

namespace zdravstvena_ustanova.Controller
{
    public class PrescribedMedicineController
    {
        private readonly PrescribedMedicineService _prescribedMedicineService;

        public PrescribedMedicineController(PrescribedMedicineService prescribedMedicineService)
        {
            _prescribedMedicineService = prescribedMedicineService;
        }

        public IEnumerable<PrescribedMedicine> GetAll()
        {
            return _prescribedMedicineService.GetAll();
        }

        public PrescribedMedicine GetById(long id)
        {
            return _prescribedMedicineService.GetById(id);
        }

        public PrescribedMedicine Create(PrescribedMedicine prescribedMedicine)
        {
            return _prescribedMedicineService.Create(prescribedMedicine);
        }
        public bool Update(PrescribedMedicine prescribedMedicine)
        {
            return _prescribedMedicineService.Update(prescribedMedicine);
        }
        public bool Delete(long prescribedMedicineId)
        {
            return _prescribedMedicineService.Delete(prescribedMedicineId);
        }

        public bool ValidateParametersFromForm(Medication medication, string timesPerDay, string onHours, DateTime? endDate, string description)
        {
            bool isFormValid = true;
           
            if(timesPerDay == null || timesPerDay=="" || onHours==null || onHours == "" || medication==null || endDate == null)
            {
                MessageBox.Show("Morate uneti sve obavezne podatke(medication,tbd,oh,endDate...)");
                isFormValid = false;
            } else
            {
                isFormValid = IsEndDateValid(isFormValid, endDate);
                AssignValueToDescriptionVariable(description);
            }           
            return isFormValid;
        }

        private void AssignValueToDescriptionVariable(string description)
        {
            if (description == null)
            {
                description = "";
            }
        }

        private bool IsEndDateValid(bool isFormValid, DateTime? endDate)
        {
            if(endDate<DateTime.Now)
            {
                isFormValid = false;
                MessageBox.Show("Izabrali ste termin u proslosti!");
            }
            return isFormValid;
        }
    }
}
