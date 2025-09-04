using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class DataElement_MetaEnablingObjective : TrainingIssue_DataElement
    {
        public int? MetaEnablingObjectiveId { get; set; }
        public EnablingObjective MetaEnablingObjective { get; set; }

        public DataElement_MetaEnablingObjective(int trainingIssueId, int? metaEnablingObjectiveId): base(trainingIssueId)
        {
            MetaEnablingObjectiveId = metaEnablingObjectiveId;
        }
        public DataElement_MetaEnablingObjective() { }
    }
}
