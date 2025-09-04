using QTD2.Domain.Interfaces.Specification;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Entities.Authentication.Specifications.TaskReQualificationEmp_SignOffSpecs
{
    public class RequiredSpec : ISpecification<TaskReQualificationEmp_SignOff>
    {
        public bool IsSatisfiedBy(TaskReQualificationEmp_SignOff entity, params object[] args)
        {
            return true;
        }
    }
}
