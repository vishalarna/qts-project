using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Task_TrainingGroupSpecs
{
    public class Task_TrainingGroupTaskIdRequiredSpec : ISpecification<Task_TrainingGroup>
    {
        public bool IsSatisfiedBy(Task_TrainingGroup entity, params object[] args)
        {
            return entity.TaskId > 0;
        }
    }
}
