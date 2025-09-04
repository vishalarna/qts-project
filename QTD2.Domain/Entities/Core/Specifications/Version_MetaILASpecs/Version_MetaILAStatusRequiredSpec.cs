using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_MetaILASpecs
{
    public class Version_MetaILAStatusRequiredSpec : ISpecification<Version_MetaILA>
    {
        public bool IsSatisfiedBy(Version_MetaILA entity, params object[] args)
        {
            return entity.MetaILAStatusId > 0;
        }
    }
}
