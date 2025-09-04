using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SafetyHazard_ILA_LinkSpecs
{
    public class SafetyHazard_ILA_LinkSHIdRequiredSpec : ISpecification<SafetyHazard_ILA_Link>
    {
        public bool IsSatisfiedBy(SafetyHazard_ILA_Link entity, params object[] args)
        {
            return entity.SafetyHazardId > 0;
        }
    }
}
