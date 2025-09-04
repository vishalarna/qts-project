using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.RegRequirement_EO_LinkSpecs
{
    public class RR_LinkEOIdRequiredSpec : ISpecification<RegRequirement_EO_Link>
    {
        public bool IsSatisfiedBy(RegRequirement_EO_Link entity, params object[] args)
        {
            return entity.EOID > 0;
        }
    }
}
