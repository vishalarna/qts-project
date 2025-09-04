using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SafetyHazard_HistorySpecs
{
    public class SafetyHazard_HistorySHIdRequiredSpec : ISpecification<SafetyHazard_History>
    {
        public bool IsSatisfiedBy(SafetyHazard_History entity, params object[] args)
        {
            return entity.SafetyHazardId > 0;
        }
    }
}
