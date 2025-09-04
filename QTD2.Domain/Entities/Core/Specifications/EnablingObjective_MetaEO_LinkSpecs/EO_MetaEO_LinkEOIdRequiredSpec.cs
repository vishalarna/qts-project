using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.EnablingObjective_MetaEO_LinkSpecs
{
    public class EO_MetaEO_LinkEOIdRequiredSpec : ISpecification<EnablingObjective_MetaEO_Link>
    {
        public bool IsSatisfiedBy(EnablingObjective_MetaEO_Link entity, params object[] args)
        {
            return entity.EOID > 0;
        }
    }
}
