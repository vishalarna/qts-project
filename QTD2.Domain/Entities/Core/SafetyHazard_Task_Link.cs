using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SafetyHazard_Task_Link : Entity
    {
        public int SaftyHazardId { get; set; }

        public int TaskId { get; set; }

        public virtual SaftyHazard SaftyHazard { get; set; }

        public virtual Task Task { get; set; }

        public SafetyHazard_Task_Link(SaftyHazard saftyHazard, Task task)
        {
            SaftyHazardId = saftyHazard.Id;
            TaskId = task.Id;
            SaftyHazard = saftyHazard;
            Task = task;
        }

        public SafetyHazard_Task_Link()
        {
        }
    }
}
