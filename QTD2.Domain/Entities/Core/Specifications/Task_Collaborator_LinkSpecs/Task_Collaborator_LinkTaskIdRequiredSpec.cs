using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Task_Collaborator_LinkSpecs
{
    public class Task_Collaborator_LinkTaskIdRequiredSpec : ISpecification<Task_Collaborator_Link>
    {
        public bool IsSatisfiedBy(Task_Collaborator_Link entity, params object[] args)
        {
            return entity.TaskId > 0;
        }
    }
}
