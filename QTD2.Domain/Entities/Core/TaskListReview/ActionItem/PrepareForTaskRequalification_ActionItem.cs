using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class PrepareForTaskRequalification_ActionItem : ActionItem
    {
        public PrepareForTaskRequalification_ActionItem(int taskReviewId, int? assigneeId, int priorityId, DateTime assignedDate, DateTime dueDate, string notes) : base(taskReviewId, assigneeId, priorityId, assignedDate, dueDate, notes)
        {
        }

        public PrepareForTaskRequalification_ActionItem() { }

        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<PrepareForTaskRequalification_ActionItem>(createdBy);
            return (T)(object)copy;
        }
    }
}
