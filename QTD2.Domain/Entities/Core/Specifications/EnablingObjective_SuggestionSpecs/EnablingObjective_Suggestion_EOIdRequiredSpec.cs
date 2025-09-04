using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.EnablingObjective_SuggestionSpecs
{
    public class EnablingObjective_Suggestion_EOIdRequiredSpec : ISpecification<EnablingObjective_Suggestion>
    {
        public bool IsSatisfiedBy(EnablingObjective_Suggestion entity, params object[] args)
        {
            return entity.EOId > 0;
        }
    }
}
