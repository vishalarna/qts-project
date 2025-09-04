using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ILA_Position_LinkSpecs
{
    public class ILA_Position_LinkPositionIdRequiredSpec : ISpecification<ILA_Position_Link>
    {
        public bool IsSatisfiedBy(ILA_Position_Link entity, params object[] args)
        {
            return entity.PositionId > 0;
        }
    }
}
