using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SaftyHazard_CategorySpecs
{
    public class SaftyHazard_CategoryTitleRequiredSpec : ISpecification<SaftyHazard_Category>
    {
        public bool IsSatisfiedBy(SaftyHazard_Category entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Title);
        }
    }
}
