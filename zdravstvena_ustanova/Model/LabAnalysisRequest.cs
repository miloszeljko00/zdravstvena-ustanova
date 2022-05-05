using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zdravstvena_ustanova.Model
{
    public class LabAnalysisRequest
    {
        public long Id { get; set; }
        public bool IsUrgent { get; set; }
        public List<LabAnalysisComponent> LabAnalysisComponent { get; set; }
        public LabAnalysisRequest(long id, bool isUrgent)
        {
            Id = id;
            IsUrgent = isUrgent;
        }

        public LabAnalysisRequest(long id, bool isUrgent, List<LabAnalysisComponent> labAnalysisComponent) : this(id, isUrgent)
        {
            LabAnalysisComponent = labAnalysisComponent;
        }
        public LabAnalysisRequest(long id)
        {
            Id = id;
        }
    }
}
