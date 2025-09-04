using QTD2.Domain.Interfaces.Specification;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Entities.Authentication.Specifications.TaskReQualificationEmp_StepsSpecs
{
    public class RequiredSpec : ISpecification<TaskReQualificationEmp_Steps>
    {
        public bool IsSatisfiedBy(TaskReQualificationEmp_Steps entity, params object[] args)
        {
            return true;
        }
    }
}
