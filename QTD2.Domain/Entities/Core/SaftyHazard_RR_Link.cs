using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SaftyHazard_RR_Link : Entity
    {
        public int RegulatoryRequirementId { get; set; }

        public int SafetyHazardId { get; set; }

        public virtual RegulatoryRequirement RegulatoryRequirement { get; set; }

        public virtual SaftyHazard SaftyHazard { get; set; }

        public SaftyHazard_RR_Link(RegulatoryRequirement regulatoryRequirement, SaftyHazard saftyHazard)
        {
            RegulatoryRequirement = regulatoryRequirement;
            SaftyHazard = saftyHazard;
            RegulatoryRequirementId = regulatoryRequirement.Id;
            SafetyHazardId = saftyHazard.Id;
        }

        public SaftyHazard_RR_Link()
        {
        }
    }
}
