using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.RR_Task_LinkSpecs
{
    public class RR_Task_LinkTaskIdRequiredSpec : ISpecification<RR_Task_Link>
    {
        public bool IsSatisfiedBy(RR_Task_Link entity, params object[] args)
        {
            return entity.TaskId > 0;
        }
    }
}
