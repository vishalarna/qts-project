using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Review
{
    public class TaskReviewActionItem_EnablingObjective_Operation_VM
    {
        public int Id { get; set; }
        public int OperationTypeId { get; set; }
        public int EnablingObjectiveId { get; set; }
        public string Operation { get; set; }

        public TaskReviewActionItem_EnablingObjective_Operation_VM(int id, int operationId, int enablingObjectiveId, string operation)
        {
            Id = id;
            OperationTypeId = operationId;
            EnablingObjectiveId = enablingObjectiveId;
            Operation = operation;
        }
        public TaskReviewActionItem_EnablingObjective_Operation_VM(){}
    }
}
