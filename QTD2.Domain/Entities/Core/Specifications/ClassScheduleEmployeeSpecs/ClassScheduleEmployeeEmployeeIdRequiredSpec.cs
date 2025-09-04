using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ClassScheduleEmployeeSpecs
{
    public class ClassScheduleEmployeeEmployeeIdRequiredSpec : ISpecification<ClassSchedule_Employee>
    {
        public bool IsSatisfiedBy(ClassSchedule_Employee entity, params object[] args)
        {
            return entity.EmployeeId > 0;
        }
    }
}
