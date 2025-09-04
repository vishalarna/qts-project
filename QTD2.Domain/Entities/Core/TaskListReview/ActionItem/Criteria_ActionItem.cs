using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Criteria_ActionItem : ActionItem
    {
        public string? Criteria { get; set; }

        public Criteria_ActionItem(int taskReviewId, int? assigneeId, int priorityId, DateTime assignedDate, DateTime dueDate, string notes, string criteria) : base(taskReviewId, assigneeId, priorityId, assignedDate, dueDate, notes)
        {
            Criteria = criteria;
        }

        public Criteria_ActionItem() { }
        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<Criteria_ActionItem>(createdBy);
            return (T)(object)copy;
        }
    }
}
