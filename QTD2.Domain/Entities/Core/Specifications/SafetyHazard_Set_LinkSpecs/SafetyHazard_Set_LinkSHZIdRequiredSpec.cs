using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SafetyHazard_Set_LinkSpecs
{
    public class SafetyHazard_Set_LinkSHZIdRequiredSpec : ISpecification<SafetyHazard_Set_Link>
    {
        public bool IsSatisfiedBy(SafetyHazard_Set_Link entity, params object[] args)
        {
            return entity.SafetyHazardId > 0;
        }
    }
}
