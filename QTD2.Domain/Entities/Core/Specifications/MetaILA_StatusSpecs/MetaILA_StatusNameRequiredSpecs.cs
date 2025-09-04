using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.MetaILA_StatusSpecs
{
    public class MetaILA_StatusNameRequiredSpecs : ISpecification<MetaILA_Status>
    {
        public bool IsSatisfiedBy(MetaILA_Status entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Name);
        }
    }
}
