using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Version_Task_Position_Link : Entity
    {
        public int Version_TaskId { get; set; }

        public int Version_PositionId { get; set; }

        public string VersionNumber { get; set; }

        public virtual Version_Task Version_Task { get; set; }

        public virtual Version_Position Version_Position { get; set; }

        public Version_Task_Position_Link()
        {
        }

        public Version_Task_Position_Link(int version_taskId, int version_posId,string versionNumber = "1.0")
        {
            Version_TaskId = version_taskId;
            Version_PositionId = version_posId;
            VersionNumber = versionNumber;
        }
    }
}
