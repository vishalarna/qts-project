using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.EnablingObjective_ToolSpecs
{
    public class EnablingObjective_Tool_EOIdRequiredSpec : ISpecification<EnablingObjective_Tool>
    {
        public bool IsSatisfiedBy(EnablingObjective_Tool entity, params object[] args)
        {
            return entity.EOId > 0;
        }
    }
}
