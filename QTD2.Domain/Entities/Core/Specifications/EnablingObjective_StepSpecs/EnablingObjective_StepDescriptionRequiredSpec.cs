using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.EnablingObjective_StepSpecs
{
    public class EnablingObjective_StepDescriptionRequiredSpec : ISpecification<EnablingObjective_Step>
    {
        public bool IsSatisfiedBy(EnablingObjective_Step entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Description);
        }
    }
}
