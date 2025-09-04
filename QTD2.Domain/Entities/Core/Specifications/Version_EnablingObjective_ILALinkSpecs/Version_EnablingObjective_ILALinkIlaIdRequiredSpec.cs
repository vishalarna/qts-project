using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_EnablingObjective_ILALinkSpecs
{
    public class Version_EnablingObjective_ILALinkIlaIdRequiredSpec : ISpecification<Version_EnablingObjective_ILALink>
    {
        public bool IsSatisfiedBy(Version_EnablingObjective_ILALink entity, params object[] args)
        {
            return entity.Version_ILAId > 0;
        }
    }
}
