using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Instructor_CategorySpecs
{
    public class Instructor_CategoryTitleRequiredSpec : ISpecification<Instructor_Category>
    {
        public bool IsSatisfiedBy(Instructor_Category entity, params object[] args)
        {
           return !string.IsNullOrEmpty(entity.ICategoryTitle);
        }
    }
}
