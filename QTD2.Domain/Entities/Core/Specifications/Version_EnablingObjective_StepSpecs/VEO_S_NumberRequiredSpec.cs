using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.Version_EnablingObjective_StepSpecs
{
    public class VEO_S_NumberRequiredSpec : ISpecification<Version_EnablingObjective_Step>
    {
        public bool IsSatisfiedBy(Version_EnablingObjective_Step entity, params object[] args)
        {
            return entity.Number > 0;             
        }
    }
}
