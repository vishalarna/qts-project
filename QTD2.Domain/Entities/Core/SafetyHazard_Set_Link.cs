using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SafetyHazard_Set_Link : Entity
    {
        public int SafetyHazardId { get; set; }

        public int SafetyHazardSetId { get; set; }

        public virtual SaftyHazard SafetyHazard { get; set; }

        public virtual SafetyHazard_Set SafetyHazardSet { get; set; }

        public SafetyHazard_Set_Link(SaftyHazard safetyHazard, SafetyHazard_Set safetyHazardSet)
        {
            SafetyHazardId = safetyHazard.Id;
            SafetyHazardSetId = safetyHazardSet.Id;
            SafetyHazard = safetyHazard;
            SafetyHazardSet = safetyHazardSet;
        }

        public SafetyHazard_Set_Link()
        {
        }
    }
}
