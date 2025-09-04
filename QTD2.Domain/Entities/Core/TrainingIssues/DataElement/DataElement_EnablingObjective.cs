using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class DataElement_EnablingObjective : TrainingIssue_DataElement
    {
        public int? EnablingObjectiveId { get; set; }
        public EnablingObjective EnablingObjective { get; set; }

        public DataElement_EnablingObjective(int trainingIssueId,int? enablingObjectiveId) : base(trainingIssueId)
        {
            EnablingObjectiveId = enablingObjectiveId;
        }

        public DataElement_EnablingObjective() { }
    }
}
