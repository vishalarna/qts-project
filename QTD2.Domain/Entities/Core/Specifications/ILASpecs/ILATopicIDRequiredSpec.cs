using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ILASpecs
{
    public class ILATopicIDRequiredSpec : ISpecification<ILA>
    {
        public bool IsSatisfiedBy(ILA entity, params object[] args)
        {
            return entity.ProviderId > 0;
        }
    }
}
