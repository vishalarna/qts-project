using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class RegRequirement_EO_Link : Entity
    {
        public int RegulatoryRequirementId { get; set; }

        public int EOID { get; set; }

        public virtual RegulatoryRequirement RegulatoryRequirement { get; set; }

        public virtual EnablingObjective EO { get; set; }

        public RegRequirement_EO_Link()
        {
        }

        public RegRequirement_EO_Link(RegulatoryRequirement regulatoryRequirement, EnablingObjective eO)
        {
            RegulatoryRequirementId = regulatoryRequirement.Id;
            EOID = eO.Id;
            RegulatoryRequirement = regulatoryRequirement;
            EO = eO;
        }
    }
}
