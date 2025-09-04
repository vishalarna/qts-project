using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.EnablingObjectiveSpecs
{
    public class EOCategoryIdRequiredSpec : ISpecification<EnablingObjective>
    {
        public bool IsSatisfiedBy(EnablingObjective entity, params object[] args)
        {
            return entity.CategoryId > 0;
        }
    }
}
