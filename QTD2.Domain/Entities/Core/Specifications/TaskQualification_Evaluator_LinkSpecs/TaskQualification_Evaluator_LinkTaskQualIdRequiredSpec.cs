using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TaskQualification_Evaluator_LinkSpecs
{
    public class TaskQualification_Evaluator_LinkTaskQualIdRequiredSpec : ISpecification<TaskQualification_Evaluator_Link>
    {
        public bool IsSatisfiedBy(TaskQualification_Evaluator_Link entity, params object[] args)
        {
            return entity.TaskQualificationId > 0;
        }
    }
}
