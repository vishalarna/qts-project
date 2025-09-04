using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SafetyHazard_Task_LinkSpecs
{
    public class SafetyHazard_Task_LinkSHIdRequiredSpec : ISpecification<SafetyHazard_Task_Link>
    {
        public bool IsSatisfiedBy(SafetyHazard_Task_Link entity, params object[] args)
        {
            return entity.SaftyHazardId > 0;
        }
    }
}
