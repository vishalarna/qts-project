using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Version_EnablingObjective_TaskSpecs
{
    public class Version_EnablingObjective_TaskVersionEOIdRequiredSpec : ISpecification<Version_EnablingObjective_Task>
    {
        public bool IsSatisfiedBy(Version_EnablingObjective_Task entity, params object[] args)
        {
            return entity.Version_EnablingObjectiveId > 0;
        }
    }
}
