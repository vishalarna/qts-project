using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Task_Reference_LinkSpecs
{
    public class Task_Reference_LinkTaskRefIdRequiredSpec : ISpecification<Task_Reference_Link>
    {
        public bool IsSatisfiedBy(Task_Reference_Link entity, params object[] args)
        {
            return entity.TaskReferenceId > 0;
        }
    }
}
