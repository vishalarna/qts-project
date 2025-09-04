using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SafetyHazard_EO_Link : Entity
    {
        public int SafetyHazardId { get; set; }

        public int EOID { get; set; }

        public virtual SaftyHazard SaftyHazard { get; set; }

        public virtual EnablingObjective EnablingObjective { get; set; }

        public SafetyHazard_EO_Link()
        {
        }

        public SafetyHazard_EO_Link(SaftyHazard saftyHazard, EnablingObjective enablingObjective)
        {
            SafetyHazardId = saftyHazard.Id;
            EOID = enablingObjective.Id;
            SaftyHazard = saftyHazard;
            EnablingObjective = enablingObjective;
        }
    }
}
