using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class MetaTask_ActionItem : ActionItem
    {
        public bool? IsMeta { get; set; }

        public MetaTask_ActionItem(int taskReviewId, int? assigneeId, int priorityId, DateTime assignedDate, DateTime dueDate, string notes, bool? isMeta) : base(taskReviewId, assigneeId, priorityId, assignedDate, dueDate, notes)
        {
            IsMeta = isMeta;
        }

        public MetaTask_ActionItem() { }

        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<MetaTask_ActionItem>(createdBy);
            return (T)(object)copy;
        }
    }
}
