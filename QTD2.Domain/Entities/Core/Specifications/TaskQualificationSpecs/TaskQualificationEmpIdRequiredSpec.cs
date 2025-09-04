using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TaskQualificationSpecs
{
    public class TaskQualificationEmpIdRequiredSpec : ISpecification<TaskQualification>
    {
        public bool IsSatisfiedBy(TaskQualification entity, params object[] args)
        {
            return entity.EmpId > 0;
        }
    }
}
