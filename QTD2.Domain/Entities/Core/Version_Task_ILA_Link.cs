using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Version_Task_ILA_Link : Entity
    {
        public int Version_TaskId { get; set; }

        public int Version_ILAId { get; set; }

        public string VersionNumber { get; set; }

        public virtual Version_Task Version_Task { get; set; }

        public virtual Version_ILA Version_ILA { get; set; }

        public Version_Task_ILA_Link()
        {
        }

        public Version_Task_ILA_Link(int version_TaskId, int version_ILAId, string versionNumber)
        {
            Version_TaskId = version_TaskId;
            Version_ILAId = version_ILAId;
            VersionNumber = versionNumber;
        }
    }
}
