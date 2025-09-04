using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SafetyHazard_Tool_LinkSpecs
{
    public class SH_Tool_LinkSHIdRequiredSpec : ISpecification<SafetyHazard_Tool_Link>
    {
        public bool IsSatisfiedBy(SafetyHazard_Tool_Link entity, params object[] args)
        {
            return entity.SafetyHazardId > 0;
        }
    }
}
