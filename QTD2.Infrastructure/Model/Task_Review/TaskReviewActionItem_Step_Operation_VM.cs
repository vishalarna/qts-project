using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Review
{
    public class TaskReviewActionItem_Step_Operation_VM
    {
        public int Id { get; set; }
        public int OperationTypeId { get; set; }
        public int? Task_StepId { get; set; }
        public string Description { get; set; }
        public string Operation { get; set; }

        public TaskReviewActionItem_Step_Operation_VM(int id, int operationId, int? task_StepId, string description, string operation)
        {
            Id = id;
            OperationTypeId = operationId;
            Task_StepId = task_StepId;
            Description = description;
            Operation = operation;
        }

        public TaskReviewActionItem_Step_Operation_VM(){}
    }
}
