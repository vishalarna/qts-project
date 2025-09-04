using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Version_Task_MetaTask_Link : Common.Entity
    {
        public Version_Task_MetaTask_Link(Version_Task version_task, Version_Task version_metatask)
        {
            Version_TaskId = version_task.Id;
            Version_MetaTaskId = version_metatask.Id;
        }

        public Version_Task_MetaTask_Link()
        {
        }

        public int Version_TaskId { get; set; }

        public int Version_MetaTaskId { get; set; }

        public virtual Version_Task Version_Task { get; set; }

        public virtual Version_Task Version_MetaTask { get; set; }
    }
}
