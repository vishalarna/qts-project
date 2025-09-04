using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ILA_EnablingObjective_LinkSpecs
{
    public class ILA_EO_LinkILAIdRequiredSpec : ISpecification<ILA_EnablingObjective_Link>
    {
        public bool IsSatisfiedBy(ILA_EnablingObjective_Link entity, params object[] args)
        {
            return entity.ILAId > 0;
        }
    }
}
