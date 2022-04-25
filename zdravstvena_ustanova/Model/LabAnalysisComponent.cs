using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdravstvena_ustanova.Model
{
    public class LabAnalysisComponent
    {
        public long Id { get; set; }
        public double Value { get; set; }
        public double MinRefValue { get; set; }
        public double MaxRefValue { get; set; }
        public string Name { get; set; }

        public LabAnalysisComponent(long id, double value, double minRefValue, double maxRefValue, string name)
        {
            Id = id;
            Value = value;
            MinRefValue = minRefValue;
            MaxRefValue = maxRefValue;
            Name = name;
        }
    }
}
