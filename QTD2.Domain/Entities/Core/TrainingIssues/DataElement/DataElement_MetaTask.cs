using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class DataElement_MetaTask : TrainingIssue_DataElement
    {
        public int? MetaTaskId { get; set; }
        public Task MetaTask { get; set; }

        public DataElement_MetaTask(int trainingIssueId, int? metaTaskId) : base(trainingIssueId)
        {
            MetaTaskId = metaTaskId;
        }

        public DataElement_MetaTask() { }
    }
}
