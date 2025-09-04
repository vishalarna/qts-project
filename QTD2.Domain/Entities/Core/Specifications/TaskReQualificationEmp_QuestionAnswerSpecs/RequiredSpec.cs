using QTD2.Domain.Interfaces.Specification;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Entities.Authentication.Specifications.TaskReQualificationEmp_QuestionAnswerSpecs
{
    public class RequiredSpec : ISpecification<TaskReQualificationEmp_QuestionAnswer>
    {
        public bool IsSatisfiedBy(TaskReQualificationEmp_QuestionAnswer entity, params object[] args)
        {
            return true;
        }
    }
}
