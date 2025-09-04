using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Location_CategorySpecs
{
    public class Location_CategoryTitleRequiredSpec : ISpecification<Location_Category>
    {
        public bool IsSatisfiedBy(Location_Category entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.LocCategoryTitle);
        }
    }
}
