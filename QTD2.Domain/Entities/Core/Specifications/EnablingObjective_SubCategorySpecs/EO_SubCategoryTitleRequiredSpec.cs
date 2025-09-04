using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.EnablingObjective_SubCategorySpecs
{
    public class EO_SubCategoryTitleRequiredSpec : ISpecification<EnablingObjective_SubCategory>
    {
        public bool IsSatisfiedBy(EnablingObjective_SubCategory entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Title);
        }
    }
}
