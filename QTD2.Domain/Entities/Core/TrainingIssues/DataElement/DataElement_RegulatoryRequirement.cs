using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class DataElement_RegulatoryRequirement : TrainingIssue_DataElement
    {
        public int? RegulatoryRequirementId { get; set; }
        public RegulatoryRequirement RegulatoryRequirement { get; set; }
        public DataElement_RegulatoryRequirement(int trainingIssueId, int? regulatoryRequirementId) : base(trainingIssueId)
        {
            RegulatoryRequirementId = regulatoryRequirementId;
        }
        public DataElement_RegulatoryRequirement()
        {

        }
    }
}
