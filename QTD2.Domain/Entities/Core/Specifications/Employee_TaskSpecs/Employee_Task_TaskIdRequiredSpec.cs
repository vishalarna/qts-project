using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.Employee_TaskSpecs
{
    public class Employee_Task_TaskIdRequiredSpec : ISpecification<Employee_Task>
    {
        public bool IsSatisfiedBy(Employee_Task entity, params object[] args)
        {
            return entity.TaskId > 0;
        }
    }
}
