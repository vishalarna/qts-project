using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class RR_Task_Link : Entity
    {
        public int RegRequirementId { get; set; }

        public int TaskId { get; set; }

        public virtual RegulatoryRequirement RegulatoryRequirement { get; set; }

        public virtual Task Task { get; set; }

        public RR_Task_Link(RegulatoryRequirement regRequirement, Task task)
        {
            RegRequirementId = regRequirement.Id;
            TaskId = task.Id;
            Task = task;
            RegulatoryRequirement = regRequirement;
        }

        public RR_Task_Link()
        {
        }
    }
}
