using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_ILASpecs
{
    public class Version_ILA_VersionNumberRequiredSpec : ISpecification<Version_ILA>
    {
        public bool IsSatisfiedBy(Version_ILA entity, params object[] args)
        {
            return entity.VersionNumber > 0;
        }
    }
}
