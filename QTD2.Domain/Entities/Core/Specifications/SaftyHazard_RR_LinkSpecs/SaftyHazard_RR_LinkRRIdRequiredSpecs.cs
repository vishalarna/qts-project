using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SaftyHazard_RR_LinkSpecs
{
    public class SaftyHazard_RR_LinkRRIdRequiredSpecs : ISpecification<SaftyHazard_RR_Link>
    {
        public bool IsSatisfiedBy(SaftyHazard_RR_Link entity, params object[] args)
        {
            return entity.RegulatoryRequirementId > 0;
        }
    }
}