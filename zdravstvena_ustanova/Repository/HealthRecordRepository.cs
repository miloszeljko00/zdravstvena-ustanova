using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Exception;

namespace zdravstvena_ustanova.Repository
{
    public class HealthRecordRepository
    {
        private const string NOT_FOUND_ERROR = "HEALTH RECORD NOT FOUND: {0}={1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _healthRecordMaxId;

        public HealthRecordRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _healthRecordMaxId = GetMaxId(GetAll());
        }

        private long GetMaxId(IEnumerable<HealthRecord> healthRecords)
        {
            return healthRecords.Count() == 0 ? 0 : healthRecords.Max(healthRecord => healthRecord.Id);
        }

        public IEnumerable<HealthRecord> GetAll()
        {
            return File.ReadAllLines(_path).Select(CSVFormatToHealthRecord).ToList();
        }

        public HealthRecord Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(HealthRecord => HealthRecord.Id == id);
            }
            catch(ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public HealthRecord Create(HealthRecord healthRecord)
        {
            healthRecord.Id = ++_healthRecordMaxId;
            AppendLineToFile(_path, HealthRecordToCSVFormat(healthRecord));
            return healthRecord;
        }

        public bool Update(HealthRecord healthRecord)
        {
            var healthRecords = GetAll();

            foreach(HealthRecord hr in healthRecords)
            {
                if (hr.Id == healthRecord.Id)
                {
                    hr.InsuranceNumber = healthRecord.InsuranceNumber;
                    hr.BloodType = healthRecord.BloodType;
                    hr.EmploymentStatus = healthRecord.EmploymentStatus;
                    hr.Allergens = healthRecord.Allergens;
                    hr.Anamnesis = healthRecord.Anamnesis;
                    hr.LabAnalysisRecord = healthRecord.LabAnalysisRecord;
                    hr.HospitalizationRecord = healthRecord.HospitalizationRecord;
                    hr.PrescribedMedicine = healthRecord.PrescribedMedicine;
                    hr.PatientDisease = healthRecord.PatientDisease;
                    hr.PatientVaccination = healthRecord.PatientVaccination;
                    hr.Patient = healthRecord.Patient;
                    WriteLinesToFile(_path, HealthRecordsToCSVFormat((List<HealthRecord>)healthRecords));
                    return true;
                }
            }
            return false;
        }

        public bool Delete(long healthRecordId)
        {
            var healthRecords = (List<HealthRecord>)GetAll();
            foreach (HealthRecord hr in healthRecords)
            {
                if (hr.Id == healthRecordId)
                {
                    healthRecords.Remove(hr);
                    WriteLinesToFile(_path, HealthRecordsToCSVFormat((List<HealthRecord>)healthRecords));
                    return true;
                }
            }
            return false;
        }

        private string HealthRecordToCSVFormat(HealthRecord healthRecord)
        {
            string allergens = "";
            for(int i=0;i<healthRecord.Allergens.Count; i++)
            {
                allergens = string.Join(_delimiter, allergens, healthRecord.Allergens[i].Id);
            }

            string anamnesis = "";
            for(int i=0; i<healthRecord.Anamnesis.Count; i++)
            {
                anamnesis = string.Join(_delimiter, anamnesis, healthRecord.Anamnesis[i].Id);
            }

            string labAnalysisRecords = "";
            for (int i = 0; i < healthRecord.LabAnalysisRecord.Count; i++)
            {
                labAnalysisRecords = string.Join(_delimiter, labAnalysisRecords, healthRecord.LabAnalysisRecord[i].Id);
            }

            string hospitalizationRecords = "";
            for (int i = 0; i < healthRecord.HospitalizationRecord.Count; i++)
            {
                hospitalizationRecords = string.Join(_delimiter, hospitalizationRecords, healthRecord.HospitalizationRecord[i].Id);
            }

            string prescribedMedicines = "";
            for (int i = 0; i < healthRecord.PrescribedMedicine.Count; i++)
            {
                prescribedMedicines = string.Join(_delimiter, prescribedMedicines, healthRecord.PrescribedMedicine[i].Id);
            }

            string patientDiseases = "";
            for (int i = 0; i < healthRecord.PatientDisease.Count; i++)
            {
                patientDiseases = string.Join(_delimiter, patientDiseases, healthRecord.PatientDisease[i].Id);
            }

            string patientVaccinations = "";
            for (int i = 0; i < healthRecord.PatientVaccination.Count; i++)
            {
                patientVaccinations = string.Join(_delimiter, patientVaccinations, healthRecord.PatientVaccination[i].Id);
            }

            return string.Join(_delimiter,
                healthRecord.Id,
                healthRecord.InsuranceNumber,
                (int)healthRecord.BloodType,
                (int)healthRecord.EmploymentStatus,
                healthRecord.Allergens.Count,
                allergens,
                healthRecord.Anamnesis.Count,
                anamnesis,
                healthRecord.LabAnalysisRecord.Count,
                labAnalysisRecords,
                healthRecord.HospitalizationRecord.Count,
                hospitalizationRecords,
                healthRecord.PrescribedMedicine.Count,
                prescribedMedicines,
                healthRecord.PatientDisease.Count,
                patientDiseases,
                healthRecord.PatientVaccination.Count,
                patientVaccinations,
                healthRecord.Patient.Id
                ).Replace(";;", ";");
        }

        private void AppendLineToFile(string path, string line)
        {
            File.AppendAllText(path, line+Environment.NewLine);
        }

        private void WriteLinesToFile(string path, List<string> lines)
        {
            File.WriteAllLines(path, lines);
        }

        private HealthRecord CSVFormatToHealthRecord(string healthRecordCSVFormat)
        {
            var tokens = healthRecordCSVFormat.Split(_delimiter.ToCharArray());

            List<Allergens> allergens = new List<Allergens>();
            for(int i=4+1;i<4+1+int.Parse(tokens[4]); i++)
            {
                var allergen = new Allergens(int.Parse(tokens[i]));
                allergens.Add(allergen);
            }

            List<Anamnesis> anamnesiss = new List<Anamnesis>();
            for (int i = 4 + 1 + int.Parse(tokens[4])+1; i < 4+1+int.Parse(tokens[4])+1+int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]); i++)
            {
                var anamnesis = new Anamnesis(int.Parse(tokens[i]));
                anamnesiss.Add(anamnesis);
            }

            List<LabAnalysisRecord> lars = new List<LabAnalysisRecord>();
            for (int i = 4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])+1;
                i< 4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1 
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]); i++)
            {
                var lar = new LabAnalysisRecord(int.Parse(tokens[i]));
                lars.Add(lar);
            }

            List<HospitalizationRecord> hrs = new List<HospitalizationRecord>();
            for (int i = 4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]) + 1;
                i < 4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]) + 1
                +int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])])]);
                i++)
            {
                var hr = new HospitalizationRecord(int.Parse(tokens[i]));
                hrs.Add(hr);
            }

            List<PrescribedMedicine> pms = new List<PrescribedMedicine>();
            for (int i = 4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])])])+1;
                i < 4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])])]) + 1 +
                int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])])])]);
                i++)
            {
                var pm = new PrescribedMedicine(int.Parse(tokens[i]));
                pms.Add(pm);
            }

            List<PatientDisease> pds= new List<PatientDisease>();
            for(int i= 4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])])]) + 1 +
                int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])])])])+1;
                i< 4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])])]) + 1 +
                int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])])])]) + 1+
                int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])])]) + 1 +
                int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])])])])]);
                i++)
            {
                var pd = new PatientDisease(int.Parse(tokens[i]));
                pds.Add(pd);
            }

            List<PatientVaccination> pvs = new List<PatientVaccination>();
            for (int i = 4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])])]) + 1 +
                int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])])])]) + 1 +
                int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])])]) + 1 +
                int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])])])])])+1;
                i < 4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])])]) + 1 +
                int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])])])]) + 1 +
                int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])])]) + 1 +
                int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])])])])]) + 1 +
                int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])])]) + 1 +
                int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])])])]) + 1 +
                int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])])]) + 1 +
                int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])]) + 1
                + int.Parse(tokens[4 + 1 + int.Parse(tokens[4]) + 1 + int.Parse(tokens[4 + 1 + int.Parse(tokens[4])])])])])])]);
                i++)
            {
                var pv = new PatientVaccination(int.Parse(tokens[i]));
                pvs.Add(pv);
            }

            int lastIndex = 4 + 7 + allergens.Count + anamnesiss.Count + lars.Count + hrs.Count + pms.Count + pds.Count + pvs.Count;

            return new HealthRecord(
                long.Parse(tokens[0]),
                int.Parse(tokens[1]),
                (BloodType)int.Parse(tokens[2]),
                (EmploymentStatus)int.Parse(tokens[3]),
                allergens,
                anamnesiss,
                lars,
                hrs,
                pms,
                pds,
                pvs,
                long.Parse(tokens[lastIndex])
                );
        }

        private List<string> HealthRecordsToCSVFormat(List<HealthRecord> healthRecords)
        {
            List<string> lines = new List<string>();
            
            foreach(HealthRecord healthRecord in healthRecords)
            {
                lines.Add(HealthRecordToCSVFormat(healthRecord));
            }
            return lines;
        }
    }
}
