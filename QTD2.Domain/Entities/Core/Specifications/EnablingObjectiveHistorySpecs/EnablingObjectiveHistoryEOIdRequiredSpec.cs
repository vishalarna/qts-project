using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.EnablingObjectiveHistorySpecs
{
    public class EnablingObjectiveHistoryEOIdRequiredSpec : ISpecification<EnablingObjectiveHistory>
    {
        public bool IsSatisfiedBy(EnablingObjectiveHistory entity, params object[] args)
        {
            return entity.EnablingObjectiveId > 0;
        }
    }
}
