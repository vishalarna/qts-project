using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.ILA_ResourceSpecs
{
    public class ILAResourceTitleRequiredSpec : ISpecification<ILA_Resource>
    {
        public bool IsSatisfiedBy(ILA_Resource entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Title);
        }
    }
}
