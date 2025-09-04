using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ILA_RegRequirement_LinkSpecs
{
    public class ILA_RegReq_LinkRegReqIdRequiredSpec : ISpecification<ILA_RegRequirement_Link>
    {
        public bool IsSatisfiedBy(ILA_RegRequirement_Link entity, params object[] args)
        {
            return entity.RegulatoryRequirementId > 0;
        }
    }
}
