using Model;
using Model.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Exception;

namespace zdravstvena_ustanova.Repository
{
    public class PrescribedMedicineRepository
    {
        private const string NOT_FOUND_ERROR = "PRESCRIBED MEDICINE NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _prescribedMedicineMaxId;

        public PrescribedMedicineRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _prescribedMedicineMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<PrescribedMedicine> prescribedMedicines)
        {
            return prescribedMedicines.Count() == 0 ? 0 : prescribedMedicines.Max(prescribedMedicine => prescribedMedicine.Id);
        }

        public IEnumerable<PrescribedMedicine> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToPrescribedMedicine)
                .ToList();
        }

        public PrescribedMedicine Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(prescribedMedicine => prescribedMedicine.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public PrescribedMedicine Create(PrescribedMedicine prescribedMedicine)
        {
            prescribedMedicine.Id = ++_prescribedMedicineMaxId;
            AppendLineToFile(_path, PrescribedMedicineToCSVFormat(prescribedMedicine));
            return prescribedMedicine;
        }

        public bool Update(PrescribedMedicine prescribedMedicine)
        {
            var prescribedMedicines = GetAll();

            foreach (PrescribedMedicine pm in prescribedMedicines)
            {
                if (pm.Id == prescribedMedicine.Id)
                {
                    pm.TimesPerDay = prescribedMedicine.TimesPerDay;
                    pm.OnHours = prescribedMedicine.OnHours;
                    pm.EndDate = prescribedMedicine.EndDate;
                    pm.Description = prescribedMedicine.Description;
                    pm.Medication = prescribedMedicine.Medication;
                    WriteLinesToFile(_path, PrescribedMedicinesToCSVFormat((List<PrescribedMedicine>)prescribedMedicines));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long prescribedMedicineId)
        {
            var prescribedMedicines = (List<PrescribedMedicine>)GetAll();

            foreach (PrescribedMedicine pm in prescribedMedicines)
            {
                if (pm.Id == prescribedMedicineId)
                {
                    prescribedMedicines.Remove(pm);
                    WriteLinesToFile(_path, PrescribedMedicinesToCSVFormat((List<PrescribedMedicine>)prescribedMedicines));
                    return true;
                }
            }
            return false;
        }
        private void WriteLinesToFile(string path, List<string> lines)
        {
            File.WriteAllLines(path, lines);
        }

        private void AppendLineToFile(string path, string line)
        {
            File.AppendAllText(path, line + Environment.NewLine);
        }

        private string PrescribedMedicineToCSVFormat(PrescribedMedicine prescribedMedicine)
        {
            return string.Join(_delimiter,
                prescribedMedicine.Id,
                prescribedMedicine.TimesPerDay.ToString(),
                prescribedMedicine.OnHours.ToString(),
                prescribedMedicine.EndDate.ToString("dd.MM.yyyy. HH:mm"),
                prescribedMedicine.Description,
                prescribedMedicine.Medication.Id
                );
        }
        private List<string> PrescribedMedicinesToCSVFormat(List<PrescribedMedicine> prescribedMedicines)
        {
            List<string> lines = new List<string>();

            foreach (PrescribedMedicine prescribedMedicine in prescribedMedicines)
            {
                lines.Add(PrescribedMedicineToCSVFormat(prescribedMedicine));
            }
            return lines;
        }

        private PrescribedMedicine CSVFormatToPrescribedMedicine(string prescribedMedicineCSVFormat)
        {
            var tokens = prescribedMedicineCSVFormat.Split(_delimiter.ToCharArray());
            var timeFormat = "dd.MM.yyyy. HH:mm";
            DateTime endDate;
            DateTime.TryParseExact(tokens[3], timeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
            return new PrescribedMedicine(
                long.Parse(tokens[0]),
                int.Parse(tokens[1]),
                int.Parse(tokens[2]),
                endDate,
                tokens[4],
                long.Parse(tokens[5])
                );
        }

    }
}
