using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ILA_TopicLinkSpecs
{
    public class ILA_Topic_LinkILAIdRequiredSpec : ISpecification<ILA_Topic_Link>
    {
        public bool IsSatisfiedBy(ILA_Topic_Link entity, params object[] args)
        {
            return entity.ILAId > 0;
        }
    }
}
