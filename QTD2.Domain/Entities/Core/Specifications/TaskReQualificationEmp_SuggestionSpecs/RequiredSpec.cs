using QTD2.Domain.Interfaces.Specification;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Entities.Authentication.Specifications.TaskReQualificationEmp_SuggestionSpecs
{
    public class RequiredSpec : ISpecification<TaskReQualificationEmp_Suggestion>
    {
        public bool IsSatisfiedBy(TaskReQualificationEmp_Suggestion entity, params object[] args)
        {
            return true;
        }
    }
}
