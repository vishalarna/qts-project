using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class DataElement_SafetyHazard : TrainingIssue_DataElement
    {
        public int? SafetyHazardId { get; set; }
        public SaftyHazard SafetyHazard { get; set; }
        public DataElement_SafetyHazard(int trainingIssueId, int? safetyHazardId) : base(trainingIssueId)
        {
            SafetyHazardId = safetyHazardId;
        }
        public DataElement_SafetyHazard()
        {

        }
    }
}
