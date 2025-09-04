using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Task_ILA_LinkSpecs
{
    public class Task_ILA_LinkILAIdRequiredSpec : ISpecification<Task_ILA_Link>
    {
        public bool IsSatisfiedBy(Task_ILA_Link entity, params object[] args)
        {
            return entity.ILAId > 0;
        }
    }
}
