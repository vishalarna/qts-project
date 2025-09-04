using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.RR_StatusHistorySpecs
{
    public class RR_StatusHistoryRRIdRequiredSpec : ISpecification<RR_StatusHistory>
    {
        public bool IsSatisfiedBy(RR_StatusHistory entity, params object[] args)
        {
            return entity.RegulatoryRequirementId > 0;
        }
    }
}
