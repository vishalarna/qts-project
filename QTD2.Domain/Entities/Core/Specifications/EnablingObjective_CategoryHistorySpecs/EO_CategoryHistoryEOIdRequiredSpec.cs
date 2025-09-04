using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.EnablingObjective_CategoryHistorySpecs
{
    public class EO_CategoryHistoryEOIdRequiredSpec : ISpecification<EnablingObjective_CategoryHistory>
    {
        public bool IsSatisfiedBy(EnablingObjective_CategoryHistory entity, params object[] args)
        {
            return entity.EnablingObjectiveCategoryId > 0;
        }
    }
}
