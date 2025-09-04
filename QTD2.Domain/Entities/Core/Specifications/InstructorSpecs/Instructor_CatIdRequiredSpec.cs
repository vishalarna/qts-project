using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.InstructorSpecs
{
    public class Instructor_CatIdRequiredSpec : ISpecification<Instructor>
    {
        public bool IsSatisfiedBy(Instructor entity, params object[] args)
        {
            return entity.ICategoryId > 0;
        }
    }
}
