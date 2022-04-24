using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using zdravstvena_ustanova.Exception;
using zdravstvena_ustanova.Model;

namespace Repository
{
    public class LabAnalysisComponentRepository
    {
        private const string NOT_FOUND_ERROR = "INGREDIENT NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _labAnalysisComponentMaxId;

        public LabAnalysisComponentRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _labAnalysisComponentMaxId = GetMaxId(GetAll());
        }
        private long GetMaxId(IEnumerable<LabAnalysisComponent> labAnalysisComponents)
        {
            return labAnalysisComponents.Count() == 0 ? 0 : labAnalysisComponents.Max(labAnalysisComponent => labAnalysisComponent.Id);
        }

        public IEnumerable<LabAnalysisComponent> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToLabAnalysisComponent)
                .ToList();
        }

        public LabAnalysisComponent GetById(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(labAnalysisComponent => labAnalysisComponent.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public LabAnalysisComponent Create(LabAnalysisComponent labAnalysisComponent)
        {
            labAnalysisComponent.Id = ++_labAnalysisComponentMaxId;
            AppendLineToFile(_path, LabAnalysisComponentToCSVFormat(labAnalysisComponent));
            return labAnalysisComponent;
        }
        public bool Update(LabAnalysisComponent labAnalysisComponent)
        {
            var labAnalysisComponents = GetAll();

            foreach (LabAnalysisComponent lac in labAnalysisComponents)
            {
                if (lac.Id == labAnalysisComponent.Id)
                {
                    lac.Value = labAnalysisComponent.Value;
                    lac.MinRefValue = labAnalysisComponent.MinRefValue;
                    lac.MaxRefValue = labAnalysisComponent.MaxRefValue;
                    lac.Name = labAnalysisComponent.Name;
                    WriteLinesToFile(_path, LabAnalysisComponentsToCSVFormat((List<LabAnalysisComponent>)labAnalysisComponents));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long Id)
        {
            var components = (List<LabAnalysisComponent>)GetAll();

            foreach (LabAnalysisComponent lac in components)
            {
                if (lac.Id == Id)
                {
                    components.Remove(lac);
                    WriteLinesToFile(_path, LabAnalysisComponentsToCSVFormat((List<LabAnalysisComponent>)components));
                    return true;
                }
            }
            return false;
        }
        private string LabAnalysisComponentToCSVFormat(LabAnalysisComponent labAnalysisComponent)
        {
            return string.Join(_delimiter,
                labAnalysisComponent.Id,
                labAnalysisComponent.Value,
                labAnalysisComponent.MinRefValue,
                labAnalysisComponent.MaxRefValue,
                labAnalysisComponent.Name
                );
        }

        private LabAnalysisComponent CSVFormatToLabAnalysisComponent(string labAnalysisComponentCSVFormat)
        {
            var tokens = labAnalysisComponentCSVFormat.Split(_delimiter.ToCharArray());
            return new LabAnalysisComponent(
                long.Parse(tokens[0]),
                double.Parse(tokens[1]),
                double.Parse(tokens[2]),
                double.Parse(tokens[3]),
                tokens[4]
                );
        }
        private List<string> LabAnalysisComponentsToCSVFormat(List<LabAnalysisComponent> labAnalysisComponents)
        {
            List<string> lines = new List<string>();

            foreach (LabAnalysisComponent labAnalysisComponent in labAnalysisComponents)
            {
                lines.Add(LabAnalysisComponentToCSVFormat(labAnalysisComponent));
            }
            return lines;
        }

        private void AppendLineToFile(string path, string line)
        {
            File.AppendAllText(path, line + Environment.NewLine);
        }

        private void WriteLinesToFile(string path, List<string> lines)
        {
            File.WriteAllLines(path, lines);
        }
    }
}