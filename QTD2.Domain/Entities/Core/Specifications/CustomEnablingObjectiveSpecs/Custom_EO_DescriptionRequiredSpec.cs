using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.CustomEnablingObjectiveSpecs
{
    public class Custom_EO_DescriptionRequiredSpec : ISpecification<CustomEnablingObjective>
    {
        public bool IsSatisfiedBy(CustomEnablingObjective entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Description);
        }
    }
}
