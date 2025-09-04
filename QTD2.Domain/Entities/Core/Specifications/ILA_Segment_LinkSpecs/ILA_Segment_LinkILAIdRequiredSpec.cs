using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ILA_Segment_LinkSpecs
{
    public class ILA_Segment_LinkILAIdRequiredSpec : ISpecification<ILA_Segment_Link>
    {
        public bool IsSatisfiedBy(ILA_Segment_Link entity, params object[] args)
        {
            return entity.ILAId > 0;
        }
    }
}
