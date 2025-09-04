using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ILATopicSpecs
{
    public class ILATopicNameRequiredSpec : ISpecification<ILA_Topic>
    {
        public bool IsSatisfiedBy(ILA_Topic entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Name);
        }
    }
}
