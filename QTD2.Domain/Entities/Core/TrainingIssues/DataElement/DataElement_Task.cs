using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class DataElement_Task : TrainingIssue_DataElement
    {
        public int? TaskId { get; set; }
        public Task Task { get; set; }
        public DataElement_Task(int trainingIssueId, int? taskId) : base(trainingIssueId)
        {
            TaskId = taskId;
        }
        public DataElement_Task()
        {

        }
    }
}
