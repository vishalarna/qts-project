using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_EnablingObjective_MetaEOLinkSpecs
{
    public class Version_EnablingObjective_MetaEOLinkMetaEOIdRequiredSpec : ISpecification<Version_EnablingObjective_MetaEOLink>
    {
        public bool IsSatisfiedBy(Version_EnablingObjective_MetaEOLink entity, params object[] args)
        {
            return entity.Version_MetaEOId > 0;
        }
    }
}
