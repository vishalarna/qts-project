using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Version_Task_RR_Link : Common.Entity
    {
        public Version_Task_RR_Link(int version_TaskId, int version_RegulatoryRequirementId)
        {
            Version_TaskId = version_TaskId;
            Version_RegulatoryRequirementId = version_RegulatoryRequirementId;
        }
        public Version_Task_RR_Link()
        {
        }

        public int Version_TaskId { get; set; }

        public int Version_RegulatoryRequirementId { get; set; }

        public virtual Version_Task Version_Task { get; set; }

        public virtual Version_RegulatoryRequirement Version_RegulatoryRequirement { get; set; }
    }
}
