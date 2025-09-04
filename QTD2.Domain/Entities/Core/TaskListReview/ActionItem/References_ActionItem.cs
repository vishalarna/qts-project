using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class References_ActionItem : ActionItem
    {
        public string? References { get; set; }

        public References_ActionItem(int taskReviewId, int? assigneeId, int priorityId, DateTime assignedDate, DateTime dueDate, string notes, string references) : base(taskReviewId, assigneeId, priorityId, assignedDate, dueDate, notes)
        {
            References = references;
        }

        public References_ActionItem() { }

        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<References_ActionItem>(createdBy);
            return (T)(object)copy;
        }
    }
}
