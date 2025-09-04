using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Review
{
    public class TaskReviewActionItem_SubDutyArea_Operation_VM
    {
        public int Id { get; set; }
        public int OperationTypeId { get; set; }
        public int SubDutyAreaId { get; set; }
        public string Operation { get; set; }

        public TaskReviewActionItem_SubDutyArea_Operation_VM(int id, int operationId, int subDutyAreaId, string operation)
        {
            Id = id;
            OperationTypeId = operationId;
            SubDutyAreaId = subDutyAreaId;
            Operation = operation;
        }

        public TaskReviewActionItem_SubDutyArea_Operation_VM(){}
    }
}
