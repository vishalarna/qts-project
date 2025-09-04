using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ClassScheduleEmployeeSpecs
{
    public class ClassSchedule_EmployeePreTestIdRequiredSpec : ISpecification<ClassSchedule_Employee>
    {
        public bool IsSatisfiedBy(ClassSchedule_Employee entity, params object[] args)
        {
            return entity.TestStatusId > 0;
        }
    }
}
