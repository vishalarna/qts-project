using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace QTD2.Domain.Entities.Core
{
    public class R5ImpactedTask : Common.Entity
    {
        public int PositionTaskId { get; set; }
        public virtual Position_Task PositionTask { get; set; }
        public int ImpactedTaskId { get; set; }
        public virtual Task ImpactedTask { get; set; }
        public R5ImpactedTask()
        {
        }
        public R5ImpactedTask(int positionTaskId, int impactedTaskId)
        {
            PositionTaskId = positionTaskId;
            ImpactedTaskId = impactedTaskId;
        }
    }
}