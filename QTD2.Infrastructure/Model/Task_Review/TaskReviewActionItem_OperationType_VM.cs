using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Review
{
    public class TaskReviewActionItem_OperationType_VM
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public TaskReviewActionItem_OperationType_VM(int id, string type)
        {
            Id = id;
            Type = type;
        }
    }
}
