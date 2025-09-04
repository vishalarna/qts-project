using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Task_ActionItem : ActionItem
    {
        public int? Number { get; set; }
        public string? Statement { get; set; }

        public Task_ActionItem(int taskReviewId, int? assigneeId, int priorityId, DateTime assignedDate, DateTime dueDate, string notes,int ? number,string statement) : base(taskReviewId, assigneeId, priorityId, assignedDate, dueDate, notes)
        {
            Number = number;
            Statement = statement;
        }

        public Task_ActionItem() { }

        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<Task_ActionItem>(createdBy);
            return (T)(object)copy;
        }
    }
}
