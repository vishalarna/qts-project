using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Task_HistorySpecs
{
    public class Task_HistoryTaskIdRequiredSpec : ISpecification<Task_History>
    {
        public bool IsSatisfiedBy(Task_History entity, params object[] args)
        {
            return entity.TaskId > 0;
        }
    }
}
