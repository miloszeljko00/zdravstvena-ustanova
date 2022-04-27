using Model;
using Model.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Exception;

namespace zdravstvena_ustanova.Repository
{
    public class MedicalExaminationRepository
    {
        private const string NOT_FOUND_ERROR = "MEDICAL EXAMINATION NOT FOUND: {0}={1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _medicalExaminationMaxId;

        public MedicalExaminationRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _medicalExaminationMaxId = GetMaxId(GetAll());
        }

        private long GetMaxId(IEnumerable<MedicalExamination> medicalExaminations)
        {
            return medicalExaminations.Count() == 0 ? 0 : medicalExaminations.Max(medicalExamination => medicalExamination.Id);
        }

        public IEnumerable<MedicalExamination> GetAll()
        {
            return File.ReadAllLines(_path).Select(CSVFormatToMedicalExamination).ToList();
        }

        public MedicalExamination Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(MedicalExamination => MedicalExamination.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public MedicalExamination Create(MedicalExamination medicalExamination)
        {
            medicalExamination.Id = ++_medicalExaminationMaxId;
            AppendLineToFile(_path, MedicalExaminationToCSVFormat(medicalExamination));
            return medicalExamination;
        }

        public bool Update(MedicalExamination medicalExamination)
        {
            var medicalExaminations = GetAll();

            foreach (MedicalExamination me in medicalExaminations)
            {
                if (me.Id == medicalExamination.Id)
                {
                    me.ScheduledAppointment = medicalExamination.ScheduledAppointment;
                    me.Anamnesis = medicalExamination.Anamnesis;
                    me.SpecialistRequest = medicalExamination.SpecialistRequest;
                    me.LabAnalysisRequest = medicalExamination.LabAnalysisRequest;
                    me.HospitalizationRequest = medicalExamination.HospitalizationRequest;
                    me.PrescribedMedicine = me.PrescribedMedicine;
                    WriteLinesToFile(_path, MedicalExaminationsToCSVFormat((List<MedicalExamination>)medicalExaminations));
                    return true;
                }
            }
            return false;
        }

        public bool Delete(long medicalExaminationId)
        {
            var medicalExaminations = (List<MedicalExamination>)GetAll();
            foreach (MedicalExamination me in medicalExaminations)
            {
                if (me.Id == medicalExaminationId)
                {
                    medicalExaminations.Remove(me);
                    WriteLinesToFile(_path, MedicalExaminationsToCSVFormat((List<MedicalExamination>)medicalExaminations));
                    return true;
                }
            }
            return false;
        }

        private string MedicalExaminationToCSVFormat(MedicalExamination medicalExamination)
        {
            int count = medicalExamination.PrescribedMedicine.Count;
            string prescribedMedicines = "";
            for (int i = 0; i < medicalExamination.PrescribedMedicine.Count; i++)
            {
                prescribedMedicines = string.Join(_delimiter, prescribedMedicines, medicalExamination.PrescribedMedicine[i].Id);
            }

            return string.Join(_delimiter,
                medicalExamination.Id,
                medicalExamination.ScheduledAppointment.Id,
                medicalExamination.Anamnesis.Id,
                medicalExamination.SpecialistRequest.Id,
                medicalExamination.LabAnalysisRequest.Id,
                medicalExamination.HospitalizationRequest.Id,
                count,
                prescribedMedicines
                );
        }

        private void AppendLineToFile(string path, string line)
        {
            File.AppendAllText(path, line + Environment.NewLine);
        }

        private void WriteLinesToFile(string path, List<string> lines)
        {
            File.WriteAllLines(path, lines);
        }

        private MedicalExamination CSVFormatToMedicalExamination(string medicalExaminationCSVFormat)
        {
            var tokens = medicalExaminationCSVFormat.Split(_delimiter.ToCharArray());

            List<PrescribedMedicine> prescribedMedicines = new List<PrescribedMedicine>();
            for (int i = 6 + 1; i < 6 + 1 + int.Parse(tokens[6]); i++)
            {
                var prescribedMedicine = new PrescribedMedicine(int.Parse(tokens[i]));
                prescribedMedicines.Add(prescribedMedicine);
            }

            return new MedicalExamination(
                long.Parse(tokens[0]),
                long.Parse(tokens[1]),
                long.Parse(tokens[2]),
                long.Parse(tokens[3]),
                long.Parse(tokens[4]),
                long.Parse(tokens[5]),
                prescribedMedicines
                );
        }

        private List<string> MedicalExaminationsToCSVFormat(List<MedicalExamination> medicalExaminations)
        {
            List<string> lines = new List<string>();

            foreach (MedicalExamination medicalExamination in medicalExaminations)
            {
                lines.Add(MedicalExaminationToCSVFormat(medicalExamination));
            }
            return lines;
        }
    }
}
