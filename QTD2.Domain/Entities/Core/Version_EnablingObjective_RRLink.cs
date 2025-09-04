using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Version_EnablingObjective_RRLink : Entity
    {
        public int Version_EnablingObjectiveId { get; set; }

        public int Version_RegulatoryRequirementId { get; set; }

        public string Version_Number { get; set; }

        public virtual Version_EnablingObjective Version_EnablingObjective { get; set; }

        public virtual Version_RegulatoryRequirement Version_RegulatoryRequirement { get; set; }

        public Version_EnablingObjective_RRLink()
        {
        }

        public Version_EnablingObjective_RRLink(Version_EnablingObjective version_EnablingObjective, Version_RegulatoryRequirement version_RegulatoryRequirement, string version_number="")
        {
            Version_Number = version_number;
            Version_EnablingObjective = version_EnablingObjective;
            Version_RegulatoryRequirement = version_RegulatoryRequirement;
            Version_EnablingObjectiveId = version_EnablingObjective.Id;
            Version_RegulatoryRequirementId = version_RegulatoryRequirement.Id;
        }
    }
}
