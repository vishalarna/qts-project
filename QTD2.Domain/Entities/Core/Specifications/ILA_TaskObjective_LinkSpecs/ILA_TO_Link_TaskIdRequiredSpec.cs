using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ILA_TaskObjective_LinkSpecs
{
    public class ILA_TO_Link_TaskIdRequiredSpec : ISpecification<ILA_TaskObjective_Link>
    {
        public bool IsSatisfiedBy(ILA_TaskObjective_Link entity, params object[] args)
        {
            return entity.TaskId > 0;
        }
    }
}
