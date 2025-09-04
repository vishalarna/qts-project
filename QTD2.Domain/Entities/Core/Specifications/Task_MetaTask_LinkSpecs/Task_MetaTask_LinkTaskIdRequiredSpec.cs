using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Task_MetaTask_LinkSpecs
{
    public class Task_MetaTask_LinkTaskIdRequiredSpec : ISpecification<Task_MetaTask_Link>
    {
        public bool IsSatisfiedBy(Task_MetaTask_Link entity, params object[] args)
        {
            return entity.TaskId > 0;
        }
    }
}
