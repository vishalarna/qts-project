using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ILA_RegRequirement_Link : Entity
    {
        public int ILAId { get; set; }

        public int RegulatoryRequirementId { get; set; }

        public virtual ILA ILA { get; set; }

        public virtual RegulatoryRequirement RegulatoryRequirement { get; set; }

        public ILA_RegRequirement_Link(ILA iLA, RegulatoryRequirement regulatoryRequirement)
        {
            ILAId = iLA.Id;
            RegulatoryRequirementId = regulatoryRequirement.Id;
            ILA = iLA;
            RegulatoryRequirement = regulatoryRequirement;
        }

        public ILA_RegRequirement_Link()
        {
        }
    }
}
