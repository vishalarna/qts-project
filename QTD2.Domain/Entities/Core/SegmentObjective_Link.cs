using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SegmentObjective_Link : Entity
    {
        public int SegmentId { get; set; }

        public int Order { get; set; }

        public int? TaskId { get; set; }

        public int? EnablingObjectiveId { get; set; }

        public int? CustomEOId { get; set; }

        public virtual Segment Segment { get; set; }

        public virtual Task Task { get; set; }

        public virtual EnablingObjective EnablingObjective { get; set; }

        public virtual CustomEnablingObjective CustomEnablingObjective { get; set; }

        public SegmentObjective_Link(Segment segment, Task task, EnablingObjective enablingObjective, CustomEnablingObjective co)
        {
            Segment = segment;
            Task = task;
            EnablingObjective = enablingObjective;
            SegmentId = segment.Id;
            TaskId = task == null ? null:task.Id;
            EnablingObjectiveId = enablingObjective?.Id;
            CustomEOId = co?.Id;
        }

        public SegmentObjective_Link()
        {
        }

        public void SetOrder(int order)
        {
            Order = order;
        }
    }
}
