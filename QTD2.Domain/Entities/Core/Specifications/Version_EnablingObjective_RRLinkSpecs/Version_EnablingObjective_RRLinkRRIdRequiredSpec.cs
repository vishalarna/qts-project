using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_EnablingObjective_RRLinkSpecs
{
    public class Version_EnablingObjective_RRLinkRRIdRequiredSpec : ISpecification<Version_EnablingObjective_RRLink>
    {
        public bool IsSatisfiedBy(Version_EnablingObjective_RRLink entity, params object[] args)
        {
            return entity.Version_RegulatoryRequirementId > 0;
        }
    }
}
