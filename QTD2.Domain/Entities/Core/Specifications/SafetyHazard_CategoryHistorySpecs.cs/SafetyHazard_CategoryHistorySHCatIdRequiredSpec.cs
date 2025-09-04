using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SafetyHazard_CategoryHistorySpecs.cs
{
    public class SafetyHazard_CategoryHistorySHCatIdRequiredSpec : ISpecification<SafetyHazard_CategoryHistory>
    {
        public bool IsSatisfiedBy(SafetyHazard_CategoryHistory entity, params object[] args)
        {
            return entity.SafetyHazardCategoryId > 0;
        }
    }
}
