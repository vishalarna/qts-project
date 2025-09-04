using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.NERCTargetAudienceSpecs
{
    public class NERCTargetAudienceNameRequiredSpec : ISpecification<NERCTargetAudience>
    {
        public bool IsSatisfiedBy(NERCTargetAudience entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Name);
        }
    }
}
