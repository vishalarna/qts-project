using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.EnablingObjective_Employee_LinkSpecs
{
    internal class EnablingObjective_Employee_LinkEOIdRequiredSpec : ISpecification<EnablingObjective_Employee_Link>
    {
        public bool IsSatisfiedBy(EnablingObjective_Employee_Link entity, params object[] args)
        {
            return entity.EOID > 0;
        }
    }
}
