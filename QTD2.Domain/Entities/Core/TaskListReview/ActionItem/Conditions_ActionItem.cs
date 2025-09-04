using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Conditions_ActionItem : ActionItem
    {
        public string? Conditions { get; set; }

        public Conditions_ActionItem(int taskReviewId, int? assigneeId, int priorityId, DateTime assignedDate, DateTime dueDate, string notes, string conditions) : base(taskReviewId, assigneeId, priorityId, assignedDate, dueDate, notes)
        {
            Conditions = conditions;
        }

        public Conditions_ActionItem() { }
        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<Conditions_ActionItem>(createdBy);
            return (T)(object)copy;
        }
    }
}
