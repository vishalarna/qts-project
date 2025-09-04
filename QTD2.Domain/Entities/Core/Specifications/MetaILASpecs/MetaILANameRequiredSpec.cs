using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.MetaILASpecs
{
    public class MetaILANameRequiredSpec : ISpecification<MetaILA>
    {
        public bool IsSatisfiedBy(MetaILA entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Name);
        }
    }
}
