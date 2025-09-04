using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.EnablingObjective_CategorySpecs
{
    public class EO_CategoryTitleRequiredSpec : ISpecification<EnablingObjective_Category>
    {
        public bool IsSatisfiedBy(EnablingObjective_Category entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Title);
        }
    }
}
